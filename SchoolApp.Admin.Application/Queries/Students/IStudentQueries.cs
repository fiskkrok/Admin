using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SchoolApp.Admin.Domain.AggregateModels.StudentAggregate;

namespace SchoolApp.Admin.Application.Queries.Students;

public interface IStudentQueries
{
    Task<IEnumerable<Student>> GetAllStudentsAsync();
    Task<Student> GetStudentByIdAsync(string studentId);
    Task<IEnumerable<Student>> GetStudentsByStatusAsync(StudentStatus status);
    // Additional query contracts can be added as needed
}

