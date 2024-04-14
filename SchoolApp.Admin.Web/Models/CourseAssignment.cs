namespace SchoolApp.Admin.Web.Models;

public class CourseAssignment
{
    public string? AssignmentId { get; set; }
    public string? FacultyId { get; set; } // Nullable if faculty is optional
    public string? CourseId { get; set; } // Nullable if course is optional
    public string? AssignmentType { get; set; }
    public Faculty? Faculty { get; set; }
    public Course? Course { get; set; }
    // Include navigation properties if needed, like Course or Faculty details
}


