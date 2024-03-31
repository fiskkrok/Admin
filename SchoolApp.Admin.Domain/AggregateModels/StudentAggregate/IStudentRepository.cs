
namespace SchoolApp.Admin.Domain.AggregateModels.StudentAggregate;
public interface IStudentRepository : IRepository<Student>
{
    IQueryable<Student> GetAll();
    Task<Student> GetAsync(int id);
    Task<Student> AddAsync(Student student);
    Task<Student> UpdateAsync(Student student);
    Task<Student> DeleteAsync(int id);



}
