using Admin.WebAPI.Endpoints.Student;
using Microsoft.eShopWeb.PublicApi;

namespace Admin.WebAPI.Endpoints.CourseAssignment;

public class ListCourseAssignmentsResponse : BaseResponse
{
    public ListCourseAssignmentsResponse(Guid correlationId) : base(correlationId)
    {
    }

    public ListCourseAssignmentsResponse()
    {
    }

    public List<CourseAssignmentRecord> CourseAssignments { get; set; } = new List<CourseAssignmentRecord>();
}