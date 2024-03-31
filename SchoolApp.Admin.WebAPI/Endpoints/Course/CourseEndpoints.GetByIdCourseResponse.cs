using SchoolApp.Admin.Application.Commands.Course;
using SchoolApp.Admin.Application.Commands.Enrollment;

namespace SchoolApp.Admin.WebAPI.Endpoints.Course;

public class GetByIdCourseResponse : BaseResponse
{
    public GetByIdCourseResponse(Guid correlationId) : base(correlationId)
    {
    }

    public GetByIdCourseResponse()
    {
    }

    public CourseRecord? Course { get; set; }
}