namespace Admin.Web.Models;

public class CourseAssignment
{
    public int Id { get; set; }
    public int AssignmentId { get; set; }
    public int? FacultyId { get; set; } // Nullable if faculty is optional
    public int? CourseId { get; set; } // Nullable if course is optional
    public string? AssignmentType { get; set; }
    // Include navigation properties if needed, like Course or Faculty details
}

