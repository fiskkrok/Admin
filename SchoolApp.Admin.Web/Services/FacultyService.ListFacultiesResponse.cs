using SchoolApp.Admin.Web.Models;

namespace SchoolApp.Admin.Web.Services;

public class ListFacultiesResponse
{
    public IEnumerable<Faculty> Faculties { get; set; } = new List<Faculty>();
}
