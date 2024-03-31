using SchoolApp.Admin.Application.Commands.CourseAssignment;
using SchoolApp.Admin.WebAPI;

namespace SchoolApp.Admin.WebAPI.Endpoints.CourseAssignment;

public class ListCourseAssignmentsResponse : BaseResponse
{
    public ListCourseAssignmentsResponse(Guid correlationId) : base(correlationId)
    {
    }

    public ListCourseAssignmentsResponse()
    {
    }

    public IEnumerable<CourseAssignmentRecord> CourseAssignments { get; init; } = new List<CourseAssignmentRecord>();
}