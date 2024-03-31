using System.ComponentModel.DataAnnotations;

namespace SchoolApp.Admin.WebAPI.Endpoints.Course;

public class UpdateCourseRequest : BaseRequest
{
    [Required]
    public int CourseId { get; set; }
    public string? CourseName { get; set; }
    public string? CourseCode { get; set; }
    public string? Description { get; set; }
    public int Credits { get; set; }
}