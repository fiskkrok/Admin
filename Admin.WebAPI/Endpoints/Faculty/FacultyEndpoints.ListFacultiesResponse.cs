using Microsoft.eShopWeb.PublicApi;

namespace Admin.WebAPI.Endpoints.Faculty;

public class ListFacultiesResponse : BaseResponse
{

    public ListFacultiesResponse(Guid correlationId) : base(correlationId)
    {
    }

    public ListFacultiesResponse()
    {
    }

    public List<FacultyRecord> Faculties { get; set; } = new List<FacultyRecord>();
}

