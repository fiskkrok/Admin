

namespace SchoolApp.Admin.Application.Commands.Course;

public record CourseRecord(int CourseId, string? CourseName)
{
    public string? CourseCode { get; set; }
    public string? Description { get; set; }
    public int Credits { get; set; }
}