using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace SchoolApp.Admin.Application.Commands.Enrollment;
[DataContract]
public class CreateEnrollmentCommand:IRequest<bool>
{
    [DataMember]
    public string EnrollmentId { get; set; }
    [DataMember]
    public string StudentId { get; set; }
    [DataMember]
    public string CourseId { get; set; }

    public CreateEnrollmentCommand(string enrollmentId, string studentId, string courseId)
    {
        EnrollmentId = enrollmentId;
        StudentId = studentId;
        CourseId = courseId;
    }

    public CreateEnrollmentCommand()
    {
    }
}
