using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolApp.Admin.Infrastructure.Repositories;
public class FacultyRepository : IFacultyRepository
{
    private readonly AdminDbContext _context;

    public IUnitOfWork UnitOfWork => _context;

    public FacultyRepository(AdminDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public Faculty Add(Faculty faculty)
    {
        return _context.Faculties.Add(faculty).Entity;

    }

    public async Task<Faculty> GetAsync(int facultyId)
    {
        var faculty = await _context.Faculties.FindAsync(facultyId);

        if (faculty != null)
        {
            await _context.Entry(faculty).Collection(i => i.CourseAssignments).LoadAsync();
        }

        return faculty;
    }

    public void Update(Faculty faculty)
    {
        _context.Entry(faculty).State = EntityState.Modified;
    }
}
