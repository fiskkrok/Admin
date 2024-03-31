using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolApp.Admin.Application.Commands.Course;

public record DeleteCourseCommand(int CourseId) : IRequest<bool>;

