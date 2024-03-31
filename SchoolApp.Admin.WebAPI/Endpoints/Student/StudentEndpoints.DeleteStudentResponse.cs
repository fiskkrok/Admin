namespace SchoolApp.Admin.WebAPI.Endpoints.Student;

public class DeleteStudentResponse : BaseResponse
{
    public DeleteStudentResponse(Guid correlationId) : base(correlationId)
    {
    }

    public DeleteStudentResponse()
    {
    }

    public string Status { get; set; } = "Deleted";
}
