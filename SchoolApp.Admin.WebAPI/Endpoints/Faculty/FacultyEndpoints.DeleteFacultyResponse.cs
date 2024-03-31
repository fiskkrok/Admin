namespace SchoolApp.Admin.WebAPI.Endpoints.Faculty;

public class DeleteFacultyResponse : BaseResponse
{
    public DeleteFacultyResponse(Guid correlationId) : base(correlationId)
    {
    }

    public DeleteFacultyResponse()
    {
    }

    public string Status { get; set; } = "Deleted";
}