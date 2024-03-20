using Admin.Web.Models;

namespace SchoolApp.Admin.Web.Services;

public class ListAuthResponse
{
    public IEnumerable<Faculty> Faculties { get; set; } = new List<Faculty>();
}
