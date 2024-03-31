using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SchoolApp.Admin.Infrastructure.Data;

namespace SchoolApp.Admin.Application.Queries.Student;
public class StudentQueries(AdminDbContext context, IMapper mapper) : IStudentQueries
{
    public async Task<StudentRecord> GetStudentByIdAsync(int studentId)
    {
        var student = await context.Students
            .Include(o => o.Enrollments)
            .FirstOrDefaultAsync(o => o.Id == studentId);

        return student is null
            ? throw new KeyNotFoundException()
            : new StudentRecord(student.Id, student.FirstName, student.LastName,student.DateOfBirth,student.Email,student.EnrollmentDate);
    }

    public IEnumerable<StudentRecord>? GetAllStudents()
    {
        var students = context.Students.AsQueryable();
        return mapper.ProjectTo<StudentRecord>(students);
    }

  
}
