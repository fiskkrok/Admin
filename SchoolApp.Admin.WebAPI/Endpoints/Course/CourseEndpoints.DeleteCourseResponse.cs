namespace SchoolApp.Admin.WebAPI.Endpoints.Course;

public class DeleteCourseResponse : BaseResponse
{
    public DeleteCourseResponse(Guid correlationId) : base(correlationId)
    {
    }

    public DeleteCourseResponse()
    {
    }

    public string Status { get; set; } = "Deleted";
}