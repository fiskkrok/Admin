
using SchoolApp.Admin.Application.Commands.Course;
using SchoolApp.Admin.WebAPI;

namespace SchoolApp.Admin.WebAPI.Endpoints.Course;

public class ListCoursesResponse : BaseResponse
{
    public ListCoursesResponse(Guid correlationId) : base(correlationId)
    {
    }

    public ListCoursesResponse()
    {
    }

    public IEnumerable<CourseRecord> Courses { get; init; } = new List<CourseRecord>();
}
