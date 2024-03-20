using Microsoft.eShopWeb.PublicApi;

namespace Admin.WebAPI.Endpoints.Student;

public class ListStudentsResponse : BaseResponse
{
    public ListStudentsResponse(Guid correlationId) : base(correlationId)
    {
    }

    public ListStudentsResponse()
    {
    }

    public List<StudentRecord> Students { get; set; } = new List<StudentRecord>();
}
