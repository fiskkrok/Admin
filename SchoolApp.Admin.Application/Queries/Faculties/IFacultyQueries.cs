using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SchoolApp.Admin.Application.Queries.CoursesAssignment;

namespace SchoolApp.Admin.Application.Queries.Faculties;

public interface IFacultyQueries
{
    Task<IEnumerable<Faculty>> GetAllFacultiesAsync();
    Task<Faculty> GetFacultyByIdAsync(string facultyId);
    //Task<IEnumerable<Faculty>> GetFacultiesByDepartmentAsync(string department);
    //Task<IEnumerable<Faculty>> GetFacultiesByNameAsync(string name);
    //Task<IEnumerable<CourseAssignment>> GetFacultyCourseAssignmentsAsync(string facultyId);
}

