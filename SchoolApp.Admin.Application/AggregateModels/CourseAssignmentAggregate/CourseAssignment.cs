
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SchoolApp.Admin.Application.AggregateModels.FacultyAggregate;
using SchoolApp.Admin.Application.SeedWork;

using Microsoft.EntityFrameworkCore;
using SchoolApp.Admin.Application.AggregateModels.CourseAggregate;


namespace SchoolApp.Admin.Application.AggregateModels.CourseAssignmentAggregate;

public class CourseAssignment(
    int assignmentId,
    int? facultyId,
    int? courseId,
    string? assignmentType)
    : BaseEntity, IAggregateRoot
{

    [Column("AssignmentID")]
    public int AssignmentId { get; set; } = assignmentId;

    [Column("FacultyID")]
    public int? FacultyId { get; set; } = facultyId;

    [Column("CourseID")]
    public int? CourseId { get; set; } = courseId;

    [StringLength(50)]
    [Unicode(false)]
    public string? AssignmentType { get; set; } = assignmentType;

    [ForeignKey("CourseId")]
    [InverseProperty("CourseAssignments")]
    public virtual Course? Course { get; set; }

    [ForeignKey("FacultyId")]
    [InverseProperty("CourseAssignments")]
    public virtual Faculty? Faculty { get; set; }

   
}
