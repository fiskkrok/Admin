using Admin.Web.Models;

namespace SchoolApp.Admin.Web.Services;

public class ListCoursesResponse
{
    public IEnumerable<Course> Courses { get; set; } = new List<Course>();
}
