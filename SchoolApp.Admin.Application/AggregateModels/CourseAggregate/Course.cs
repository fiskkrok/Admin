using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SchoolApp.Admin.Application.AggregateModels.CourseAssignmentAggregate;
using SchoolApp.Admin.Application.AggregateModels.EnrollmentAggregate;
using SchoolApp.Admin.Application.SeedWork;
using Admin.Domain.SeedWork;
using Microsoft.EntityFrameworkCore;

namespace SchoolApp.Admin.Application.AggregateModels.CourseAggregate;

public class Course(
    int courseId,
    string description,
    string courseName,
    string courseCode,
    int credits)
    : BaseEntity, IAggregateRoot
{

    [Column("CourseID")]
    public int CourseId { get; init; } = courseId;

    [StringLength(10)]
    [Unicode(false)]
    public string? CourseCode { get; set; } = courseCode;

    [StringLength(100)]
    [Unicode(false)]
    public string? CourseName { get; set; } = courseName;

    public int Credits { get; set; } = credits;

    public string Description { get; init; } = description ?? string.Empty;

    [InverseProperty("Course")]
    public virtual IEnumerable<CourseAssignment> CourseAssignments { get;  } = new List<CourseAssignment>();

    [InverseProperty("Course")]
    public virtual IEnumerable<Enrollment> Enrollments { get; } = new List<Enrollment>();

}
