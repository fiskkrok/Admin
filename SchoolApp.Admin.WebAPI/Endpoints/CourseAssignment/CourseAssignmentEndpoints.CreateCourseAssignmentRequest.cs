using SchoolApp.Admin.WebAPI;

namespace SchoolApp.Admin.WebAPI.Endpoints.CourseAssignment;

public record CreateCourseAssignmentRequest(int AssignmentId, int FacultyId, int CourseId, string? AssignmentType);

