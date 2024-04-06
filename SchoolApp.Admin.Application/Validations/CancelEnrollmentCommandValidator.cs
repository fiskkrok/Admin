using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using Microsoft.Extensions.Logging;

using SchoolApp.Admin.Application.Commands.Enrollment;

namespace SchoolApp.Admin.Application.Validations;
public class CancelEnrollmentCommandValidator : AbstractValidator<CancelEnrollmentCommand>
{
    public CancelEnrollmentCommandValidator(ILogger<CancelEnrollmentCommandValidator> logger)
    {
        RuleFor(command => command.EnrollmentNumber).NotEmpty();
       

        if (logger.IsEnabled(LogLevel.Trace))
        {
            logger.LogTrace("INSTANCE Deleted - {ClassName}", GetType().Name);
        }
    }

  

}
