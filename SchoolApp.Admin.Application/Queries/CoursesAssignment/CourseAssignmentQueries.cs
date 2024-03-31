using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SchoolApp.Admin.Application.Queries.CoursesAssignment;
using SchoolApp.Admin.Infrastructure.Data;

namespace SchoolApp.Admin.Application.Queries.CoursesAssignment;
public class CourseAssignmentQueries(AdminDbContext context) : ICourseAssignmentQueries
{
    public async Task<CourseAssignmentRecord> GetCourseAssignmentByIdAsync(int courseAssignmentId)
    {
        var courseAssignment = await context.CourseAssignments
            .Include(o => o.Course)
            .FirstOrDefaultAsync(o => o.Id == courseAssignmentId);

        return courseAssignment is null
            ? throw new KeyNotFoundException()
            : new CourseAssignmentRecord(int.Parse(courseAssignment.AssignmentId), courseAssignment.FacultyId,
                courseAssignment.CourseId,courseAssignment.AssignmentType);
    }

    public Task<IEnumerable<CourseAssignmentRecord>> GetAllCourseAssignmentAsync()
    {
        return Task.FromResult<IEnumerable<CourseAssignmentRecord>>(context.CourseAssignments.ToListAsync() as IEnumerable<CourseAssignmentRecord>);
    }

}
