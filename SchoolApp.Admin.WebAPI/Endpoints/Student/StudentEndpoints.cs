

using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;
using SchoolApp.Admin.Application.SeedWork;

namespace SchoolApp.Admin.WebAPI.Endpoints.Student;

public static class StudentEndpoints
{
    public static IEndpointRouteBuilder MapStudentEndpoints(this IEndpointRouteBuilder app)
    {
        // Routes for querying students
        
        app.MapGet("/Student", GetAllStudents).AllowAnonymous();
        app.MapGet("/Student/{id}", GetStudentById);

        // Routes for modifying students
        app.MapPut("/Student/{id}", UpdateStudent);
        app.MapPost("/Student", CreateStudent);
        app.MapDelete("/Student/{id}", DeleteStudent);

        return app;
    }

    private static async Task<Ok<ListStudentsResponse>> GetAllStudents(IRepository<Application.AggregateModels.StudentAggregate.Student> studentRepository, IMapper mapper)
    {
        var response = new ListStudentsResponse();
        var items = await studentRepository.ListAsync();
        response.Students.AddRange(items.Select(mapper.Map<StudentRecord>));
        return TypedResults.Ok(response);
    }

    private static async Task<Results<Ok<Application.AggregateModels.StudentAggregate.Student>, NotFound>> GetStudentById(int studentid, IRepository<Application.AggregateModels.StudentAggregate.Student> studentRepository)
    {
        var student = await studentRepository.GetByIdAsync(studentid);
        return student != null ? TypedResults.Ok(student) : TypedResults.NotFound();
    }

    private static async Task<Results<Ok, NotFound>> UpdateStudent(int studentid, Application.AggregateModels.StudentAggregate.Student student, IRepository<Application.AggregateModels.StudentAggregate.Student> studentRepository)
    {
        var existingStudent = await studentRepository.GetByIdAsync(studentid);
        if (existingStudent == null)
        {
            return TypedResults.NotFound();
        }

        existingStudent.FirstName = student.FirstName;
        existingStudent.LastName = student.LastName;
        existingStudent.DateOfBirth = student.DateOfBirth;
        existingStudent.Email = student.Email;
        existingStudent.EnrollmentDate = student.EnrollmentDate;

        await studentRepository.UpdateAsync(existingStudent);
        return TypedResults.Ok();
    }

    private static async Task<IResult> CreateStudent(CreateStudentRequest request, IRepository<Application.AggregateModels.StudentAggregate.Student> studentRepository)
    {
        var response = new CreateStudentResponse(request.CorrelationId());

        var newStudent = new Application.AggregateModels.StudentAggregate.Student(
            request.StudentId,
            request.FirstName,
            request.LastName,
            request.DateOfBirth,
            request.Email,
            request.Address);
        newStudent = await studentRepository.AddAsync(newStudent);

        var dto = new StudentRecord
        (
            StudentId: newStudent.StudentId,
            FirstName: newStudent.FirstName ?? "noName",
            LastName: newStudent.LastName ?? "noName",
            DateOfBirth: newStudent.DateOfBirth,
            Email: newStudent.Email,
            EnrollmentDate: newStudent.EnrollmentDate
        );
        response.Student = dto;
        return Results.Created($"student/{dto.StudentId}", response);
    }

    private static async Task<Results<Ok, NotFound>> DeleteStudent(int studentid, IRepository<Application.AggregateModels.StudentAggregate.Student> studentRepository)
    {
        var student = await studentRepository.GetByIdAsync(studentid);
        if (student == null)
        {
            return TypedResults.NotFound();
        }

        await studentRepository.DeleteAsync(student);
        return TypedResults.Ok();
    }
}