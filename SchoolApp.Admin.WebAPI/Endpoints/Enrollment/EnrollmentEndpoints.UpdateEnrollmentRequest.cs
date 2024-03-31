using SchoolApp.Admin.WebAPI;
using System.ComponentModel.DataAnnotations;

namespace Admin.WebAPI;

public class UpdateEnrollmentRequest : BaseRequest
{
    public int EnrollmentId { get; set; }
    public int? StudentId { get; set; }
    public int? CourseId { get; set; }
    public DateOnly? EnrollmentDate { get; set; }
}