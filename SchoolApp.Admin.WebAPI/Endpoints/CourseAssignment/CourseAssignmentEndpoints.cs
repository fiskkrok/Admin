using Admin.Application.Commands;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http.HttpResults;
using SchoolApp.Admin.Application.Commands.CourseAssignment;
using SchoolApp.EventBus.Extensions;
using Azure.Core;

namespace SchoolApp.Admin.WebAPI.Endpoints.CourseAssignment;
public static class CourseAssignmentEndpoints
{
    public static IEndpointRouteBuilder MapCourseAssignmentEndpoints(this IEndpointRouteBuilder app)
    {
        // Define endpoints for course assignment operations
        app.MapGet("/CourseAssignment/", GetAllCourseAssignmentsAsync);
        app.MapGet("/CourseAssignment/{assignmentId:int}", GetCourseAssignmentByIdAsync);
        app.MapPost("/CourseAssignment/", CreateCourseAssignmentAsync);
        app.MapPut("/CourseAssignment/{assignmentId:int}", UpdateCourseAssignmentAsync);
        app.MapDelete("/CourseAssignment/{assignmentId:int}", DeleteCourseAssignmentAsync);

        return app;
    }

    public static async Task<Results<Ok<ListCourseAssignmentsResponse>, NotFound>> GetAllCourseAssignmentsAsync([AsParameters] CourseAssignmentServices services)
    {
        services.Logger.LogInformation("Fetching all course assignments");
        var response = new ListCourseAssignmentsResponse
        {
            CourseAssignments = await services.Queries.GetAllCourseAssignmentAsync()
        };

        if (response.CourseAssignments.Any())
            return TypedResults.Ok(response);

        services.Logger.LogWarning("No course assignments found");

        return TypedResults.NotFound();

    }

    public static async Task<Results<Ok<GetByIdCourseAssignmentResponse>, NotFound<string>>> GetCourseAssignmentByIdAsync(int assignmentId, [AsParameters] CourseAssignmentServices services)
    {
        services.Logger.LogInformation("Fetching course assignment with ID {assignmentId}", assignmentId);
        var response = new GetByIdCourseAssignmentResponse
        {
            CourseAssignment = await services.Queries.GetCourseAssignmentByIdAsync(assignmentId)
        };
        
        if (response != null) return TypedResults.Ok(response);

        services.Logger.LogWarning("Course assignment with ID {assignmentId} not found", assignmentId);
        return TypedResults.NotFound("Course assignment with ID not found");
    }

    public static async Task<Results<Ok, BadRequest<string>>> CreateCourseAssignmentAsync(
        CreateCourseAssignmentRequest request, [FromHeader(Name = "x-requestid")] Guid requestId,
        [AsParameters] CourseAssignmentServices services)
    {
        services.Logger.LogInformation(
            "Creating course assignment with Request ID: {RequestId}", requestId);

        if (requestId == Guid.Empty)
        {
            services.Logger.LogWarning("Empty GUID provided as Request ID");
            return TypedResults.BadRequest("Empty GUID is not valid for request ID");
        }

        var createCourseAssignmentCommand = new CreateCourseAssignmentCommand();

        bool result = await services.Mediator.Send(createCourseAssignmentCommand);

        if (result)
        {
            services.Logger.LogInformation("CreateCourseAssignmentCommand succeeded - RequestId: {RequestId}", requestId);
            return TypedResults.Ok();
        }
        else
        {
            services.Logger.LogWarning("CreateCourseAssignmentCommand failed - RequestId: {RequestId}", requestId);
            return TypedResults.BadRequest("Failed to create course assignment.");
        }
    }

    public static async Task<Results<Ok, NotFound, BadRequest<string>>> UpdateCourseAssignmentAsync(
        int assignmentId, UpdateCourseAssignmentRequest request,
        [AsParameters] CourseAssignmentServices services, [FromHeader(Name = "x-requestid")] Guid requestId)
    {
        services.Logger.LogInformation("Updating course assignment with ID {assignmentId}", assignmentId);

        bool success = await services.Mediator.Send(new IdentifiedCommand<UpdateCourseAssignmentRequest, bool>(request, requestId));

        if (!success)
        {
            services.Logger.LogWarning("Failed to update course assignment with ID {assignmentId}", assignmentId);
            return TypedResults.BadRequest("Failed to update course assignment.");
        }

        services.Logger.LogInformation("Course assignment with ID {assignmentId} updated successfully", assignmentId);
        return TypedResults.Ok();
    }

    public static async Task<Results<Ok, NotFound>> DeleteCourseAssignmentAsync(
        int assignmentId, DeleteCourseAssignmentRequest request,
        [AsParameters] CourseAssignmentServices services, [FromHeader(Name = "x-requestid")] Guid requestId)
    {
        services.Logger.LogInformation("Deleting course assignment with ID {assignmentId}", assignmentId);

        bool success = await services.Mediator.Send(new IdentifiedCommand<DeleteCourseAssignmentRequest, bool>(request, requestId));
        if (success)
        {
            services.Logger.LogInformation("Course assignment with ID {assignmentId} deleted successfully", assignmentId);
            return TypedResults.Ok();
        }
        else
        {
            services.Logger.LogWarning("Failed to delete course assignment with ID {assignmentId}", assignmentId);
            return TypedResults.NotFound();
        }
    }
}
