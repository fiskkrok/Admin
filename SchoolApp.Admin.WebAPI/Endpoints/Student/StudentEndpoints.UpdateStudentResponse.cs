using SchoolApp.Admin.Application.Commands.Student;

namespace SchoolApp.Admin.WebAPI.Endpoints.Student;

public class UpdateStudentResponse : BaseResponse
{
    public UpdateStudentResponse(Guid correlationId) : base(correlationId)
    {
    }

    public UpdateStudentResponse()
    {
    }

    public StudentRecord Student { get; set; }
}