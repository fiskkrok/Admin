using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolApp.Admin.Infrastructure.Repositories;
public class CourseAssignmentRepository : ICourseAssignmentRepository
{
    private readonly AdminDbContext _context;

    public IUnitOfWork UnitOfWork => _context;
    public CourseAssignmentRepository(AdminDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public CourseAssignment Add(CourseAssignment assignment)
    {
        return _context.CourseAssignments.Add(assignment).Entity;
    }

    public void Update(CourseAssignment assignment)
    {
        _context.Entry(assignment).State = EntityState.Modified;
    }

    public async Task<CourseAssignment> GetAsync(int assignmentId)
    {
        var order = await _context.CourseAssignments.FindAsync(assignmentId);

        if (order != null)
        {
            await _context.Entry(order).GetDatabaseValuesAsync();
        }

        return order;
    }
}
