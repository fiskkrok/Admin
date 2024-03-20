using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Admin.Application.Entities.CourseLogic;

namespace Admin.WebAPI.Endpoints.CourseAssignment;

public record CourseAssignmentRecord(
    int AssignmentId,
    int? FacultyId,
    int? CourseId,
    string? AssignmentType);
