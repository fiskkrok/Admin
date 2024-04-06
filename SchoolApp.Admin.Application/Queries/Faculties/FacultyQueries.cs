

using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using SchoolApp.Admin.Application.Queries.CoursesAssignment;
using SchoolApp.Admin.Infrastructure.Data;

namespace SchoolApp.Admin.Application.Queries.Faculties;
public class FacultyQueries(AdminDbContext context) : IFacultyQueries
{

    public async Task<IEnumerable<Faculty>> GetAllFacultiesAsync()
    {
        return await context.Faculties.Select(f => new Faculty
        {
            Department = f.Department,
            FirstName = f.FirstName,
            LastName = f.LastName,
            FacultyId = f.FacultyId
        }).ToListAsync();
    }

    public async Task<Faculty> GetFacultyByIdAsync(string facultyId)
    {
        var faculty = await context.Faculties
            .FirstOrDefaultAsync(f => f.FacultyId == facultyId);
        if (faculty == null) throw new KeyNotFoundException($"No faculty found with ID {facultyId}");
        return new Faculty
        {
            Department = faculty.Department,
            FirstName = faculty.FirstName,
            LastName = faculty.LastName,
            FacultyId = faculty.FacultyId
        };
    }

    public async Task<IEnumerable<Faculty>> GetFacultiesByDepartmentAsync(string department)
    {
        return await context.Faculties
            .Where(f => f.Department == department).Select(f => new Faculty
            {
                Department = f.Department,
                FirstName = f.FirstName,
                LastName = f.LastName,
                FacultyId = f.FacultyId
            }).ToListAsync();
    }

    public async Task<IEnumerable<Faculty>> GetFacultiesByNameAsync(string name)
    {
        return await context.Faculties
            .Where(f => f.FirstName.Contains(name) || f.LastName.Contains(name)).Select(f => new Faculty
            {
                Department = f.Department,
                FirstName = f.FirstName,
                LastName = f.LastName,
                FacultyId = f.FacultyId
            }).ToListAsync();
    }

    public async Task<IEnumerable<CourseAssignment>> GetFacultyCourseAssignmentsAsync(string facultyId)
    {
        //var faculty = await context.Faculties
        //    .Include(f => f.CourseAssignments)
        //    .FirstOrDefaultAsync(f => f.FacultyId == facultyId);

        //return faculty?.CourseAssignments ?? Enumerable.Empty<CourseAssignment>();
        throw new NotImplementedException();
    }
}



