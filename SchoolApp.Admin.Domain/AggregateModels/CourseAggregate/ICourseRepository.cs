namespace SchoolApp.Admin.Domain.AggregateModels.CourseAggregate;
public interface ICourseRepository : IRepository<Course>
{
    IQueryable<Course> GetAll();
    Task<Course> GetAsync(int id);
    Task<Course> AddAsync(Course course);
    Task<Course> UpdateAsync(Course course);
    Task<Course> DeleteAsync(int id);
}
