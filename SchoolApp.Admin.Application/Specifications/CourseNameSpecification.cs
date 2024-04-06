
using Ardalis.Specification;
using SchoolApp.Admin.Application.Queries.Courses;

namespace SchoolApp.Admin.Application.Specifications;
public class CourseNameSpecification : Specification<Course>
{
    public CourseNameSpecification(string courseName)
    {
        Query.Where(course => course.CourseName == courseName);
    }
}
