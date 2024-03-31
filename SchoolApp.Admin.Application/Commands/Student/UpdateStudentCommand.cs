using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace SchoolApp.Admin.Application.Commands.Student;
public class UpdateStudentCommand:IRequest<bool>
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateOnly? DateOfBirth { get; set; }
    public string? Email { get; set; }
    public DateOnly? EnrollmentDate { get; set; }
    public UpdateStudentCommand(DateOnly enrollmentDate, string email, DateOnly dateOfBirth, string lastName, string firstName)
    {
        EnrollmentDate = enrollmentDate;
        Email = email;
        DateOfBirth = dateOfBirth;
        LastName = lastName;
        FirstName = firstName;
    }
}