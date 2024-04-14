

using Admin.Application.Commands;
using Admin.WebAPI;
using AutoMapper;

using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

using SchoolApp.Admin.Application.Commands.Enrollment;
using SchoolApp.Admin.Domain.SeedWork;

namespace SchoolApp.Admin.WebAPI.Endpoints.Enrollment;

public static class EnrollmentEndpoints
{
    public static RouteGroupBuilder MapEnrollmentEndpoints(this RouteGroupBuilder app)
    {
        // Define endpoints for enrollment operations
        app.MapGet("/Enrollment", GetAllEnrollmentsAsync);
        app.MapGet("/Enrollment/{id}", GetEnrollmentByIdAsync);
        app.MapPut("/Enrollment/{id}", UpdateEnrollmentAsync);
        app.MapPost("/Enrollment", CreateEnrollmentAsync);
        app.MapDelete("/Enrollment/{id}", DeleteEnrollmentAsync);

        return app;
    }

    public static async Task<Ok<IEnumerable<Application.Queries.Enrollments.Enrollment>>> GetAllEnrollmentsAsync([AsParameters] EnrollmentServices services)
    {
        var enrollments = await services.Queries.GetAllEnrollmentsAsync();
        return TypedResults.Ok(enrollments);
    }

    public static async Task<Results<Ok<Application.Queries.Enrollments.Enrollment>, NotFound>> GetEnrollmentByIdAsync(int id, [AsParameters] EnrollmentServices services)
    {
        try
        {
            var enrollment = await services.Queries.GetEnrollmentByIdAsync(id.ToString());
            return TypedResults.Ok(enrollment);
        }
        catch
        {
            return TypedResults.NotFound();
        }
    }

    public static async Task<Results<Ok, NotFound, BadRequest<string>>> UpdateEnrollmentAsync(int id, [FromBody] UpdateEnrollmentRequest request, [AsParameters] EnrollmentServices services)
    {
        services.Logger.LogInformation("Updating enrollment with ID {id}", id);
        var command = new UpdateEnrollmentCommand(request.EnrollmentId, request.StudentId, request.CourseId, request.EnrollmentDate);
        bool success = await services.Mediator.Send(new IdentifiedCommand<UpdateEnrollmentCommand, bool>(command, request.CorrelationId()));

        if (!success)
        {
            services.Logger.LogWarning("Failed to update enrollment with ID {id}", id);
            return TypedResults.BadRequest("Failed to update enrollment.");
        }

        services.Logger.LogInformation("Enrollment with ID {id} updated successfully", id);
        return TypedResults.Ok();
    }

    public static async Task<Results<Ok, BadRequest<string>>> CreateEnrollmentAsync([FromHeader(Name = "x-requestid")] Guid requestId, [FromBody] CreateEnrollmentRequest request, [AsParameters] EnrollmentServices services)
    {
        services.Logger.LogInformation("Creating enrollment with Request ID: {RequestId}", requestId);

        var command = new CreateEnrollmentCommand(request.EnrollmentId, request.StudentId, request.CourseId);
        var identifiedCommand = new IdentifiedCommand<CreateEnrollmentCommand, bool>(command, requestId);
        var result = await services.Mediator.Send(identifiedCommand);

        if (result) return TypedResults.Ok();
        services.Logger.LogWarning("CreateEnrollmentCommand failed - RequestId: {RequestId}", requestId);
        return TypedResults.BadRequest("Failed to create enrollment.");
    }

    public static async Task<Results<Ok, NotFound>> DeleteEnrollmentAsync(int id, [AsParameters] EnrollmentServices services, [FromHeader(Name = "x-requestid")] Guid requestId)
    {
        services.Logger.LogInformation("Deleting enrollment with ID {id}", id);
        var command = new CancelEnrollmentCommand();
        bool success = await services.Mediator.Send(new IdentifiedCommand<CancelEnrollmentCommand, bool>(command, requestId));
        if (success)
        {
            services.Logger.LogInformation("Enrollment with ID {id} deleted successfully", id);
            return TypedResults.Ok();
        }
        else
        {
            services.Logger.LogWarning("Failed to delete enrollment with ID {id}", id);
            return TypedResults.NotFound();
        }
    }
}
