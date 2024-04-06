

namespace SchoolApp.Admin.Domain.AggregateModels.EnrollmentAggregate;
public interface IEnrollmentRepository : IRepository<Enrollment>
{

    Enrollment Add(Enrollment enrollment);

    void Update(Enrollment enrollment);

    Task<Enrollment> GetAsync(int enrollmentId);
}
