

using AggregateModel = SchoolApp.Admin.Application.AggregateModels.CourseAssignmentAggregate;
using AutoMapper;

using Microsoft.AspNetCore.Http.HttpResults;
using SchoolApp.Admin.Application.SeedWork;

namespace SchoolApp.Admin.WebAPI.Endpoints.CourseAssignment;

public static class CourseAssignmentEndpoints
{
    public static IEndpointRouteBuilder MapCourseAssignmentEndpoints(this IEndpointRouteBuilder app)
    {
        // Routes for querying course assignments
        app.MapGet("/CourseAssignmentt", GetAllCourseAssignments);
        app.MapGet("/CourseAssignmentt/{id}", GetCourseAssignmentById);

        // Routes for modifying course assignments
        app.MapPut("/CourseAssignmentt/{id}", UpdateCourseAssignment);
        app.MapPost("/CourseAssignmentt", CreateCourseAssignment);
        app.MapDelete("/CourseAssignmentt/{id}", DeleteCourseAssignment);

        return app;
    }

    public static async Task<Ok<ListCourseAssignmentsResponse>> GetAllCourseAssignments(IRepository<AggregateModel.CourseAssignment> courseAssignmentRepository, IMapper mapper)
    {
        var response = new ListCourseAssignmentsResponse();
        var items = await courseAssignmentRepository.ListAsync();
        response.CourseAssignments.AddRange(items.Select(mapper.Map<CourseAssignmentRecord>));
        return TypedResults.Ok(response);
    }

    public static async Task<Results<Ok<AggregateModel.CourseAssignment>, NotFound>> GetCourseAssignmentById(int assignmentid, IRepository<AggregateModel.CourseAssignment> courseAssignmentRepository)
    {
        var courseAssignment = await courseAssignmentRepository.GetByIdAsync(assignmentid);
        return courseAssignment != null ? TypedResults.Ok(courseAssignment) : TypedResults.NotFound();
    }

    public static async Task<Results<Ok, NotFound>> UpdateCourseAssignment(int assignmentid, AggregateModel.CourseAssignment courseAssignment, IRepository<AggregateModel.CourseAssignment> courseAssignmentRepository)
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

    public static async Task<IResult> CreateCourseAssignment(CreateCourseAssignmentRequest request, IRepository<AggregateModel.CourseAssignment> courseAssignmentRepository)
    {
        var response = new CreateCourseAssignmentResponse(request.CorrelationId());

        var newCourseAssignment = new AggregateModel.CourseAssignment(
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
        return Results.Created($"courseassignment/{dto.AssignmentId}", response);
    }

    public static async Task<Results<Ok, NotFound>> DeleteCourseAssignment(int assignmentid, IRepository<AggregateModel.CourseAssignment> courseAssignmentRepository)
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