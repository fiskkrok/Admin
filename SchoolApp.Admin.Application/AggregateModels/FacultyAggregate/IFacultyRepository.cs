using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SchoolApp.Admin.Application.SeedWork;
using Admin.Domain.SeedWork;

namespace SchoolApp.Admin.Application.AggregateModels.FacultyAggregate;
public interface IFacultyRepository : IRepository<Faculty>
{
    IEnumerable<Faculty> GetAll();
    Task<Faculty> GetAsync(int id);
    Task<Faculty> AddAsync(Faculty faculty);
    Task<Faculty> UpdateAsync(Faculty faculty);
    Task<Faculty> DeleteAsync(int id);
    Task<int> CountAsync();
}
