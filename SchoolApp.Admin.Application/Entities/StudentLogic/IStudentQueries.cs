using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Admin.Application.AggregateModels.StudentAggregate;
using Microsoft.VisualBasic;

namespace Admin.Application.Entities.StudentLogic;
public interface IStudentQueries
{
    Task<Student> GetStudentAsync(int id);

    //Task<IEnumerable<StudentSummary>> GetStudentsFromUserAsync(string userId);

    Task<IEnumerable<CardType>> GetCardTypesAsync();
}
