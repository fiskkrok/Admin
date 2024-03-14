using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Admin.Application.AggregateModels.CourseAssignmentAggregate;
using Admin.Application.SeedWork;
using Admin.Domain.SeedWork;

namespace Admin.Application.AggregateModels.FacultyAggregate;


public class Faculty(int facultyId, string? firstName, string? lastName, string? department)
    : BaseEntity, IAggregateRoot
{
    public int FacultyId { get; init; } = facultyId;
    public string? FirstName { get; set; } = firstName;

    public string? LastName { get; set; } = lastName;
    public string? Department { get; set; } = department;
    public virtual ICollection<CourseAssignment> CourseAssignments { get; init; } = new List<CourseAssignment>
    {
        Capacity = 0
    };
}
