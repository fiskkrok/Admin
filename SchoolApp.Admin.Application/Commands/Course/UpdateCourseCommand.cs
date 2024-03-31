using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolApp.Admin.Application.Commands.Course;
public class UpdateCourseCommand : IRequest<bool>
{
    public int CourseId { get; set; }
    public string? Description { get; set; }
    public int Credits { get; set; }
    public string? CourseName { get; set; }
    public string? CourseCode { get; set; }

    public UpdateCourseCommand(string? courseCode, string? courseName, int credits, string? description, int courseId)
    {
        CourseCode = courseCode;
        CourseName = courseName;
        Credits = credits;
        Description = description;
        CourseId = courseId;
    }
}
