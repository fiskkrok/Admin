using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Admin.Application.Commands;
using FluentValidation;
using Microsoft.Extensions.Logging;
using SchoolApp.Admin.Application.Commands.Student;


namespace SchoolApp.Admin.Application.Validations;
public class IdentifiedCommandValidator : AbstractValidator<IdentifiedCommand<CreateStudentCommand, bool>>
{
    public IdentifiedCommandValidator(ILogger<IdentifiedCommandValidator> logger)
    {
        RuleFor(command => command.Id).NotEmpty();

        if (logger.IsEnabled(LogLevel.Trace))
        {
            logger.LogTrace("INSTANCE CREATED - {ClassName}", GetType().Name);
        }
    }
}
