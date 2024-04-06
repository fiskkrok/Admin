using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using FluentValidation;

using Microsoft.Extensions.Logging;

using SchoolApp.Admin.Application.Commands.Faculty;

namespace SchoolApp.Admin.Application.Validations;
public class DeleteFacultyCommandValidator:AbstractValidator<DeleteFacultyCommand>
{
    public DeleteFacultyCommandValidator(ILogger<DeleteFacultyCommandValidator> logger)
    {
        RuleFor(order => order.FacultyId).NotEmpty().WithMessage("No orderId found");

        if (logger.IsEnabled(LogLevel.Trace))
        {
            logger.LogTrace("INSTANCE Deleted - {ClassName}", GetType().Name);
        }
    }
}