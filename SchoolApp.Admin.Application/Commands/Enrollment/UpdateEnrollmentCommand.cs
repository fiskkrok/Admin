using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolApp.Admin.Application.Commands.Enrollment;
public class UpdateEnrollmentCommand(int EnrollmentId, int? StudentId, int? CourseId, DateOnly? EnrollmentDate
    ) : IRequest<bool>;
