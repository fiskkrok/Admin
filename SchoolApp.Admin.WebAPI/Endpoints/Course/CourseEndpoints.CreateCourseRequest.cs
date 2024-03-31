
using MediatR;
using SchoolApp.Admin.WebAPI;

namespace SchoolApp.Admin.WebAPI.Endpoints.Course;

public class CreateCourseRequest : BaseRequest, IRequest<bool>
{
    public int CourseId { get; set; }
    public string? CourseCode { get; set; }
    public string? CourseName { get; set; }
    public int Credits { get; set; }
    public string? Description { get; set; }
}
