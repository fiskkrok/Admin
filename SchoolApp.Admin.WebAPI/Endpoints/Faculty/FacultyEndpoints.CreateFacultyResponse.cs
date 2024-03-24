
using SchoolApp.Admin.WebAPI;

namespace SchoolApp.Admin.WebAPI.Endpoints.Faculty;

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

