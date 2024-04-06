using Admin.Application.Commands;
using Microsoft.AspNetCore.Mvc;

using Microsoft.AspNetCore.Http.HttpResults;
using SchoolApp.Admin.Application.Commands.Course;
using SchoolApp.EventBus.Extensions;



namespace SchoolApp.Admin.WebAPI.Endpoints.Course;
public static class CourseEndpoints
{
    public static RouteGroupBuilder MapCourseEndpoints(this RouteGroupBuilder app)
    {
        app.MapGet("/Course/", GetAllCoursesAsync);
        app.MapGet("/Course/{courseId:int}", GetCourseByIdAsync);
        app.MapPost("/Course/", CreateCourseAsync);
        app.MapPut("/Course/{courseId:int}", UpdateCourseAsync);
        app.MapDelete("/Course/{courseId:int}", DeleteCourseAsync);

        return app;
    }

    public static async Task<Results<Ok<IEnumerable<Application.Queries.Courses.Course>>, NotFound>> GetAllCoursesAsync([AsParameters] CourseServices services)
    {
        try
        {
            var courses = await services.Queries.GetAllCourses();

        services.Logger.LogInformation("Fetching all courses");
        return TypedResults.Ok(courses);
        }
        catch
        {
            return TypedResults.NotFound();
        }

    }

    public static async Task<Results<Ok<Application.Queries.Courses.Course>, NotFound>> GetCourseByIdAsync(int courseId, [AsParameters] CourseServices services)
    {
        try
        {
            var order = await services.Queries.GetCourseByIdAsync(courseId);
            return TypedResults.Ok(order);
        }
        catch
        {
            return TypedResults.NotFound();
        }
    }


    public static async Task<Results<Ok, BadRequest<string>>> CreateCourseAsync(
        [FromHeader(Name = "x-requestid")] Guid requestId, CreateCourseRequest request,
        [AsParameters] CourseServices services)
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
            var createCourseCommand = new CreateCourseCommand(request.CourseCode, request.CourseName,
                request.Credits,
                request.Description, request.CourseId);
            var requestCreateCourse =
                new IdentifiedCommand<CreateCourseCommand, bool>(createCourseCommand, requestId);
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

    public static async Task<Results<Ok, NotFound, BadRequest<string>>> UpdateCourseAsync(
        [FromHeader(Name = "x-requestid")] Guid requestId,
        UpdateCourseRequest request,
        [AsParameters] CourseServices services)
    {
        services.Logger.LogInformation("Attempting to update course with ID {requestId}", requestId);
        var course = await services.Queries.GetCourseByIdAsync(request.CourseId);
        if (course == null)
        {
            services.Logger.LogWarning("Course with ID {requestId} not found for update", requestId);
            return TypedResults.NotFound();
        }

        var command = new UpdateCourseCommand(request.CourseCode, request.CourseName, request.Credits, request.Description, request.CourseId);
        var success = await services.Mediator.Send(new IdentifiedCommand<UpdateCourseCommand, bool>(command, requestId)); // Assuming new GUID as request ID for simplicity

        if (!success)
        {
            services.Logger.LogWarning("Failed to update course with ID {requestId}", requestId);
            return TypedResults.BadRequest("Failed to update course.");
        }

        services.Logger.LogInformation("Course with ID {requestId} updated successfully", requestId);
        return TypedResults.Ok();
    }

    public static async Task<Results<Ok, NotFound>> DeleteCourseAsync(
        [FromHeader(Name = "x-requestid")] Guid requestId,
       int courseId,
        [AsParameters] CourseServices services)
    {
        var command = new DeleteCourseCommand(courseId);
        services.Logger.LogInformation("Attempting to delete course with ID {requestId}");
        var success = await services.Mediator.Send(new IdentifiedCommand<DeleteCourseCommand, bool>(command, requestId));
        if (success)
        {
            services.Logger.LogInformation("Course with ID {requestId} deleted successfully", requestId);
            return TypedResults.Ok();
        }

        services.Logger.LogWarning("Failed to delete course with ID {requestId}", requestId);
        return TypedResults.NotFound();
    }
}

