
using SchoolApp.Admin.Application.Queries.CoursesAssignment;

namespace SchoolApp.Admin.Application.Queries.Courses;
public record Course
{
    public string CourseId { get; init; }


    public string? CourseCode { get; set; }


    public string? CourseName { get; set; }

    public int Credits { get; set; }

    public string? Description { get; init; }

 
    public IEnumerable<CourseAssignment> CourseAssignments { get; }
    public IEnumerable<Enrollments.Enrollment> Enrollments { get; }
    public IEnumerable<Students.Student> Students { get; }
}
