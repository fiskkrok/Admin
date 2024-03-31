namespace SchoolApp.Admin.WebAPI.Endpoints.Student;

public class DeleteStudentRequest(int studentId) : BaseRequest
{
    public int StudentId { get; init; } = studentId;
}