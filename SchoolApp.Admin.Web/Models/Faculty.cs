namespace Admin.Web.Models;

public class Faculty
{
    public int Id { get; set; }
    public int FacultyId { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Department { get; set; }
    // Include other properties as needed, like CourseAssignments
}

