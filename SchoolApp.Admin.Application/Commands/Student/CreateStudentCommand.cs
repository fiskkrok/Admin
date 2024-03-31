using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace SchoolApp.Admin.Application.Commands.Student;
[DataContract]
public class CreateStudentCommand
    : IRequest<bool>
{

    [DataMember]
    public string FirstName { get; set; }
    [DataMember]
    public string LastName { get; set; }
    [DataMember]
    public DateOnly? DateOfBirth { get; set; }
    [DataMember]
    public string? Email { get; set; }
    [DataMember]
    public DateOnly? EnrollmentDate { get; set; }
    public CreateStudentCommand(DateOnly enrollmentDate, string email, DateOnly dateOfBirth, string lastName, string firstName)
    {
        EnrollmentDate = enrollmentDate;
        Email = email;
        DateOfBirth = dateOfBirth;
        LastName = lastName;
        FirstName = firstName;
    }
}
