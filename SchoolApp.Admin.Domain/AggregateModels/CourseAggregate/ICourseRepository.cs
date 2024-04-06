namespace SchoolApp.Admin.Domain.AggregateModels.CourseAggregate;
public interface ICourseRepository : IRepository<Course>
{
    Course Add(Course course);

    void Update(Course course);

    Task<Course> GetAsync(int courseId);
}
