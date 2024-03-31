

using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;
using SchoolApp.Admin.Application.Commands.Enrollment;
using SchoolApp.Admin.Domain.SeedWork;

namespace SchoolApp.Admin.WebAPI.Endpoints.Enrollment;

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

    public static async Task<Ok<ListEnrollmentsResponse>> GetAllEnrollments( IMapper mapper)
    {
        var response = new ListEnrollmentsResponse();
        //var items = await enrollmentRepository.ListAsync();
        //response.Enrollments.AddRange(items.Select(mapper.Map<EnrollmentRecord>));
        return TypedResults.Ok(response);
    }

    public static async Task<Results<Ok<GetByIdEnrollmentResponse>, NotFound>> GetEnrollmentById(int enrollmentid)
    {
        var respone = new GetByIdEnrollmentResponse();
        //var enrollment = await enrollmentRepository.GetByIdAsync(enrollmentid);
        //return enrollment != null ? TypedResults.Ok(enrollment) : TypedResults.NotFound();
        return TypedResults.Ok(respone);
    }

    public static async Task<Results<Ok, NotFound>> UpdateEnrollment(int enrollmentid)
    {
        var existingEnrollment = string.Empty;
        if (existingEnrollment == null)
        {
            return TypedResults.NotFound();
        }

        return TypedResults.Ok();
    }

    public static async Task<IResult> CreateEnrollment(CreateEnrollmentRequest request)
    {
        var response = new CreateEnrollmentResponse(request.CorrelationId());

       
        return Results.Created($"enrollment/", response);
    }

    public static async Task<Results<Ok, NotFound>> DeleteEnrollment(int enrollmentid)
    {
        var enrollment = string.Empty;
        if (enrollment == null)
        {
            return TypedResults.NotFound();
        }
        return TypedResults.Ok();
    }
}