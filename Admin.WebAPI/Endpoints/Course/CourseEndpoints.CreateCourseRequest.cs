using Microsoft.eShopWeb.PublicApi;

namespace Admin.WebAPI.Endpoints.Course;

public class CreateCourseRequest : BaseRequest
{
    public int CourseId { get; set; }
    public string? CourseCode { get; set; }
    public string? CourseName { get; set; }
    public int Credits { get; set; }
    public string? Description { get; set; }
}
