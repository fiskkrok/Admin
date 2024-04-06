

namespace SchoolApp.Admin.Domain.AggregateModels.CourseAssignmentAggregate;
public interface ICourseAssignmentRepository : IRepository<CourseAssignment>
{
    CourseAssignment Add(CourseAssignment assignment);

    void Update(CourseAssignment assignment);

    Task<CourseAssignment> GetAsync(int assignmentId);
}
