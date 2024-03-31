using SchoolApp.Admin.Application.Commands.Faculty;
using SchoolApp.Admin.WebAPI;

namespace SchoolApp.Admin.WebAPI.Endpoints.Faculty;

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

