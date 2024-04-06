
namespace SchoolApp.Admin.Domain.AggregateModels.StudentAggregate;
public interface IStudentRepository : IRepository<Student>
{
    Student Add(Student student);

    void Update(Student student);

    Task<Student> GetAsync(int studentId);

}
