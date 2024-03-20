using Microsoft.eShopWeb.PublicApi;

namespace Admin.WebAPI.Endpoints.Enrollment;

public class ListEnrollmentsResponse : BaseResponse
{
    public ListEnrollmentsResponse(Guid correlationId) : base(correlationId)
    {
    }

    public ListEnrollmentsResponse()
    {
    }

    public List<EnrollmentRecord> Enrollments { get; set; } = new List<EnrollmentRecord>();
}
