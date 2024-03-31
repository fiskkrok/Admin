namespace SchoolApp.Admin.WebAPI.Endpoints.Student;

public class GetByIdStudentRequest : BaseRequest
{
    public int StudentId { get; init; }

    public GetByIdStudentRequest(int StudentId)
    {
        StudentId = StudentId;
    }
}