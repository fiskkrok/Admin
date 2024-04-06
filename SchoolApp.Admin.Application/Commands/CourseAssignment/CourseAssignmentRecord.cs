using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolApp.Admin.Application.Commands.CourseAssignment;

public record CourseAssignmentRecord(
    string AssignmentId,
    string FacultyId,
    string CourseId,
    string? AssignmentType);
