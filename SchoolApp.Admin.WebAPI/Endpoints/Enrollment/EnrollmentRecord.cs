using Admin.Application.Entities.CourseLogic;
using Admin.Application.Entities.StudentLogic;

namespace Admin.WebAPI.Endpoints.Enrollment;

public record EnrollmentRecord(int EnrollmentId, int? StudentId, int? CourseId, DateOnly? EnrollmentDate);