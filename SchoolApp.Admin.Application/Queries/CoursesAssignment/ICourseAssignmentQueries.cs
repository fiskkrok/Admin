using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SchoolApp.Admin.Application.Queries.CoursesAssignment;


namespace SchoolApp.Admin.Application.Queries.CourseAssignments;

public interface ICourseAssignmentQueries
{
    Task<IEnumerable<CourseAssignment>> GetAllAssignments();
    Task<CourseAssignment> GetAssignmentByIdAsync(string assignmentId);
    Task<IEnumerable<CourseAssignment>> GetAssignmentsByCourseIdAsync(string courseId);
    Task<IEnumerable<CourseAssignment>> GetAssignmentsByFacultyIdAsync(string facultyId);
}


