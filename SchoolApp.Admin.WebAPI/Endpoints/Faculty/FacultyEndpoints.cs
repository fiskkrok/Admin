using Admin.Application.Exceptions;


using AutoMapper;

using Microsoft.AspNetCore.Http.HttpResults;
using SchoolApp.Admin.Application.SeedWork;

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

    public static async Task<Ok<ListFacultiesResponse>> GetAllFaculties(IRepository<Application.AggregateModels.FacultyAggregate.Faculty> facultyRepository, IMapper mapper)
    {
        var response = new ListFacultiesResponse();
        var items = await facultyRepository.ListAsync();
        response.Faculties.AddRange(items.Select(mapper.Map<FacultyRecord>));
        return TypedResults.Ok(response);
    }

    public static async Task<Results<Ok<Application.AggregateModels.FacultyAggregate.Faculty>, NotFound>> GetFacultyById(int facultyid, IRepository<Application.AggregateModels.FacultyAggregate.Faculty> facultyRepository)
    {
        var faculty = await facultyRepository.GetByIdAsync(facultyid);
        return faculty != null ? TypedResults.Ok(faculty) : TypedResults.NotFound();
    }

    public static async Task<Results<Ok, NotFound>> UpdateFaculty(int facultyid, Application.AggregateModels.FacultyAggregate.Faculty faculty, IRepository<Application.AggregateModels.FacultyAggregate.Faculty> facultyRepository)
    {
        var existingFaculty = await facultyRepository.GetByIdAsync(facultyid);
        if (existingFaculty == null)
        {
            return TypedResults.NotFound();
        }

        existingFaculty.FirstName = faculty.FirstName;
        existingFaculty.LastName = faculty.LastName;
        existingFaculty.Department = faculty.Department;

        await facultyRepository.UpdateAsync(existingFaculty);
        return TypedResults.Ok();
    }

    public static async Task<IResult> CreateFaculty(CreateFacultyRequest request, IRepository<Application.AggregateModels.FacultyAggregate.Faculty> facultyRepository)
    {
        var response = new CreateFacultyResponse(request.CorrelationId());

        var newFaculty = new Application.AggregateModels.FacultyAggregate.Faculty(
            request.FacultyId,
            request.FirstName,
            request.LastName,
            request.Department);
        newFaculty = await facultyRepository.AddAsync(newFaculty);

        var dto = new FacultyRecord
        (
            FacultyId: newFaculty.FacultyId,
            FirstName: newFaculty.FirstName,
            LastName: newFaculty.LastName,
            Department: newFaculty.Department
        );
        response.Faculty = dto;
        return Results.Created($"faculty/{dto.FacultyId}", response);
    }

    public static async Task<Results<Ok, NotFound>> DeleteFaculty(int facultyid, IRepository<Application.AggregateModels.FacultyAggregate.Faculty> facultyRepository)
    {
        var faculty = await facultyRepository.GetByIdAsync(facultyid);
        if (faculty == null)
        {
            return TypedResults.NotFound();
        }

        await facultyRepository.DeleteAsync(faculty);
        return TypedResults.Ok();
    }
}