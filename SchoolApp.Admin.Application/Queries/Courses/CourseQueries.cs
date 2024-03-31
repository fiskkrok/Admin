using Microsoft.EntityFrameworkCore;
using SchoolApp.Admin.Infrastructure.Data;

namespace SchoolApp.Admin.Application.Queries.Courses;

public class CourseQueries(AdminDbContext context, IMapper mapper) : ICourseQueries
{
    public async Task<CourseRecord> GetCourseByIdAsync(int courseId)
    {
        var course = await context.Courses
            .Include(o => o.Enrollments)
            .FirstOrDefaultAsync(o => o.Id == courseId);

        return course is null
            ? throw new KeyNotFoundException()
            : new CourseRecord(course.Id, course.CourseName);
    }

    public IQueryable<CourseRecord> GetAllCourses()
    {
        var courses = context.Courses.AsQueryable();
        return mapper.ProjectTo<CourseRecord>(courses);
    }

    public IQueryable<CourseRecord>? GetAllCoursesStudent(int studentId)
    {
        return context.Courses
            .Where(o => o.Students.Any(a => a.StudentId.Equals(studentId.ToString())))
            .Select(c => new CourseRecord(
                c.Id,
                c.CourseName
            ));
    }
}