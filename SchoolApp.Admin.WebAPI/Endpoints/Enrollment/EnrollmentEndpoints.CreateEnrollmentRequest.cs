using Microsoft.eShopWeb.PublicApi;

namespace Admin.WebAPI.Endpoints.Enrollment;

public class CreateEnrollmentRequest : BaseRequest
{
    public int EnrollmentId { get; set; }

    public int? StudentId { get; set; }

    public int? CourseId { get; set; }

    public DateOnly? EnrollmentDate { get; set; }
}
