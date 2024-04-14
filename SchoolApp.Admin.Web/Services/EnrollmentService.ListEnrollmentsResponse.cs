using SchoolApp.Admin.Web.Models;

namespace SchoolApp.Admin.Web.Services;

public class ListEnrollmentsResponse
{
    public IEnumerable<Enrollment> Enrollments { get; set; } = new List<Enrollment>();
}
