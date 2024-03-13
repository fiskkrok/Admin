using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Admin.Application.AggregateModels.CourseAggregate;
using Admin.Application.AggregateModels.FacultyAggregate;
using Admin.Application.SeedWork;
using Admin.Domain.SeedWork;

namespace Admin.Application.AggregateModels.CourseAssignmentAggregate;

public partial class CourseAssignment : BaseEntity, IAggregateRoot
{
    //[Key]
    //[Column("AssignmentID")]
    public int AssignmentId { get; set; }

    //[Column("FacultyID")]
    public int? FacultyId { get; set; }

    //[Column("CourseID")]
    public int? CourseId { get; set; }

    //[StringLength(50)]
    //[Unicode(false)]
    public string? AssignmentType { get; set; }

    //[ForeignKey("CourseId")]
    //[InverseProperty("CourseAssignments")]
    public virtual Course? Course { get; set; }

    //[ForeignKey("FacultyId")]
    //[InverseProperty("CourseAssignments")]
    public virtual Faculty? Faculty { get; set; }

    public CourseAssignment(int assignmentId,
        int? facultyId,
        int? courseId,
        string? assignmentType)
    {
        AssignmentId = assignmentId;
        FacultyId = facultyId;
        CourseId = courseId;
        AssignmentType = assignmentType;
    }
}
