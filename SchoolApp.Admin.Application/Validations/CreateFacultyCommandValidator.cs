using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using FluentValidation;
using Microsoft.Extensions.Logging;
using SchoolApp.Admin.Application.Commands.Course;
using SchoolApp.Admin.Application.Commands.Faculty;

namespace SchoolApp.Admin.Application.Validations;
public class CreateFacultyCommandValidator : AbstractValidator<CreateFacultyCommand>
{
    public CreateFacultyCommandValidator(ILogger<CreateFacultyCommandValidator> logger)
    {
        RuleFor(command => command.Department).NotEmpty();
        RuleFor(command => command.FirstName).NotEmpty();
        RuleFor(command => command.LastName).NotEmpty();

        if (logger.IsEnabled(LogLevel.Trace))
        {
            logger.LogTrace("INSTANCE CREATED - {ClassName}", GetType().Name);
        }
    }
}
