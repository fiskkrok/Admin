

namespace SchoolApp.Admin.Application.Commands.Enrollment;

public record EnrollmentRecord(int EnrollmentId, int? StudentId, int? CourseId, DateOnly? EnrollmentDate);