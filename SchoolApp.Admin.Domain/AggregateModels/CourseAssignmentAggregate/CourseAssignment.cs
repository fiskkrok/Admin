namespace SchoolApp.Admin.Domain.AggregateModels.CourseAssignmentAggregate;

public sealed class CourseAssignment : BaseEntity, IAggregateRoot
{
    private CourseAssignment(int? facultyId,
        int? courseId,
        string? assignmentType)
    {
        FacultyId = facultyId;
        CourseId = courseId;
        AssignmentType = assignmentType;
    }

    public static CourseAssignment CreateInstance(int? facultyId, int? courseId, string? assignmentType)
    {
        return new CourseAssignment(facultyId, courseId, assignmentType);
    }


    [Column("AssignmentID")]
    public string AssignmentId { get; set; } = Guid.NewGuid().ToString();

    [Column("FacultyID")]
    public int? FacultyId { get; set; }

    [Column("CourseID")]
    public int? CourseId { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string? AssignmentType { get; set; }

    [ForeignKey("CourseId")]
    [InverseProperty("CourseAssignments")]
    public Course? Course { get; set; }

    [ForeignKey("FacultyId")]
    [InverseProperty("CourseAssignments")]
    public Faculty? Faculty { get; set; }

   
}
