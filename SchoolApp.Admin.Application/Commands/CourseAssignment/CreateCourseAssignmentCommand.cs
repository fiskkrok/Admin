
using System.Runtime.Serialization;
using System.Runtime.Serialization.DataContracts;


namespace SchoolApp.Admin.Application.Commands.CourseAssignment;
[DataContract]
public class CreateCourseAssignmentCommand:IRequest<bool>
{
    [DataMember] 
    public string AssignmentId { get; set; }
    [DataMember]
    public string FacultyId { get; set; }
    [DataMember]
    public string CourseId { get; set; }
    [DataMember]
    public string? AssignmentType { get; set; }

    public CreateCourseAssignmentCommand(string assignmentId, string facultyId, string assignmentType, string courseId)
    {
        AssignmentId = assignmentId;
        FacultyId = facultyId;
        AssignmentType = assignmentType;
        CourseId = courseId;
    }

    public CreateCourseAssignmentCommand()
    {
    }
}