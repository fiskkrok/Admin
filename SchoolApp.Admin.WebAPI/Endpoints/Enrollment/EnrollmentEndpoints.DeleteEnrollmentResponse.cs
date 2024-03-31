using SchoolApp.Admin.WebAPI;

namespace Admin.WebAPI;

public class DeleteEnrollmentResponse : BaseResponse
{
    public DeleteEnrollmentResponse(Guid correlationId) : base(correlationId)
    {
    }

    public DeleteEnrollmentResponse()
    {
    }

    public string Status { get; set; } = "Deleted";
}