using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolApp.Admin.Infrastructure.Repositories;
public class StudentRepository : IStudentRepository
{
    private readonly AdminDbContext _context;

    public IUnitOfWork UnitOfWork => _context;

    public StudentRepository(AdminDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public Student Add(Student student)
    {
        return _context.Students.Add(student).Entity;

    }

    public async Task<Student> GetAsync(int studentId)
    {
        var student = await _context.Students.FindAsync(studentId);

        if (student != null)
        {
            await _context.Entry(student).Collection(i => i.Courses).LoadAsync();
        }

        return student;
    }

    public void Update(Student student)
    {
        _context.Entry(student).State = EntityState.Modified;
    }
}
