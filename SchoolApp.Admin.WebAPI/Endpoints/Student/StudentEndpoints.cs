using Admin.Application.Commands;

using AutoMapper;

using Azure.Core;

using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

using SchoolApp.Admin.Application.Commands;
using SchoolApp.Admin.Application.Commands.Student;
using SchoolApp.EventBus.Extensions;

using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace SchoolApp.Admin.WebAPI.Endpoints.Student;

public static class StudentEndpoints
{
    public static RouteGroupBuilder MapStudentEndpoints(this RouteGroupBuilder app)
    {
        app.MapGet("/Student", GetAllStudentsAsync);
        app.MapGet("/Student/{studentId:int}", GetStudentByIdAsync);
        app.MapPost("/Student", CreateStudentAsync);
        app.MapPut("/Student/{studentId:int}", UpdateStudentAsync);
        app.MapDelete("/Student/{studentId:int}", DeleteStudentAsync);

        return app;
    }

    public static async Task<Ok<IEnumerable<Application.Queries.Students.Student>>> GetAllStudentsAsync([FromServices] StudentServices services)
    {
        var students = await services.Queries.GetAllStudentsAsync();
        return TypedResults.Ok(students);

    }

    public static async Task<Results<Ok<Application.Queries.Students.Student>, NotFound<string>>> GetStudentByIdAsync(int studentId, [FromServices] StudentServices services)
    {
        services.Logger.LogInformation("Fetching student with ID {StudentId}", studentId);

        var student = await services.Queries.GetStudentByIdAsync(studentId.ToString());
        return TypedResults.Ok(student);
    }

    public static async Task<Results<Ok, BadRequest<string>>> CreateStudentAsync(
       [FromBody] CreateStudentRequest request, [FromHeader(Name = "x-requestid")] Guid requestId, [FromServices] StudentServices services)
    {
        if (requestId == Guid.Empty)
        {
            services.Logger.LogWarning("Empty GUID provided as Request ID");
            return TypedResults.BadRequest("Empty GUID is not valid for request ID");
        }

        services.Logger.LogInformation("Processing student creation request with ID {RequestId}", requestId);
        var createStudentCommand = new CreateStudentCommand(request.EnrollmentDate, request.Email, request.DateOfBirth, request.LastName, request.FirstName);
        var result = await services.Mediator.Send(new IdentifiedCommand<CreateStudentCommand, bool>(createStudentCommand, requestId));

        if (!result)
        {
            services.Logger.LogWarning("Failed to create student - RequestId: {RequestId}", requestId);
            return TypedResults.BadRequest("Failed to create student.");
        }

        services.Logger.LogInformation("Student created successfully - RequestId: {RequestId}", requestId);
        return TypedResults.Ok();
    }

    public static async Task<Results<Ok, NotFound, BadRequest<string>>> UpdateStudentAsync(
        [FromBody] UpdateStudentRequest request,
        [FromHeader(Name = "x-requestid")] Guid requestId, int studentId, [FromServices] StudentServices services)
    {
        var existingStudent = await services.Queries.GetStudentByIdAsync(studentId.ToString());
        if (existingStudent == null)
        {
            services.Logger.LogWarning("Course with ID {studentId} not found for update", studentId);
            return TypedResults.NotFound();
        }
        var command = new UpdateStudentCommand(request.EnrollmentDate, request.Email, request.DateOfBirth,
            request.LastName, request.FirstName);
        var success = await services.Mediator.Send(new IdentifiedCommand<UpdateStudentCommand, bool>(command, requestId));
        if (!success)
        {
            services.Logger.LogWarning("Failed to update student with ID {StudentId}", studentId);
            return TypedResults.BadRequest("Failed to update student.");
        }

        services.Logger.LogInformation("Student with ID {StudentId} updated successfully", studentId);
        return TypedResults.Ok();
    }

    public static async Task<Results<Ok, NotFound>> DeleteStudentAsync(int studentId,
        [FromHeader(Name = "x-requestid")] Guid requestId, [FromServices] StudentServices services)
    {
        var command = new DeleteStudentCommand(studentId);
        var success = await services.Mediator.Send(new IdentifiedCommand<DeleteStudentCommand ,bool>(command, requestId));
        if (!success)
        {
            services.Logger.LogWarning("Failed to delete student with ID {requestId}", requestId);
            return TypedResults.NotFound();
        }

        services.Logger.LogInformation("Student with ID {requestId} deleted successfully", requestId);
        return TypedResults.Ok();
    }
}
