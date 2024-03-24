

namespace SchoolApp.Admin.WebAPI.Endpoints.Enrollment;

public record EnrollmentRecord(int EnrollmentId, int? StudentId, int? CourseId, DateOnly? EnrollmentDate);