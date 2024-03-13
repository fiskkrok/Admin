using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Admin.Application.AggregateModels.CourseAssignmentAggregate;
using Admin.Application.AggregateModels.EnrollmentAggregate;
using Admin.Application.SeedWork;
using Admin.Domain.SeedWork;

namespace Admin.Application.AggregateModels.CourseAggregate;

public class Course : BaseEntity, IAggregateRoot
{
    //[Key]
    //[Column("CourseID")]
    public int CourseId { get; set; }

    //[StringLength(10)]
    //[Unicode(false)]
    public string? CourseCode { get; set; }

    //[StringLength(100)]
    //[Unicode(false)]
    public string? CourseName { get; set; }

    public int Credits { get; set; }

    public string Description { get; set; }

    //[InverseProperty("Course")]
    public virtual ICollection<CourseAssignment> CourseAssignments { get; set; } = new List<CourseAssignment>();

    //[InverseProperty("Course")]
    public virtual ICollection<Enrollment> Enrollments { get; set; } = new List<Enrollment>();

    public Course(int courseId,
        
        string description,
        string courseName,
        string courseCode,
        int credits)
    {
        CourseId = courseId;
        Description = description;
        CourseName = courseName;
        CourseCode = courseCode;
        Credits = credits;
    }
}
