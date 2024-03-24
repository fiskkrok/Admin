
using SchoolApp.Admin.Application.AggregateModels.StudentAggregate;


namespace SchoolApp.Admin.WebAPI.Endpoints.Student;

public class CreateStudentRequest : BaseRequest
{
    public int StudentId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateOnly DateOfBirth { get; set; }
    public string Email { get; set; }
    public Address Address { get; set; }
    public DateTime EnrollmentDate { get; set; }
}
