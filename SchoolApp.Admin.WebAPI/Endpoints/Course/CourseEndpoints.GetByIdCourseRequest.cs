namespace SchoolApp.Admin.WebAPI.Endpoints.Course;

public class GetByIdCourseRequest : BaseRequest
{
    public int CourseId { get; init; }

    public GetByIdCourseRequest(int courseId)
    {
        CourseId = courseId;
    }
}
