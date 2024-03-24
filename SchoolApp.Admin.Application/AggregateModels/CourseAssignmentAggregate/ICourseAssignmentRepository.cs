using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SchoolApp.Admin.Application.SeedWork;
using Admin.Domain.SeedWork;

namespace SchoolApp.Admin.Application.AggregateModels.CourseAssignmentAggregate;
public interface ICourseAssignmentRepository : IRepository<CourseAssignment>
{
    IQueryable<CourseAssignment> GetAll();
    Task<CourseAssignment> GetAsync(int id);
    Task<CourseAssignment> AddAsync(CourseAssignment courseAssignment);
    Task<CourseAssignment> UpdateAsync(CourseAssignment courseAssignment);
    Task<CourseAssignment> DeleteAsync(int id);
}
