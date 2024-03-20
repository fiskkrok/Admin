using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Admin.Application.AggregateModels.CourseAggregate;
using Admin.Application.AggregateModels.StudentAggregate;
using Admin.Application.SeedWork;
using Admin.Domain.SeedWork;

namespace Admin.Application.AggregateModels.EnrollmentAggregate;

public class Enrollment(int enrollmentId, int? studentId, int? courseId, DateOnly? enrollmentDate)
    : BaseEntity, IAggregateRoot
{

    [Column("EnrollmentID")]
    public int EnrollmentId { get; set; } = enrollmentId;

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
