namespace SchoolApp.Admin.Domain.AggregateModels.CourseAggregate;

public class Course
    : BaseEntity, IAggregateRoot
{
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    public Course()
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    {
      
    }
    public Course(string courseId, string? courseCode, string? courseName, int credits, IEnumerable<CourseAssignment> courseAssignments, IEnumerable<Enrollment> enrollments, IEnumerable<Student> students)
    {
        CourseId = courseId;
        CourseCode = courseCode;
        CourseName = courseName;
        Credits = credits;
        CourseAssignments = courseAssignments;
        Enrollments = enrollments;
        Students = students;
    }

    public static Course CreateInstance(string courseId, string? courseCode, string? courseName, int credits)
    {
        return new Course(courseId, courseCode, courseName, credits,  [], [], []);
    }

    public bool Status { get; set; } = true;
    public string CourseId { get; set; }

    [StringLength(10)]
    [Unicode(false)]
    public string? CourseCode { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string? CourseName { get; set; }

    public int Credits { get; set; }

    public string? Description { get; init; }

    [InverseProperty("Course")]
    public IEnumerable<CourseAssignment>? CourseAssignments { get; set; }

    [InverseProperty("Course")]
    public IEnumerable<Enrollment> Enrollments { get; set; }
    public IEnumerable<Student> Students { get; set; }

}
