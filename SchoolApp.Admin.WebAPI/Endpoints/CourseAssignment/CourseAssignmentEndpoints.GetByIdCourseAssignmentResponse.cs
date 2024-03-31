using SchoolApp.Admin.Application.Commands.CourseAssignment;

namespace SchoolApp.Admin.WebAPI.Endpoints.CourseAssignment;

public class GetByIdCourseAssignmentResponse : BaseResponse
{
    public GetByIdCourseAssignmentResponse(Guid correlationId) : base(correlationId)
    {
        
    }

    public GetByIdCourseAssignmentResponse()
    {
        
    }

    public CourseAssignmentRecord CourseAssignment { get; set; }

}
