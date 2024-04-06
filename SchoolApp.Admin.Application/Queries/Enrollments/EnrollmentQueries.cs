using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;

using SchoolApp.Admin.Application.Queries.Faculties;
using SchoolApp.Admin.Infrastructure.Data;

namespace SchoolApp.Admin.Application.Queries.Enrollments;
public class EnrollmentQueries(AdminDbContext context) : IEnrollmentQueries
{
    public async Task<IEnumerable<Enrollment>> GetAllEnrollmentsAsync()
    {
        return await context.Enrollments.Select(e => new Enrollment
        {
            CourseId = e.CourseId,
            EnrollmentDate = e.EnrollmentDate,
            EnrollmentId = e.EnrollmentId,
            StudentId = e.StudentId
        }).ToListAsync();
    }

    public async Task<Enrollment> GetEnrollmentByIdAsync(string enrollmentId)
    {
        var enrollment = await context.Enrollments
            .FirstOrDefaultAsync(e => e.EnrollmentId == enrollmentId);
        if (enrollment == null) throw new KeyNotFoundException($"No enrollment found with ID {enrollmentId}");
        return new Enrollment
        {
            CourseId = enrollment.CourseId, EnrollmentDate = enrollment.EnrollmentDate,
            EnrollmentId = enrollment.EnrollmentId, StudentId = enrollment.StudentId
        };
    }

    public async Task<IEnumerable<Enrollment>> GetEnrollmentsByStudentIdAsync(string studentId)
    {
        return await context.Enrollments
            .Where(e => e.StudentId == studentId).Select(e => new Enrollment
            {
                CourseId = e.CourseId,
                EnrollmentDate = e.EnrollmentDate,
                EnrollmentId = e.EnrollmentId,
                StudentId = e.StudentId
            }).ToListAsync();
    }

    public async Task<IEnumerable<Enrollment>> GetEnrollmentsByCourseIdAsync(string courseId)
    {
        return await context.Enrollments
            .Where(e => e.CourseId == courseId).Select(e => new Enrollment
            {
                CourseId = e.CourseId,
                EnrollmentDate = e.EnrollmentDate,
                EnrollmentId = e.EnrollmentId,
                StudentId = e.StudentId
            }).ToListAsync();
    }

    public async Task<IEnumerable<Enrollment>> GetEnrollmentsByDateAsync(DateOnly enrollmentDate)
    {
        return await context.Enrollments
            .Where(e => e.EnrollmentDate == enrollmentDate).Select(e => new Enrollment
            {
                CourseId = e.CourseId,
                EnrollmentDate = e.EnrollmentDate,
                EnrollmentId = e.EnrollmentId,
                StudentId = e.StudentId
            }).ToListAsync();
    }
}
