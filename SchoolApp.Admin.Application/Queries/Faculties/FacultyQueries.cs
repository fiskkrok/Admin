

using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using SchoolApp.Admin.Infrastructure.Data;

namespace SchoolApp.Admin.Application.Queries.Faculties;
public class FacultyQueries(AdminDbContext context, IMapper mapper) : IFacultyQueries
{
    public IQueryable<FacultyRecord> GetAllFacultiesAsync()
    {
        var faculties = context.Faculties.AsQueryable();
        return mapper.ProjectTo<FacultyRecord>(faculties);
    }

    public async Task<FacultyRecord> GetFacultyByIdAsync(Guid facultyId)
    {
        var faculty = await context.Faculties
            .Include(o => o.FirstName)
            .FirstOrDefaultAsync(o => o.FacultyId == facultyId.ToString());

        return faculty is null
            ? throw new KeyNotFoundException()
            : new FacultyRecord(faculty.Id);
    }
}
