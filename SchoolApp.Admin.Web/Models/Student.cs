namespace Admin.Web.Models;

public class Student
{
    public int Id { get; set; }
    public int StudentId { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public DateTime DateOfBirth { get; set; }
    public string? Email { get; set; }
    public DateTime EnrollmentDate { get; set; }
    // Include Address and any other relevant properties
}

