namespace SchoolApp.Admin.WebAPI.Endpoints.Enrollment;

public class GetByIdEnrollmentRequest : BaseRequest
{
    public int EnrollmentId { get; init; }

    public GetByIdEnrollmentRequest(int enrollmentId)
    {
        EnrollmentId = enrollmentId;
    }
}
