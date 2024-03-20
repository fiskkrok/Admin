using Admin.Web.Models;

namespace SchoolApp.Admin.Web.Services;

public class ListStudentsResponse
{
    public IEnumerable<Student> Students { get; set; } = new List<Student>();
}
