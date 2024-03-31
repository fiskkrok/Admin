using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Http.HttpResults;

namespace SchoolApp.Admin.Application.Commands.Course;

[DataContract]
public class CreateCourseCommand
    : IRequest<bool>
{
    [DataMember]
    public int CourseId { get; set; }
    [DataMember]
    public string? Description { get; set; }

    [DataMember]
    public int Credits { get; set; }

    [DataMember]
    public string? CourseName { get; set; }

    [DataMember]
    public string? CourseCode { get; set; }

    public CreateCourseCommand(string? courseCode, string? courseName, int credits, string? description, int courseId)
    {
        CourseCode = courseCode;
        CourseName = courseName;
        Credits = credits;
        Description = description;
        CourseId = courseId;
    }
}
