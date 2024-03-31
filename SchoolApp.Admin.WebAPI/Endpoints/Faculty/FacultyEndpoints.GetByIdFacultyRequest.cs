namespace SchoolApp.Admin.WebAPI.Endpoints.Faculty;

public class GetByIdFacultyRequest : BaseRequest
{
    public int FacultyId { get; init; }

    public GetByIdFacultyRequest(int facultyId)
    {
        FacultyId = facultyId;
    }
}