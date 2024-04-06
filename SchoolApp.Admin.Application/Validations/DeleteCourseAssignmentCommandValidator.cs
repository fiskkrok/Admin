using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using FluentValidation;

using Microsoft.Extensions.Logging;
using SchoolApp.Admin.Application.Commands.CourseAssignment;

namespace SchoolApp.Admin.Application.Validations;
public class DeleteCourseAssignmentCommandValidator: AbstractValidator<DeleteCourseAssignmentCommand>
{
    public DeleteCourseAssignmentCommandValidator(ILogger<DeleteCourseAssignmentCommandValidator> logger)
    {
        RuleFor(order => order.CourseAssignmentId).NotEmpty().WithMessage("No orderId found");

        if (logger.IsEnabled(LogLevel.Trace))
        {
            logger.LogTrace("INSTANCE CREATED - {ClassName}", GetType().Name);
        }
    }
}