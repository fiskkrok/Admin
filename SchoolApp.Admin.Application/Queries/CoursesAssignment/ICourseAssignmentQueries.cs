using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolApp.Admin.Application.Queries.CoursesAssignment;
public interface ICourseAssignmentQueries{
Task<CourseAssignmentRecord> GetCourseAssignmentByIdAsync(int courseAssignmentId);
Task<IEnumerable<CourseAssignmentRecord>> GetAllCourseAssignmentAsync();
}

