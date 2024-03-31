
using Ardalis.Specification;

namespace SchoolApp.Admin.Application.Specifications;
public class CourseNameSpecification : Specification<Course>
{
    public CourseNameSpecification(string courseName)
    {
        Query.Where(course => course.CourseName == courseName);
    }
}
