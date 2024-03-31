using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace SchoolApp.Admin.Application.Commands.Faculty;
[DataContract]
public class CreateFacultyCommand : IRequest<bool>
{
    [DataMember]
    public string? FirstName { get; set; }
    [DataMember]
    public string? LastName { get; set; }
    [DataMember]
    public string? Department { get; set; }

    public CreateFacultyCommand(string firstName,string lastName,string department)
    {
        FirstName = firstName;
        LastName = lastName;
            Department = department;
    }
}
