using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using FluentValidation;
using Microsoft.Extensions.Logging;
using SchoolApp.Admin.Application.Commands.Course;
using SchoolApp.Admin.Application.Commands.CourseAssignment;

namespace SchoolApp.Admin.Application.Validations;
public class CreateCourseAssignmentCommandValidator : AbstractValidator<CreateCourseAssignmentCommand>
{
    public CreateCourseAssignmentCommandValidator(ILogger<CreateCourseAssignmentCommandValidator> logger)
    {
        RuleFor(command => command.CourseId).NotEmpty();
        RuleFor(command => command.FacultyId).NotEmpty();
        RuleFor(command => command.AssignmentType).NotEmpty();
        RuleFor(command => command.AssignmentId).NotEmpty();

        if (logger.IsEnabled(LogLevel.Trace))
        {
            logger.LogTrace("INSTANCE CREATED - {ClassName}", GetType().Name);
        }
    }
}