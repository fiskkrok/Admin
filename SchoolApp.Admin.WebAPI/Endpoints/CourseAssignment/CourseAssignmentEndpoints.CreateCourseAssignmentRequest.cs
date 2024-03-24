using SchoolApp.Admin.WebAPI;

namespace SchoolApp.Admin.WebAPI.Endpoints.CourseAssignment;

public class CreateCourseAssignmentRequest : BaseRequest
{
    public int AssignmentId { get; set; }
    public int? FacultyId { get; set; }
    public int? CourseId { get; set; }
    public string? AssignmentType { get; set; }
}
