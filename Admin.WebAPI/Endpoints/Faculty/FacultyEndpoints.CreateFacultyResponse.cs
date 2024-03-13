using Microsoft.eShopWeb.PublicApi;

namespace Admin.WebAPI.Endpoints.Faculty;

public class CreateFacultyResponse: BaseResponse
{
    public CreateFacultyResponse(Guid correlationId) : base(correlationId)
    {
    }

    public CreateFacultyResponse()
    {
    }

    public FacultyRecord Faculty { get; set; }
}

