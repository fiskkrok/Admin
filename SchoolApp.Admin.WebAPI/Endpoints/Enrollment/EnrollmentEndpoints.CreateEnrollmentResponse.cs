using Microsoft.eShopWeb.PublicApi;

namespace Admin.WebAPI.Endpoints.Enrollment;

public class CreateEnrollmentResponse : BaseResponse
{
    public CreateEnrollmentResponse(Guid correlationId) : base(correlationId)
    {
    }

    public CreateEnrollmentResponse()
    {
    }

    public EnrollmentRecord Enrollment { get; set; }
}