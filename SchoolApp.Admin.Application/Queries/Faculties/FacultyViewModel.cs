using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SchoolApp.Admin.Application.Queries.CoursesAssignment;

namespace SchoolApp.Admin.Application.Queries.Faculties;
public record Faculty
{
    public string FacultyId { get; init ; }
    public string? FirstName { get; init ; }
    public string? LastName { get; set; }
    public string? Department { get; set; }
    public  ICollection<CourseAssignment> CourseAssignments { get; init; }
}
