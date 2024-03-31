using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolApp.Admin.Application.Queries.Student;
public interface IStudentQueries
{
    Task<StudentRecord> GetStudentByIdAsync(int studentId);
    IEnumerable<StudentRecord>? GetAllStudents();
}
