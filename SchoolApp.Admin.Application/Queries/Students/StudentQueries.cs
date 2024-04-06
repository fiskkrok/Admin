
using Microsoft.EntityFrameworkCore;


using SchoolApp.Admin.Infrastructure.Data;

namespace SchoolApp.Admin.Application.Queries.Students;
public class StudentQueries(AdminDbContext context) : IStudentQueries
{
public async Task<IEnumerable<Student>> GetAllStudentsAsync()
{
    return await context.Students
        .Select(s => new Student
        {
            StudentId = s.StudentId,
            FirstName = s.FirstName,
            LastName = s.LastName,
            Email = s.Email,
            PhoneNumber = s.PhoneNumber,
            DateOfBirth = s.DateOfBirth.HasValue ? s.DateOfBirth.Value.ToDateTime(TimeOnly.MinValue, DateTimeKind.Utc) : default(DateTime),
            Address = new Address
            {
                Street = s.Address.Street,
                City = s.Address.City,
                State = s.Address.State,
                ZipCode = s.Address.ZipCode
            }
        }).ToListAsync();
}

public async Task<Student> GetStudentByIdAsync(string studentId)
{
    var student = await context.Students
        .Where(s => s.StudentId == studentId)
        .Select(s => new Student
        {
            StudentId = s.StudentId,
            FirstName = s.FirstName,
            LastName = s.LastName,
            Email = s.Email,
            PhoneNumber = s.PhoneNumber,
            DateOfBirth = s.DateOfBirth.HasValue ? s.DateOfBirth.Value.ToDateTime(TimeOnly.MinValue, DateTimeKind.Utc) : default(DateTime),
            Address = new Address
            {
                Street = s.Address.Street,
                City = s.Address.City,
                State = s.Address.State,
                ZipCode = s.Address.ZipCode
            }
        }).FirstOrDefaultAsync();

    return student ?? throw new KeyNotFoundException($"No student found with ID {studentId}");
}

public async Task<IEnumerable<Student>> GetStudentsByStatusAsync(StudentStatus status)
{
    return await context.Students
        .Where(s => (int)s.StudentStatus == (int)status)
        .Select(s => new Student
        {
            StudentId = s.StudentId,
            FirstName = s.FirstName,
            LastName = s.LastName,
            Email = s.Email,
            PhoneNumber = s.PhoneNumber,
            DateOfBirth = s.DateOfBirth.HasValue ? s.DateOfBirth.Value.ToDateTime(TimeOnly.MinValue, DateTimeKind.Utc) : default(DateTime),
            Address = new Address
            {
                Street = s.Address.Street,
                City = s.Address.City,
                State = s.Address.State,
                ZipCode = s.Address.ZipCode
            }
        }).ToListAsync();
}
}
