using Admin.Application.Commands;
using Admin.Application.Exceptions;


using AutoMapper;

using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using SchoolApp.Admin.Application.Commands.Course;
using SchoolApp.Admin.Application.Commands.CourseAssignment;
using SchoolApp.Admin.Application.Commands.Faculty;
using SchoolApp.EventBus.Extensions;


namespace SchoolApp.Admin.WebAPI.Endpoints.Faculty;

public static class FacultyEndpoints
{
    public static IEndpointRouteBuilder MapFacultyEndpoints(this IEndpointRouteBuilder app)
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

    public static async Task<Results<Ok<ListFacultiesResponse>, NotFound>> GetAllFaculties(FacultyServices services,ListFacultiesRequest request)
    {

        services.Logger.LogInformation("Fetching all course assignments");
        var response = new ListFacultiesResponse
        {
            Faculties = [.. services.Queries.GetAllFacultiesAsync()]
        };

        if (response.Faculties is { Count: > 0 })
            return TypedResults.Ok(response);

        services.Logger.LogWarning("No course assignments found");

        return TypedResults.NotFound();
    }

    public static async Task<Results<Ok<GetByIdFacultyResponse>, NotFound>> GetFacultyById(FacultyServices services, Guid facultyId)
    {
    
        var response = new GetByIdFacultyResponse
        {
            Faculty = await services.Queries.GetFacultyByIdAsync(facultyId)
        };
        if (response.Faculty is null) return TypedResults.NotFound();

        return TypedResults.Ok(response);

    }

    public static async Task<Results<Ok, NotFound, BadRequest<string>>> UpdateFaculty([AsParameters] FacultyServices services, UpdateFacultyCommand command, 
        [FromHeader(Name = "x-requestid")] Guid requestId)
    {
        var existingFaculty = await services.Queries.GetFacultyByIdAsync(requestId);
        if (existingFaculty == null)
        {
            services.Logger.LogWarning("Course with ID {requestId} not found for update", requestId);
            return TypedResults.NotFound();
        }

        var success = await services.Mediator.Send(new IdentifiedCommand<UpdateFacultyCommand, bool>(command, requestId)); // Assuming new GUID as request ID for simplicity

        if (!success)
        {
            services.Logger.LogWarning("Failed to update course with ID {requestId}", requestId);
            return TypedResults.BadRequest("Failed to update course.");
        }

        services.Logger.LogInformation("Course with ID {requestId} updated successfully", requestId);
        return TypedResults.Ok();
    }

    public static async Task<IResult> CreateFaculty(CreateFacultyRequest request, [FromHeader(Name = "x-requestid")] Guid requestId,
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
        DeleteFacultyCommand request,
        [AsParameters] FacultyServices services)
    {

        services.Logger.LogInformation("Attempting to delete course with ID {requestId}");
        var success = await services.Mediator.Send(new IdentifiedCommand<DeleteFacultyCommand, bool>(request, requestId));
        if (success)
        {
            services.Logger.LogInformation("Course with ID {requestId} deleted successfully", requestId);
            return TypedResults.Ok();
        }

        services.Logger.LogWarning("Failed to delete course with ID {requestId}", requestId);
        return TypedResults.NotFound();
    }
}