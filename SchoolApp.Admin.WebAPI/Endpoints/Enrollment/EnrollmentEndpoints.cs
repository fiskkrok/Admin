using Admin.Application.Exceptions;
using Admin.Application.SeedWork;

using AutoMapper;

using Microsoft.AspNetCore.Http.HttpResults;

namespace Admin.WebAPI.Endpoints.Enrollment;

public static class EnrollmentEndpoints
{
    public static IEndpointRouteBuilder MapEnrollmentEndpoints(this IEndpointRouteBuilder app)
    {
        // Routes for querying enrollments
        app.MapGet("/Enrollment", GetAllEnrollments);
        app.MapGet("/Enrollment/{id}", GetEnrollmentById);

        // Routes for modifying enrollments
        app.MapPut("/Enrollment/{id}", UpdateEnrollment);
        app.MapPost("/Enrollment", CreateEnrollment);
        app.MapDelete("/Enrollment/{id}", DeleteEnrollment);

        return app;
    }

    public static async Task<Ok<ListEnrollmentsResponse>> GetAllEnrollments(IRepository<Application.AggregateModels.EnrollmentAggregate.Enrollment> enrollmentRepository, IMapper mapper)
    {
        var response = new ListEnrollmentsResponse();
        var items = await enrollmentRepository.ListAsync();
        response.Enrollments.AddRange(items.Select(mapper.Map<EnrollmentRecord>));
        return TypedResults.Ok(response);
    }

    public static async Task<Results<Ok<Application.AggregateModels.EnrollmentAggregate.Enrollment>, NotFound>> GetEnrollmentById(int enrollmentid, IRepository<Application.AggregateModels.EnrollmentAggregate.Enrollment> enrollmentRepository)
    {
        var enrollment = await enrollmentRepository.GetByIdAsync(enrollmentid);
        return enrollment != null ? TypedResults.Ok(enrollment) : TypedResults.NotFound();
    }

    public static async Task<Results<Ok, NotFound>> UpdateEnrollment(int enrollmentid, Application.AggregateModels.EnrollmentAggregate.Enrollment enrollment, IRepository<Application.AggregateModels.EnrollmentAggregate.Enrollment> enrollmentRepository)
    {
        var existingEnrollment = await enrollmentRepository.GetByIdAsync(enrollmentid);
        if (existingEnrollment == null)
        {
            return TypedResults.NotFound();
        }

        existingEnrollment.StudentId = enrollment.StudentId;
        existingEnrollment.CourseId = enrollment.CourseId;
        existingEnrollment.EnrollmentDate = enrollment.EnrollmentDate;

        await enrollmentRepository.UpdateAsync(existingEnrollment);
        return TypedResults.Ok();
    }

    public static async Task<IResult> CreateEnrollment(CreateEnrollmentRequest request, IRepository<Application.AggregateModels.EnrollmentAggregate.Enrollment> enrollmentRepository)
    {
        var response = new CreateEnrollmentResponse(request.CorrelationId());

        var newEnrollment = new Application.AggregateModels.EnrollmentAggregate.Enrollment(
            request.EnrollmentId,
            request.StudentId,
            request.CourseId,
            request.EnrollmentDate);
        newEnrollment = await enrollmentRepository.AddAsync(newEnrollment);

        var dto = new EnrollmentRecord
        (
            EnrollmentId: newEnrollment.EnrollmentId,
            StudentId: newEnrollment.StudentId,
            CourseId: newEnrollment.CourseId,
            EnrollmentDate: newEnrollment.EnrollmentDate
        );
        response.Enrollment = dto;
        return Results.Created($"enrollment/{dto.EnrollmentId}", response);
    }

    public static async Task<Results<Ok, NotFound>> DeleteEnrollment(int enrollmentid, IRepository<Application.AggregateModels.EnrollmentAggregate.Enrollment> enrollmentRepository)
    {
        var enrollment = await enrollmentRepository.GetByIdAsync(enrollmentid);
        if (enrollment == null)
        {
            return TypedResults.NotFound();
        }

        await enrollmentRepository.DeleteAsync(enrollment);
        return TypedResults.Ok();
    }
}