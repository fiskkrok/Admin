using MediatR;

namespace SchoolApp.Admin.WebAPI.Endpoints.Course;

public class DeleteCourseRequest : BaseRequest
{
    public int CourseId { get; init; }

    public DeleteCourseRequest(int courseId)
    {
        CourseId = courseId;
    }
}