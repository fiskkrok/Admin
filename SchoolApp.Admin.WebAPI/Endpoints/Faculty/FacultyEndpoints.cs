using Admin.Application.Commands;
using Admin.Application.Exceptions;


using AutoMapper;

using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using SchoolApp.Admin.Application.Commands.Course;
using SchoolApp.Admin.Application.Commands.CourseAssignment;
using SchoolApp.Admin.Application.Commands.Faculty;
using SchoolApp.EventBus.Extensions;

using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;


namespace SchoolApp.Admin.WebAPI.Endpoints.Faculty;

public static class FacultyEndpoints
{
    public static RouteGroupBuilder MapFacultyEndpoints(this RouteGroupBuilder app)
    {
        // Routes for querying faculties
        app.MapGet("/Faculty", GetAllFaculties);
        app.MapGet("/Faculty/{id}", GetFacultyById);

        // Routes for modifying faculties
        app.MapPut("/Faculty/{id}", UpdateFaculty);
        app.MapPost("/Faculty", CreateFaculty);
        app.MapDelete("/Faculty/{id}", DeleteFaculty);

        return app;
    }

    public static async Task<Ok<IEnumerable<Application.Queries.Faculties.Faculty>>> GetAllFaculties([AsParameters]FacultyServices services)
    {

        services.Logger.LogInformation("Fetching all course assignments");

        var faculties = await services.Queries.GetAllFacultiesAsync();
      

       
            return TypedResults.Ok(faculties);

      
    }

    public static async Task<Results<Ok<Application.Queries.Faculties.Faculty>, NotFound>> GetFacultyById([AsParameters] FacultyServices services, int facultyId)
    {


        var faculty = await services.Queries.GetFacultyByIdAsync(facultyId.ToString());

        return TypedResults.Ok(faculty);

    }

    public static async Task<Results<Ok, NotFound, BadRequest<string>>> UpdateFaculty([AsParameters] FacultyServices services,
        [FromBody] UpdateFacultyRequest request,
        [FromHeader(Name = "x-requestid")] Guid requestId, int facultyId)
    {
        var existingFaculty = await services.Queries.GetFacultyByIdAsync(facultyId.ToString());
        if (existingFaculty == null)
        {
            services.Logger.LogWarning("Course with ID {facultyId} not found for update", facultyId);
            return TypedResults.NotFound();
        }

        var command =
            new UpdateFacultyCommand(request.FirstName, request.LastName, request.Department, request.FacultyId);
        var success = await services.Mediator.Send(new IdentifiedCommand<UpdateFacultyCommand, bool>(command, requestId)); // Assuming new GUID as
                                                                                                                           // ID for simplicity

        if (!success)
        {
            services.Logger.LogWarning("Failed to update course with ID {requestId}", requestId);
            return TypedResults.BadRequest("Failed to update course.");
        }

        services.Logger.LogInformation("Course with ID {requestId} updated successfully", requestId);
        return TypedResults.Ok();
    }

    public static async Task<IResult> CreateFaculty([FromBody] CreateFacultyRequest request, [FromHeader(Name = "x-requestid")] Guid requestId,
        [AsParameters] FacultyServices services)
    {
        services.Logger.LogInformation(
            "Sending command: {CommandName} - {IdProperty}: {CommandId} ({@Command})",
            request.GetGenericTypeName(),
            nameof(request.CorrelationId),
            request.CorrelationId(),
            request);
        if (requestId == Guid.Empty)
        {
            services.Logger.LogWarning("Empty GUID provided as Request ID");
            return TypedResults.BadRequest("Empty GUID is not valid for request ID");
        }

        using (services.Logger.BeginScope(new List<KeyValuePair<string, object>>
                   { new("IdentifiedCommandId", requestId) }))
        {
            var createFacultyCommand = new CreateFacultyCommand(request.Department,
                request.FirstName,
                request.LastName);
            var requestCreateCourse =
                new IdentifiedCommand<CreateFacultyCommand, bool>(createFacultyCommand, requestId);
            services.Logger.LogInformation(
                "Sending command: {CommandName} - {IdProperty}: {CommandId} ({@Command})",
                requestCreateCourse.GetGenericTypeName(),
                nameof(requestCreateCourse.Id),
                requestCreateCourse.Id,
                requestCreateCourse);
            var result = await services.Mediator.Send(requestCreateCourse);

            if (result)
            {
                services.Logger.LogInformation("CreateCourseCommand succeeded - RequestId: {RequestId}", requestId);
            }
            else
            {
                services.Logger.LogWarning("CreateCourseCommand failed - RequestId: {RequestId}", requestId);
            }

            return TypedResults.Ok();

        }
    }

    public static async Task<Results<Ok, NotFound>> DeleteFaculty([FromHeader(Name = "x-requestid")] Guid requestId,
        int facultyId,
        [AsParameters] FacultyServices services)
    {
        var command = new DeleteFacultyCommand(facultyId);
        services.Logger.LogInformation("Attempting to delete course with ID {requestId}");
        var success = await services.Mediator.Send(new IdentifiedCommand<DeleteFacultyCommand, bool>(command, requestId));
        if (success)
        {
            services.Logger.LogInformation("Course with ID {requestId} deleted successfully", requestId);
            return TypedResults.Ok();
        }

        services.Logger.LogWarning("Failed to delete course with ID {requestId}", requestId);
        return TypedResults.NotFound();
    }
}