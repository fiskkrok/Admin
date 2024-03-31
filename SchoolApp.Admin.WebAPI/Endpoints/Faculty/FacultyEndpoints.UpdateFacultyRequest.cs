using System.ComponentModel.DataAnnotations;

namespace SchoolApp.Admin.WebAPI.Endpoints.Faculty;

public class UpdateFacultyRequest : BaseRequest
{
    public int FacultyId { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Department { get; set; }
}