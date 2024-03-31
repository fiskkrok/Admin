


using SchoolApp.Admin.Application.Commands.Student;

namespace SchoolApp.Admin.WebAPI.Endpoints.Student;

public class CreateStudentRequest : BaseRequest
{
    public int StudentId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateOnly DateOfBirth { get; set; }
    public string Email { get; set; }
    public AddressRecord Address { get; set; }
    public DateOnly EnrollmentDate { get; set; }
}
