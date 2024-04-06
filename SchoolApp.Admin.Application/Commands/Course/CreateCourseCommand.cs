using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
    [Required]
    public int CourseId { get; set; }

    [DataMember]
    [StringLength(100)]
    public string? Description { get; set; }

    [DataMember]
    [Range(1, 10)]
    public int Credits { get; set; }

    [DataMember]
    [Required]
    [StringLength(100)]
    public string? CourseName { get; set; }

    [DataMember]
    [Required]
    [StringLength(10)]
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
