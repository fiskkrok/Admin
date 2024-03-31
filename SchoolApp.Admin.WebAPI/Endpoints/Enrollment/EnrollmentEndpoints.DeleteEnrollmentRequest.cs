using SchoolApp.Admin.WebAPI;

namespace Admin.WebAPI;

public class DeleteEnrollmentRequest : BaseRequest
{
    public int EnrollmentId { get; init; }

    public DeleteEnrollmentRequest(int enrollmentId)
    {
        EnrollmentId = enrollmentId;
    }
}
