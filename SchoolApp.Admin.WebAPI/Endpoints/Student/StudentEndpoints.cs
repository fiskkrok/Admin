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
    public static IEndpointRouteBuilder MapStudentEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapGet("/Student", GetAllStudentsAsync);
        app.MapGet("/Student/{studentId:int}", GetStudentByIdAsync);
        app.MapPost("/Student", CreateStudentAsync);
        app.MapPut("/Student/{studentId:int}", UpdateStudentAsync);
        app.MapDelete("/Student/{studentId:int}", DeleteStudentAsync);

        return app;
    }

    public static async Task<Results<Ok<ListStudentsResponse>, NotFound>> GetAllStudentsAsync([FromServices] StudentServices services)
    {
        var studentsResponse = new ListStudentsResponse
        {
            Students = (services.Queries.GetAllStudents() ?? []).ToList()
        };
        services.Logger.LogInformation("Retrieving all students");

        if (studentsResponse.Students.Count != 0) return TypedResults.Ok(studentsResponse);
        services.Logger.LogWarning("No students found");
        return TypedResults.NotFound();

    }

    public static async Task<Results<Ok<GetByIdStudentResponse>, NotFound<string>>> GetStudentByIdAsync(int studentId, [FromServices] StudentServices services)
    {
        services.Logger.LogInformation("Fetching student with ID {StudentId}", studentId);
        var studentResponse = new GetByIdStudentResponse
        {
            Student = await services.Queries.GetStudentByIdAsync(studentId)
        };

        return studentResponse.Student != null ? TypedResults.Ok(studentResponse) : TypedResults.NotFound<string>($"Student with ID {studentId} not found.");
    }

    public static async Task<Results<Ok, BadRequest<string>>> CreateStudentAsync(
        CreateStudentRequest request, [FromHeader(Name = "x-requestid")] Guid requestId, [FromServices] StudentServices services)
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
        [FromHeader(Name = "x-requestid")] Guid requestId,int studentId, UpdateStudentCommand request, [FromServices] StudentServices services)
    {
        var student = await services.Queries.GetStudentByIdAsync(studentId);
        if (student == null)
        {
            services.Logger.LogWarning("Student with ID {StudentId} not found", studentId);
            return TypedResults.NotFound();
        }

        var success = await services.Mediator.Send(new IdentifiedCommand<UpdateStudentCommand, bool>(request, requestId));
        if (!success)
        {
            services.Logger.LogWarning("Failed to update student with ID {StudentId}", studentId);
            return TypedResults.BadRequest("Failed to update student.");
        }

        services.Logger.LogInformation("Student with ID {StudentId} updated successfully", studentId);
        return TypedResults.Ok();
    }

    public static async Task<Results<Ok, NotFound>> DeleteStudentAsync(DeleteStudentCommand request,
        [FromHeader(Name = "x-requestid")] Guid requestId, [FromServices] StudentServices services)
    {
        var success = await services.Mediator.Send(new IdentifiedCommand<DeleteStudentCommand ,bool>(request, requestId));
        if (!success)
        {
            services.Logger.LogWarning("Failed to delete student with ID {requestId}", requestId);
            return TypedResults.NotFound();
        }

        services.Logger.LogInformation("Student with ID {requestId} deleted successfully", requestId);
        return TypedResults.Ok();
    }
}
