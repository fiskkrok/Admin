using SchoolApp.Admin.WebAPI;

namespace SchoolApp.Admin.WebAPI.Endpoints.Enrollment;

public class CreateEnrollmentRequest : BaseRequest
{
    public string? EnrollmentId { get; set; }

    public string? StudentId { get; set; }

    public string? CourseId { get; set; }

    public DateOnly? EnrollmentDate { get; set; }
}
