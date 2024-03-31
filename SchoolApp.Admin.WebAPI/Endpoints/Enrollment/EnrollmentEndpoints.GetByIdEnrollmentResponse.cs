using SchoolApp.Admin.Application.Commands.Enrollment;

namespace SchoolApp.Admin.WebAPI.Endpoints.Enrollment;

public class GetByIdEnrollmentResponse : BaseResponse
{
    public GetByIdEnrollmentResponse(Guid correlationId) : base(correlationId)
    {
    }

    public GetByIdEnrollmentResponse()
    {
    }

    public EnrollmentRecord? Enrollment { get; set; }
}