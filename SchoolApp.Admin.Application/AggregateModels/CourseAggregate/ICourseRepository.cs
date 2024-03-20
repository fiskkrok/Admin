using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Admin.Application.SeedWork;
using Admin.Domain.SeedWork;

namespace Admin.Application.AggregateModels.CourseAggregate;
public interface ICourseRepository : IRepository<Course>
{
    IQueryable<Course> GetAll();
    Task<Course> GetAsync(int id);
    Task<Course> AddAsync(Course course);
    Task<Course> UpdateAsync(Course course);
    Task<Course> DeleteAsync(int id);
}
