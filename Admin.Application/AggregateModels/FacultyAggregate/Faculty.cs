using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Admin.Application.AggregateModels.CourseAssignmentAggregate;
using Admin.Application.SeedWork;
using Admin.Domain.SeedWork;

namespace Admin.Application.AggregateModels.FacultyAggregate;


public partial class Faculty : BaseEntity, IAggregateRoot
{
    public int FacultyId { get; set; }
    public string? FirstName { get; set; }

    public string? LastName { get; set; }
    public string? Department { get; set; }
    public virtual ICollection<CourseAssignment> CourseAssignments { get; set; } = new List<CourseAssignment>();

    public Faculty(int facultyId, string? firstName, string? lastName, string? department)
    {
        FacultyId = facultyId;
        FirstName = firstName;
        LastName = lastName;
        Department = department;
    }
}
