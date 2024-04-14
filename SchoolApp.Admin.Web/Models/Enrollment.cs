namespace SchoolApp.Admin.Web.Models;

public class Enrollment
{
    public string? EnrollmentId { get; set; }
    public string? StudentId { get; set; } // Nullable if student is optional
    public string? CourseId { get; set; } // Nullable if course is optional
    public IEnumerable<Course>? Course { get; set; } // Nullable if course is optional
    public IEnumerable<Student>? Student { get; set; } // Nullable if course is optional
    public DateTime? EnrollmentDate { get; set; }
    // Include navigation properties for Course or Student if needed
}

