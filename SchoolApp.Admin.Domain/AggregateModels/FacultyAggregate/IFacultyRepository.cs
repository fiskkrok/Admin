

namespace SchoolApp.Admin.Domain.AggregateModels.FacultyAggregate;
public interface IFacultyRepository : IRepository<Faculty>
{
    IEnumerable<Faculty> GetAll();
    Task<Faculty> GetAsync(int id);
    Task<Faculty> AddAsync(Faculty faculty);
    Task<Faculty> UpdateAsync(Faculty faculty);
    Task<Faculty> DeleteAsync(int id);
    Task<int> CountAsync();
}
