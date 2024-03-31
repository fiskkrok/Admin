using SchoolApp.Admin.Application.Commands.Enrollment;
using SchoolApp.Admin.WebAPI;

namespace SchoolApp.Admin.WebAPI.Endpoints.Enrollment;

public class CreateEnrollmentResponse : BaseResponse
{
    public CreateEnrollmentResponse(Guid correlationId) : base(correlationId)
    {
    }

    public CreateEnrollmentResponse()
    {
    }

    public EnrollmentRecord? Enrollment { get; set; }
}