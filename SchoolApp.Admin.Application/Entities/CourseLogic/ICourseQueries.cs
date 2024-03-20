using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Admin.Application.Entities.StudentLogic;
using Admin.Application.AggregateModels.CourseAggregate;
using Admin.Application.AggregateModels.StudentAggregate;

namespace Admin.Application.Entities.CourseLogic;
public interface ICourseQueries
{
    Task<Course> GetCourseAsync(int id);

    //Task<IEnumerable<StudentSummary>> GetStudentsFromUserAsync(string userId);

}
