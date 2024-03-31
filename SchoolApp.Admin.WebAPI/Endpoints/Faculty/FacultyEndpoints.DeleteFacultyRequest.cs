namespace SchoolApp.Admin.WebAPI.Endpoints.Faculty;

public class DeleteFacultyRequest : BaseRequest
{
    public int FacultyId { get; init; }

    public DeleteFacultyRequest(int facultyId)
    {
        FacultyId = facultyId;
    }
}