namespace SchoolApp.Admin.Web.Models;

public class Faculty
{
    public IEnumerable<CourseAssignment>? CourseAssignments { get; set; }
    public string? FacultyId { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Department { get; set; }
    // Include other properties as needed, like CourseAssignments
}

