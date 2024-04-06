using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using FluentValidation;
using Microsoft.Extensions.Logging;
using SchoolApp.Admin.Application.Commands.Course;
using SchoolApp.Admin.Application.Commands.Student;

namespace SchoolApp.Admin.Application.Validations;
public class CreateStudentCommandValidator : AbstractValidator<CreateStudentCommand>
{
    public CreateStudentCommandValidator(ILogger<CreateStudentCommandValidator> logger)
    {
        RuleFor(command => command.DateOfBirth).NotEmpty();
        RuleFor(command => command.Email).NotEmpty();
        RuleFor(command => command.EnrollmentDate).NotEmpty();
        RuleFor(command => command.FirstName).NotEmpty();
        RuleFor(command => command.LastName).NotEmpty();

        if (logger.IsEnabled(LogLevel.Trace))
        {
            logger.LogTrace("INSTANCE CREATED - {ClassName}", GetType().Name);
        }
    }
  
}
