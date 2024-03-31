using System.ComponentModel.DataAnnotations;
using MediatR;

namespace SchoolApp.Admin.WebAPI.Endpoints.CourseAssignment;

public class UpdateCourseAssignmentRequest : BaseRequest, IRequest<bool>
{
    public int AssignmentId { get; set; }
    public int? FacultyId { get; set; }
    public int? CourseId { get; set; }
    public string? AssignmentType { get; set; }
}
