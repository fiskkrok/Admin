using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

using SchoolApp.Admin.Application.Queries.CourseAssignments;
using SchoolApp.Admin.Application.Queries.Courses;
using SchoolApp.Admin.Application.Queries.CoursesAssignment;
using SchoolApp.Admin.Infrastructure.Data;

namespace SchoolApp.Admin.Application.Queries.CoursesAssignment;
public class CourseAssignmentQueries(AdminDbContext context) : ICourseAssignmentQueries
{

    public async Task<IEnumerable<CourseAssignment>> GetAllAssignments()
    {
        return  await context.CourseAssignments.Select(c => new CourseAssignment
        {
            AssignmentType = c.AssignmentType,
            AssignmentId = c.AssignmentId,
            CourseId = c.CourseId,
            FacultyId = c.FacultyId,

        }).ToListAsync();
     
        
    }

    public async Task<CourseAssignment> GetAssignmentByIdAsync(string assignmentId)
    {
        var assignment = await context.CourseAssignments
            .FirstOrDefaultAsync(o => o.Id == int.Parse(assignmentId));
        if (assignment is null)
            throw new KeyNotFoundException();
        return new CourseAssignment
        {
           CourseId = assignment.CourseId
           , AssignmentType = assignment.AssignmentType,
           FacultyId = assignment.FacultyId,
           AssignmentId = assignment.AssignmentId
        };
    }

    public async Task<IEnumerable<CourseAssignment>> GetAssignmentsByCourseIdAsync(string courseId)
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<CourseAssignment>> GetAssignmentsByFacultyIdAsync(string facultyId)
    {
        throw new NotImplementedException();
    }

    // Implement other methods similarly...
}

