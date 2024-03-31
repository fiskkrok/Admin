using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolApp.Admin.Application.Commands.Faculty;
public class UpdateFacultyCommand : IRequest<bool>
{
    public int FacultyId { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Department { get; set; }

    public UpdateFacultyCommand(string firstName, string lastName, string department, int facultyId)
    {
        FirstName = firstName;
        LastName = lastName;
        Department = department;
        FacultyId = facultyId;
    }
}
