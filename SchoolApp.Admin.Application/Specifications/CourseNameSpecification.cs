using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SchoolApp.Admin.Application.AggregateModels.CourseAggregate;
using Ardalis.Specification;

namespace Admin.Application.Specifications;
public class CourseNameSpecification : Specification<Course>
{
    public CourseNameSpecification(string courseName)
    {
        Query.Where(course => course.CourseName == courseName);
    }
}
