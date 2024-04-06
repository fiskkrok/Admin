using System.ComponentModel.DataAnnotations;

namespace SchoolApp.Admin.WebAPI.Endpoints.Student;

public class UpdateStudentRequest : BaseRequest
{

    public DateOnly EnrollmentDate { get; set; }
    //Email, DateOfBirth, LastName, FirstName

    [Required]
    public string Email { get; set; }
    [Required]
    public DateOnly DateOfBirth { get; set; }
    [Required]
    public string LastName { get; set; }
    [Required]
    public string FirstName { get; set; }


}
