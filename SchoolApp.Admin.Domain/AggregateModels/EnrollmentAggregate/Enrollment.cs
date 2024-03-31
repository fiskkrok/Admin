
namespace SchoolApp.Admin.Domain.AggregateModels.EnrollmentAggregate;

public class Enrollment(int? studentId, int? courseId, DateOnly? enrollmentDate)
    : BaseEntity, IAggregateRoot
{

    [Column("EnrollmentID")]
    public string EnrollmentId { get; set; } = Guid.NewGuid().ToString();

    [Column("StudentID")]
    public int? StudentId { get; set; } = studentId;

    [Column("CourseID")]
    public int? CourseId { get; set; } = courseId;


    public DateOnly? EnrollmentDate { get; set; } = enrollmentDate;

    [ForeignKey("CourseId")]
    [InverseProperty("Enrollments")]
    public virtual Course? Course { get; set; }

    [ForeignKey("StudentId")]
    [InverseProperty("Enrollments")]
    public virtual Student? Student { get; set; }

  
}
