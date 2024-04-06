using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using FluentValidation;

using Microsoft.Extensions.Logging;

using SchoolApp.Admin.Application.Commands.Course;

namespace SchoolApp.Admin.Application.Validations;
public class CreateCourseCommandValidator : AbstractValidator<CreateCourseCommand>
{
    public CreateCourseCommandValidator(ILogger<CreateCourseCommandValidator> logger)
    {
        RuleFor(command => command.CourseCode).NotEmpty();
        RuleFor(command => command.CourseName).NotEmpty();
        RuleFor(command => command.Credits).NotEmpty();
        RuleFor(command => command.Description).NotEmpty();

        if (logger.IsEnabled(LogLevel.Trace))
        {
            logger.LogTrace("INSTANCE CREATED - {ClassName}", GetType().Name);
        }
    }
}