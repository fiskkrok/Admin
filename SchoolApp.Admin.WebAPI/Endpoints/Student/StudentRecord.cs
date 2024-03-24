

namespace SchoolApp.Admin.WebAPI.Endpoints.Student;

public record StudentRecord(
    int StudentId,
    string FirstName,
    string LastName,
    DateOnly? DateOfBirth,
    string? Email,
    DateOnly? EnrollmentDate);
