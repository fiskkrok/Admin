namespace SchoolApp.Admin.WebAPI.Endpoints.CourseAssignment;

public class DeleteCourseAssignmentResponse : BaseResponse
{
    public DeleteCourseAssignmentResponse(Guid correlationId) : base(correlationId)
    {
    }

    public DeleteCourseAssignmentResponse()
    {
    }

    public string Status { get; set; } = "Deleted";
}