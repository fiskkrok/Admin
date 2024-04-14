using SchoolApp.Admin.WebAPI;

namespace SchoolApp.Admin.WebAPI.Endpoints.CourseAssignment;

public record CreateCourseAssignmentRequest(string AssignmentId, string FacultyId, string CourseId, string? AssignmentType);

