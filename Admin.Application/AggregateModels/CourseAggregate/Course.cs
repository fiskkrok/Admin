using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Admin.Application.AggregateModels.CourseAssignmentAggregate;
using Admin.Application.AggregateModels.EnrollmentAggregate;
using Admin.Application.SeedWork;
using Admin.Domain.SeedWork;

namespace Admin.Application.AggregateModels.CourseAggregate;

public class Course(
    int courseId,
    string description,
    string courseName,
    string courseCode,
    int credits)
    : BaseEntity, IAggregateRoot
{
    //[Key]
    //[Column("CourseID")]
    public int CourseId { get; set; } = courseId;

    //[StringLength(10)]
    //[Unicode(false)]
    public string? CourseCode { get; set; } = courseCode;

    //[StringLength(100)]
    //[Unicode(false)]
    public string? CourseName { get; set; } = courseName;

    public int Credits { get; set; } = credits;

    public string Description { get; init; } = description ?? string.Empty;

    //[InverseProperty("Course")]
    public virtual ICollection<CourseAssignment> CourseAssignments { get; set; } = new List<CourseAssignment>();

    //[InverseProperty("Course")]
    public virtual ICollection<Enrollment> Enrollments { get; set; } = new List<Enrollment>();
}
