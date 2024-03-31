using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolApp.Admin.Application.Queries.Faculties;
public interface IFacultyQueries
{
    IQueryable<FacultyRecord> GetAllFacultiesAsync();
    Task<FacultyRecord> GetFacultyByIdAsync(Guid facultyId);
}
