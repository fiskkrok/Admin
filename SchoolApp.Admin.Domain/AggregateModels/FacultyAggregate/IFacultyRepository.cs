

namespace SchoolApp.Admin.Domain.AggregateModels.FacultyAggregate;
public interface IFacultyRepository : IRepository<Faculty>
{
    Faculty Add(Faculty faculty);

    void Update(Faculty faculty);

    Task<Faculty> GetAsync(int facultyId);
}
