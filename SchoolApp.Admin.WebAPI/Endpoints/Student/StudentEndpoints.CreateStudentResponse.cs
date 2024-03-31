using SchoolApp.Admin.Application.Commands.Student;
using SchoolApp.Admin.WebAPI;

namespace SchoolApp.Admin.WebAPI.Endpoints.Student;

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
