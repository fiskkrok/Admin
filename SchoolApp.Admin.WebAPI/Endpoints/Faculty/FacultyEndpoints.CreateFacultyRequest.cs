using Microsoft.eShopWeb.PublicApi;

namespace Admin.WebAPI.Endpoints.Faculty;

public class CreateFacultyRequest : BaseRequest
{
    public int FacultyId { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Department { get; set; }
}
