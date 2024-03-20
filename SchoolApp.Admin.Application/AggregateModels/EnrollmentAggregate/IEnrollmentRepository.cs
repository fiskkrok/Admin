using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Admin.Application.SeedWork;
using Admin.Domain.SeedWork;

namespace Admin.Application.AggregateModels.EnrollmentAggregate;
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
