using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolApp.Admin.Infrastructure.Repositories;
public class CourseRepository : ICourseRepository
{
    private readonly AdminDbContext _context;

    public IUnitOfWork UnitOfWork => _context;

    public CourseRepository(AdminDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public Course Add(Course course)
    {
        return _context.Courses.Add(course).Entity;

    }

    public async Task<Course> GetAsync(int courseId)
    {
        var course = await _context.Courses.FindAsync(courseId);

        if (course != null)
        {
            await _context.Entry(course)
                .Collection(i => i.CourseAssignments).LoadAsync();
        }

        return course;
    }

    public void Update(Course course)
    {
        _context.Entry(course).State = EntityState.Modified;
    }
}
