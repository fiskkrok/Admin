namespace SchoolApp.Admin.Domain.AggregateModels.FacultyAggregate;


public class Faculty(string? firstName, string? lastName, string? department)
    : BaseEntity, IAggregateRoot
{
    public string FacultyId { get;  set; }

    [Required]
    [StringLength(50)]
    public string? FirstName { get; set; } = firstName;

    [Required]
    [StringLength(50)]
    public string? LastName { get; set; } = lastName;

    [StringLength(50)]
    public string? Department { get; set; } = department;

    public virtual ICollection<CourseAssignment> CourseAssignments { get; init; } = new List<CourseAssignment>();

}
