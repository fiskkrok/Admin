

namespace SchoolApp.Admin.Application.Commands.Faculty;

public record FacultyRecord(int FacultyId)
{
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Department { get; set; }
};
