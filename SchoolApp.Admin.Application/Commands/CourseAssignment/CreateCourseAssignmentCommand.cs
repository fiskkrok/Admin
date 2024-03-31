
using System.Runtime.Serialization;
using System.Runtime.Serialization.DataContracts;


namespace SchoolApp.Admin.Application.Commands.CourseAssignment;
[DataContract]
public class CreateCourseAssignmentCommand:IRequest<bool>
{
    [DataMember] 
    public int AssignmentId { get; set; }
    [DataMember]
    public int? FacultyId { get; set; }
    [DataMember]
    public int? CourseId { get; set; }
    [DataMember]
    public string? AssignmentType { get; set; }

    public CreateCourseAssignmentCommand(int assignmentId, int facultyId, string assignmentType, int courseId)
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