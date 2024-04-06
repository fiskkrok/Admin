using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;
using SchoolApp.Admin.Application.Queries.Courses;
using SchoolApp.Admin.Application.Queries.Faculties;

namespace SchoolApp.Admin.Application.Queries.CoursesAssignment;
public record CourseAssignment
{
    public string AssignmentId { get; set; }

    public string FacultyId { get; set; }

    public string CourseId { get; set; }

    public string? AssignmentType { get; set; }

    public Course? Course { get; set; }

    public Faculty? Faculty { get; set; }
}
