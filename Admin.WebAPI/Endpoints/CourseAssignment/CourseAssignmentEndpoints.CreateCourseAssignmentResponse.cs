using Admin.WebAPI.Endpoints.Course;
using Microsoft.eShopWeb.PublicApi;

namespace Admin.WebAPI.Endpoints.CourseAssignment;

public class CreateCourseAssignmentResponse : BaseResponse
{
    public CreateCourseAssignmentResponse(Guid correlationId) : base(correlationId)
    {
    }

    public CreateCourseAssignmentResponse()
    {
    }

    public CourseAssignmentRecord CourseAssignment { get; set; }
}
