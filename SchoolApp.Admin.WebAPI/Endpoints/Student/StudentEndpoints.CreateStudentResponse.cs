using Microsoft.eShopWeb.PublicApi;

namespace Admin.WebAPI.Endpoints.Student;

public class CreateStudentResponse : BaseResponse
{
    public CreateStudentResponse(Guid correlationId) : base(correlationId)
    {
    }

    public CreateStudentResponse()
    {
    }

    public StudentRecord Student { get; set; }
}
