using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Admin.Domain.SeedWork;
using Admin.Application.AggregateModels.StudentAggregate;
using Admin.Application.SeedWork;

namespace Admin.Application.AggregateModels.StudentAggregate;
public interface IStudentRepository : IRepository<Student>
{
    IQueryable<Student> GetAll();
    Task<Student> GetAsync(int id);
    Task<Student> AddAsync(Student student);
    Task<Student> UpdateAsync(Student student);
    Task<Student> DeleteAsync(int id);



}
