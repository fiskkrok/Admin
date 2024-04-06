using System.Linq;

using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using SchoolApp.Admin.Infrastructure.Data;

namespace SchoolApp.Admin.Application.Queries.Courses;

public class CourseQueries(AdminDbContext context) : ICourseQueries
{
    public async Task<Course> GetCourseByIdAsync(int courseId)
    {
        var course = await context.Courses
            .Include(o => o.Enrollments)
            .FirstOrDefaultAsync(o => o.Id == courseId);
        if (course is null)
            throw new KeyNotFoundException();
        return new Course
        {
            CourseCode = course.CourseCode,
            CourseId = course.CourseId,
            CourseName = course.CourseName,
            Credits = course.Credits,
            Description = course.Description,

        };

    }

    public async Task<IEnumerable<Course>> GetAllCourses()
    {
        return await context.Courses.Where(c => c.Status.Equals(true))
            .Include(c => c.Enrollments)
            .Include(c => c.CourseAssignments)
            .Include(c => c.Students)
            .Select(o => new Course
        {
            CourseId = o.CourseId,
            Credits = o.Credits,
            CourseName = o.CourseName,
            CourseAssignments = {},
            CourseCode = o.CourseCode,
            Description = o.Description,
            Enrollments = { },
            Students = {}

        }).ToListAsync();
    }

    public async Task<IEnumerable<Course>> GetAllCoursesStudent(int studentId)
    {
        return await context.Courses
            .Where(o => o.Students.Any(a => a.StudentId.Equals(studentId.ToString()))).Select(o => new Course
            {
                CourseId = o.CourseId,
                Credits = o.Credits,
                CourseName = o.CourseName,
            }).ToListAsync();
    }
}