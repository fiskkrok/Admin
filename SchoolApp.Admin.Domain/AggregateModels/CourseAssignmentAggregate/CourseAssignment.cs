namespace SchoolApp.Admin.Domain.AggregateModels.CourseAssignmentAggregate;

public sealed class CourseAssignment : BaseEntity, IAggregateRoot
{
    public CourseAssignment()
    {
    }
    private CourseAssignment(string facultyId,
        string courseId,
        string? assignmentType)
    {
        FacultyId = facultyId;
        CourseId = courseId;
        AssignmentType = assignmentType;
    }

    public static CourseAssignment CreateInstance(string facultyId, string courseId, string? assignmentType)
    {
        return new CourseAssignment(facultyId, courseId, assignmentType);
    }


    [Column("AssignmentID")]
    public string AssignmentId { get; set; }

    [Column("FacultyID")]
    public string FacultyId { get; set; }

    [Column("CourseID")]
    public string CourseId { get; set; }

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
