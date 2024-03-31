
namespace SchoolApp.Admin.Application.Queries.Courses;
public interface ICourseQueries
{
    Task<CourseRecord> GetCourseByIdAsync(int courseId);
    IQueryable<CourseRecord>? GetAllCourses();
    IQueryable<CourseRecord>? GetAllCoursesStudent(int studentId);
}
