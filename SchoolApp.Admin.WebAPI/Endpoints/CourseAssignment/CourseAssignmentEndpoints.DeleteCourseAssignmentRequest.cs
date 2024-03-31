using MediatR;

namespace SchoolApp.Admin.WebAPI.Endpoints.CourseAssignment;

public class DeleteCourseAssignmentRequest : BaseRequest, IRequest<bool>
{
    public int CourseAssignmentId { get; init; }

    public DeleteCourseAssignmentRequest(int courseAssignmentId)
    {
        CourseAssignmentId = courseAssignmentId;
    }
}