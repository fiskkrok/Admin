namespace SchoolApp.Admin.Domain.AggregateModels.CourseAggregate;

public sealed class Course
    : BaseEntity, IAggregateRoot
{
    private Course(string courseId, string? courseCode, string? courseName, int credits)
    {
        CourseId = courseId;
        CourseCode = courseCode;
        CourseName = courseName;
        Credits = credits;
    }

    public static Course CreateInstance(string courseId, string? courseCode, string? courseName, int credits)
    {
        return new Course(courseId, courseCode, courseName, credits);
    }

    [Column("CourseID")]
    public string CourseId { get; init; } = Guid.NewGuid().ToString();

    [StringLength(10)]
    [Unicode(false)]
    public string? CourseCode { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string? CourseName { get; set; }

    public int Credits { get; set; }

    public string? Description { get; init; }

    [InverseProperty("Course")]
    public IEnumerable<CourseAssignment> CourseAssignments { get;  } = new List<CourseAssignment>();

    [InverseProperty("Course")]
    public IEnumerable<Enrollment> Enrollments { get; } = new List<Enrollment>();
    public IEnumerable<Student> Students { get; } = new List<Student>();

}
