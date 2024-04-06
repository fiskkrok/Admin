
namespace SchoolApp.Admin.Application.Queries.Courses;
public interface ICourseQueries
{
    Task<Course> GetCourseByIdAsync(int courseId);
    Task<IEnumerable<Course>> GetAllCourses();
    Task<IEnumerable<Course>> GetAllCoursesStudent(int studentId);
}
