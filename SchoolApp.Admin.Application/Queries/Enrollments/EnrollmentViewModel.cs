using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SchoolApp.Admin.Application.Queries.Courses;


namespace SchoolApp.Admin.Application.Queries.Enrollments;
public record Enrollment
{
   
    public string? EnrollmentId { get; set; } 


    public string? StudentId { get; set; }


    public string? CourseId { get; set; }

    public DateOnly? EnrollmentDate { get; set; }

    public Course? Course { get; set; }

    public Students.Student? Student { get; set; }
}
