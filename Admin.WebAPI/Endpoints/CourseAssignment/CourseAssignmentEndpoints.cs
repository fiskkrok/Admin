using Admin.Application.Exceptions;
using Admin.Application.SeedWork;

using AutoMapper;

using Microsoft.AspNetCore.Http.HttpResults;

namespace Admin.WebAPI.Endpoints.CourseAssignment;

public static class CourseAssignmentEndpoints
{
    public static IEndpointRouteBuilder MapCourseAssignmentEndpoints(this IEndpointRouteBuilder app)
    {
        // Routes for querying course assignments
        app.MapGet("/api/CourseAssignment", GetAllCourseAssignments);
        app.MapGet("/api/CourseAssignment/{id}", GetCourseAssignmentById);

        // Routes for modifying course assignments
        app.MapPut("/api/CourseAssignment/{id}", UpdateCourseAssignment);
        app.MapPost("/api/CourseAssignment", CreateCourseAssignment);
        app.MapDelete("/api/CourseAssignment/{id}", DeleteCourseAssignment);

        return app;
    }

    public static async Task<Ok<ListCourseAssignmentsResponse>> GetAllCourseAssignments(IRepository<Application.AggregateModels.CourseAssignmentAggregate.CourseAssignment> courseAssignmentRepository, IMapper mapper)
    {
        var response = new ListCourseAssignmentsResponse();
        var items = await courseAssignmentRepository.ListAsync();
        response.CourseAssignments.AddRange(items.Select(mapper.Map<CourseAssignmentRecord>));
        return TypedResults.Ok(response);
    }

    public static async Task<Results<Ok<Application.AggregateModels.CourseAssignmentAggregate.CourseAssignment>, NotFound>> GetCourseAssignmentById(int assignmentid, IRepository<Application.AggregateModels.CourseAssignmentAggregate.CourseAssignment> courseAssignmentRepository)
    {
        var courseAssignment = await courseAssignmentRepository.GetByIdAsync(assignmentid);
        return courseAssignment != null ? TypedResults.Ok(courseAssignment) : TypedResults.NotFound();
    }

    public static async Task<Results<Ok, NotFound>> UpdateCourseAssignment(int assignmentid, Application.AggregateModels.CourseAssignmentAggregate.CourseAssignment courseAssignment, IRepository<Application.AggregateModels.CourseAssignmentAggregate.CourseAssignment> courseAssignmentRepository)
    {
        var existingCourseAssignment = await courseAssignmentRepository.GetByIdAsync(assignmentid);
        if (existingCourseAssignment == null)
        {
            return TypedResults.NotFound();
        }

        existingCourseAssignment.FacultyId = courseAssignment.FacultyId;
        existingCourseAssignment.CourseId = courseAssignment.CourseId;
        existingCourseAssignment.AssignmentType = courseAssignment.AssignmentType;

        await courseAssignmentRepository.UpdateAsync(existingCourseAssignment);
        return TypedResults.Ok();
    }

    public static async Task<IResult> CreateCourseAssignment(CreateCourseAssignmentRequest request, IRepository<Application.AggregateModels.CourseAssignmentAggregate.CourseAssignment> courseAssignmentRepository)
    {
        var response = new CreateCourseAssignmentResponse(request.CorrelationId());

        var newCourseAssignment = new Application.AggregateModels.CourseAssignmentAggregate.CourseAssignment(
            request.AssignmentId,
            request.FacultyId,
            request.CourseId,
            request.AssignmentType);
        newCourseAssignment = await courseAssignmentRepository.AddAsync(newCourseAssignment);

        var dto = new CourseAssignmentRecord
        (
            AssignmentId: newCourseAssignment.AssignmentId,
            FacultyId: newCourseAssignment.FacultyId,
            CourseId: newCourseAssignment.CourseId,
            AssignmentType: newCourseAssignment.AssignmentType
        );
        response.CourseAssignment = dto;
        return Results.Created($"api/courseassignment/{dto.AssignmentId}", response);
    }

    public static async Task<Results<Ok, NotFound>> DeleteCourseAssignment(int assignmentid, IRepository<Application.AggregateModels.CourseAssignmentAggregate.CourseAssignment> courseAssignmentRepository)
    {
        var courseAssignment = await courseAssignmentRepository.GetByIdAsync(assignmentid);
        if (courseAssignment == null)
        {
            return TypedResults.NotFound();
        }

        await courseAssignmentRepository.DeleteAsync(courseAssignment);
        return TypedResults.Ok();
    }
}