using SchoolApp.Admin.Web.Models;

namespace SchoolApp.Admin.Web.Services;

public class ListCourseAssignmentsResponse
{
    public IEnumerable<CourseAssignment> CourseAssignments { get; set; } = new List<CourseAssignment>();
}
