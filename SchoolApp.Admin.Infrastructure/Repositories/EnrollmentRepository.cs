using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolApp.Admin.Infrastructure.Repositories;
public class EnrollmentRepository : IEnrollmentRepository
{
    private readonly AdminDbContext _context;

    public IUnitOfWork UnitOfWork => _context;

    public EnrollmentRepository(AdminDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public Enrollment Add(Enrollment enrollment)
    {
        return _context.Enrollments.Add(enrollment).Entity;

    }

    public async Task<Enrollment> GetAsync(int enrollmentId)
    {
        var enrollment = await _context.Enrollments.FindAsync(enrollmentId);

        if (enrollment != null)
        {
            await _context.Entry(enrollment).GetDatabaseValuesAsync();
        }

        return enrollment;
    }

    public void Update(Enrollment enrollment)
    {
        _context.Entry(enrollment).State = EntityState.Modified;
    }
}
