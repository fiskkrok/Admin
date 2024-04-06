using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using FluentValidation;
using Microsoft.Extensions.Logging;
using SchoolApp.Admin.Application.Commands.Course;
using SchoolApp.Admin.Application.Commands.Enrollment;

namespace SchoolApp.Admin.Application.Validations;
public class CreateEnrollmentCommandValidator : AbstractValidator<CreateEnrollmentCommand>
{
    public CreateEnrollmentCommandValidator(ILogger<CreateEnrollmentCommandValidator> logger)
    {
        RuleFor(command => command.CourseId).NotEmpty();


        if (logger.IsEnabled(LogLevel.Trace))
        {
            logger.LogTrace("INSTANCE CREATED - {ClassName}", GetType().Name);
        }
    }
}