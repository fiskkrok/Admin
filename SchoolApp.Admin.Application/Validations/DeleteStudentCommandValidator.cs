using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using FluentValidation;

using Microsoft.Extensions.Logging;

using SchoolApp.Admin.Application.Commands.Student;

namespace SchoolApp.Admin.Application.Validations;
public class DeleteStudentCommandValidator : AbstractValidator<DeleteStudentCommand>
{
    public DeleteStudentCommandValidator
        (ILogger<DeleteStudentCommandValidator> logger)
    {
        RuleFor(order => order.StudentId).NotEmpty().WithMessage("No orderId found");

        if (logger.IsEnabled(LogLevel.Trace))
        {
            logger.LogTrace("INSTANCE Deleted - {ClassName}", GetType().Name);
        }
    }
}