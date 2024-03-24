
using SchoolApp.Admin.WebAPI;

namespace SchoolApp.Admin.WebAPI.Endpoints.Course;

public class CreateCourseResponse : BaseResponse
{
    public CreateCourseResponse(Guid correlationId) : base(correlationId)
    {
    }

    public CreateCourseResponse()
    {
    }

    public CourseRecord Course { get; set; }
}