using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Admin.Application.AggregateModels.CourseAggregate;
using Admin.Application.AggregateModels.StudentAggregate;
using Admin.Application.SeedWork;
using Admin.Domain.SeedWork;

namespace Admin.Application.AggregateModels.EnrollmentAggregate;

public partial class Enrollment : BaseEntity, IAggregateRoot
{
    //[Key]
    //[Column("EnrollmentID")]
    public int EnrollmentId { get; set; }

    //[Column("StudentID")]
    public int? StudentId { get; set; }

    //[Column("CourseID")]
    public int? CourseId { get; set; }

    public DateOnly? EnrollmentDate { get; set; }

    //[ForeignKey("CourseId")]
    //[InverseProperty("Enrollments")]
    public virtual Course? Course { get; set; }

    //[ForeignKey("StudentId")]
    //[InverseProperty("Enrollments")]
    public virtual Student? Student { get; set; }

    public Enrollment(int enrollmentId, int? studentId, int? courseId, DateOnly? enrollmentDate)
    {
        EnrollmentId = enrollmentId;
        StudentId = studentId;
        CourseId = courseId;
        EnrollmentDate = enrollmentDate;
    }
}
