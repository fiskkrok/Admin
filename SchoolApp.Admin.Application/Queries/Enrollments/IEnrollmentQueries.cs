using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolApp.Admin.Application.Queries.Enrollments;

public interface IEnrollmentQueries
{
    Task<IEnumerable<Enrollment>> GetAllEnrollmentsAsync();
    Task<Enrollment> GetEnrollmentByIdAsync(string enrollmentId);
    Task<IEnumerable<Enrollment>> GetEnrollmentsByStudentIdAsync(string studentId);
    Task<IEnumerable<Enrollment>> GetEnrollmentsByCourseIdAsync(string courseId);
    Task<IEnumerable<Enrollment>> GetEnrollmentsByDateAsync(DateOnly enrollmentDate);
}
