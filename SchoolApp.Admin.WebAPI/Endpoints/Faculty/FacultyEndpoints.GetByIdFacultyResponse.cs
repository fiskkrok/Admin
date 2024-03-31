using SchoolApp.Admin.Application.Commands.Enrollment;
using SchoolApp.Admin.Application.Commands.Faculty;

namespace SchoolApp.Admin.WebAPI.Endpoints.Faculty;

public class GetByIdFacultyResponse : BaseResponse
{
    public GetByIdFacultyResponse(Guid correlationId) : base(correlationId)
    {
    }

    public GetByIdFacultyResponse()
    {
    }

    public FacultyRecord? Faculty { get; init; }
}