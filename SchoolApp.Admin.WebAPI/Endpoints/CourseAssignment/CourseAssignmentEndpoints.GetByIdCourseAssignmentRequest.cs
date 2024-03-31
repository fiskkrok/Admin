namespace SchoolApp.Admin.WebAPI.Endpoints.CourseAssignment;

public class GetByIdCourseAssignmentRequest : BaseRequest
{
    public int CourseAssignmentId { get; init; }

    public GetByIdCourseAssignmentRequest(int courseAssignmentId)
    {
        CourseAssignmentId = courseAssignmentId;
    }
}