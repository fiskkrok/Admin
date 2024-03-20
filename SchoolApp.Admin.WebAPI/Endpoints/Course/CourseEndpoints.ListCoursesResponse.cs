using Microsoft.eShopWeb.PublicApi;

namespace Admin.WebAPI.Endpoints.Course;

public class ListCoursesResponse : BaseResponse
{
    public ListCoursesResponse(Guid correlationId) : base(correlationId)
    {
    }

    public ListCoursesResponse()
    {
    }

    public List<CourseRecord> Courses { get; set; } = new List<CourseRecord>();
}
