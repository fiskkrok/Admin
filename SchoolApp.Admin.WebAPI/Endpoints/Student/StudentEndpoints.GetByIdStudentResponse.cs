using SchoolApp.Admin.Application.Commands.Enrollment;
using SchoolApp.Admin.Application.Commands.Student;

namespace SchoolApp.Admin.WebAPI.Endpoints.Student;

public class GetByIdStudentResponse : BaseResponse
{
    public GetByIdStudentResponse(Guid correlationId) : base(correlationId)
    {
    }
    public GetByIdStudentResponse()
    {
    }

    public StudentRecord? Student { get; set; }
}