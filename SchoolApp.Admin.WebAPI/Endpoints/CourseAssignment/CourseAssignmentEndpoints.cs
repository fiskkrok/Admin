using Admin.Application.Commands;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http.HttpResults;
using SchoolApp.Admin.Application.Commands.CourseAssignment;

namespace SchoolApp.Admin.WebAPI.Endpoints.CourseAssignment;
public static class CourseAssignmentEndpoints
{
    public static RouteGroupBuilder MapCourseAssignmentEndpoints(this RouteGroupBuilder app)
    {
        // Define endpoints for course assignment operations
        app.MapGet("/CourseAssignment/", GetAllCourseAssignmentsAsync);
        app.MapGet("/CourseAssignment/{assignmentId:int}", GetCourseAssignmentByIdAsync);
        app.MapPost("/CourseAssignment/", CreateCourseAssignmentAsync);
        app.MapPut("/CourseAssignment/{assignmentId:int}", UpdateCourseAssignmentAsync);
        app.MapDelete("/CourseAssignment/{assignmentId:int}", DeleteCourseAssignmentAsync);

        return app;
    }

    public static async Task<Ok<IEnumerable<Application.Queries.CoursesAssignment.CourseAssignment>>> GetAllCourseAssignmentsAsync([AsParameters] CourseAssignmentServices services)
    {
        var courseAssignments = await services.Queries.GetAllAssignments();
            return TypedResults.Ok(courseAssignments);

    }

    public static async Task<Results<Ok<Application.Queries.CoursesAssignment.CourseAssignment>, NotFound>> GetCourseAssignmentByIdAsync(int assignmentId, [AsParameters] CourseAssignmentServices services)
    {
        try
        {
            var courseAssignment = await services.Queries.GetAssignmentByIdAsync(assignmentId.ToString());
            return TypedResults.Ok(courseAssignment);
        }
        catch
        {
            return TypedResults.NotFound();
        }
    }

    public static async Task<Results<Ok, BadRequest<string>>> CreateCourseAssignmentAsync(
        [FromHeader(Name = "x-requestid")] Guid requestId,
        [FromBody] CreateCourseAssignmentRequest request,
        [AsParameters] CourseAssignmentServices services)
    {
        services.Logger.LogInformation(
            "Creating course assignment with Request ID: {RequestId}", requestId);

        var command = new CreateCourseAssignmentCommand(request.AssignmentId, request.FacultyId, request.AssignmentType, request.CourseId);
        var identifiedCommand = new IdentifiedCommand<CreateCourseAssignmentCommand, bool>(command, requestId);
        var result = await services.Mediator.Send(identifiedCommand);

        if (result) return TypedResults.Ok();
        services.Logger.LogWarning("CreateCourseAssignmentCommand failed - RequestId: {RequestId}", requestId);
        return TypedResults.BadRequest("Failed to create course assignment.");
    }

    public static async Task<Results<Ok, NotFound, BadRequest<string>>> UpdateCourseAssignmentAsync(
        int assignmentId,
        [FromBody] UpdateCourseAssignmentRequest request,
        [AsParameters] CourseAssignmentServices services, [FromHeader(Name = "x-requestid")] Guid requestId)
    {
        services.Logger.LogInformation("Updating course assignment with ID {assignmentId}", assignmentId);
        var command = new UpdateCourseAssignmentCommand(request.AssignmentId, request.FacultyId, request.CourseId, request.AssignmentType);
        bool success = await services.Mediator.Send(new IdentifiedCommand<UpdateCourseAssignmentCommand, bool>(command, requestId));

        if (!success)
        {
            services.Logger.LogWarning("Failed to update course assignment with ID {assignmentId}", assignmentId);
            return TypedResults.BadRequest("Failed to update course assignment.");
        }

        services.Logger.LogInformation("Course assignment with ID {assignmentId} updated successfully", assignmentId);
        return TypedResults.Ok();
    }

    public static async Task<Results<Ok, NotFound>> DeleteCourseAssignmentAsync(
        int assignmentId,
        [AsParameters] CourseAssignmentServices services, [FromHeader(Name = "x-requestid")] Guid requestId)
    {
        services.Logger.LogInformation("Deleting course assignment with ID {assignmentId}", assignmentId);
        var command = new DeleteCourseAssignmentCommand(assignmentId);
        bool success = await services.Mediator.Send(new IdentifiedCommand<DeleteCourseAssignmentCommand, bool>(command, requestId));
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

