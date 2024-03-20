namespace Admin.Web.Models;
public class Course
{
    public int Id { get; set; }
    public int CourseId { get; set; }
    public string? CourseCode { get; set; }
    public string? CourseName { get; set; }
    public int Credits { get; set; }
    public string? Description { get; set; }
    // Consider including other properties such as CourseAssignments and Enrollments as needed
}
