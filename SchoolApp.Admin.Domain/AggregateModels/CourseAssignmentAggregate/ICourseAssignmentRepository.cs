

namespace SchoolApp.Admin.Domain.AggregateModels.CourseAssignmentAggregate;
public interface ICourseAssignmentRepository : IRepository<CourseAssignment>
{
    IQueryable<CourseAssignment> GetAll();
    Task<CourseAssignment> GetAsync(int id);
    Task<CourseAssignment> AddAsync(CourseAssignment courseAssignment);
    Task<CourseAssignment> UpdateAsync(CourseAssignment courseAssignment);
    Task<CourseAssignment> DeleteAsync(int id);
}
