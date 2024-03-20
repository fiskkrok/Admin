namespace Admin.Web.Models;

public class Enrollment
{
    public int Id { get; set; }
    public int EnrollmentId { get; set; }
    public int? StudentId { get; set; } // Nullable if student is optional
    public int? CourseId { get; set; } // Nullable if course is optional
    public DateTime? EnrollmentDate { get; set; }
    // Include navigation properties for Course or Student if needed
}

