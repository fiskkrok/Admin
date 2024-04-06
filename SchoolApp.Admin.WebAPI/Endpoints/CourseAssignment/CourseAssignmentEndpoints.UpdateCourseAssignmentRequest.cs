using System.ComponentModel.DataAnnotations;
using MediatR;

namespace SchoolApp.Admin.WebAPI.Endpoints.CourseAssignment;

public record UpdateCourseAssignmentRequest(
    int AssignmentId,
    int FacultyId,
    int CourseId,
    string? AssignmentType
);
