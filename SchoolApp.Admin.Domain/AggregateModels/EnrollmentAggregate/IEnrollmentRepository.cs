

namespace SchoolApp.Admin.Domain.AggregateModels.EnrollmentAggregate;
public interface IEnrollmentRepository : IRepository<Enrollment>
{

    Enrollment GetById(int id);
    Enrollment Add(Enrollment enrollment);
    void Update(Enrollment enrollment);
    void Delete(int id);
    Task<Enrollment> GetByIdAsync(int id);
    Task<Enrollment> AddAsync(Enrollment enrollment);
    Task<Enrollment> UpdateAsync(Enrollment enrollment);
    Task<Enrollment> DeleteAsync(int id);
}
