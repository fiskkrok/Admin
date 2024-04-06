using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using FluentValidation;

using Microsoft.Extensions.Logging;

using SchoolApp.Admin.Application.Commands.Course;

namespace SchoolApp.Admin.Application.Validations;
public class DeleteCourseCommandValidator : AbstractValidator<DeleteCourseCommand>
{
    public DeleteCourseCommandValidator(ILogger<DeleteCourseCommandValidator> logger)
    {
        RuleFor(order => order.CourseId).NotEmpty().WithMessage("No orderId found");

        if (logger.IsEnabled(LogLevel.Trace))
        {
            logger.LogTrace("INSTANCE Deleted - {ClassName}", GetType().Name);
        }
    }
}