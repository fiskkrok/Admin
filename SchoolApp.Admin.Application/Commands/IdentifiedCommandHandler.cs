﻿using Admin.Application.Commands;

using Microsoft.Extensions.Logging;
using SchoolApp.Admin.Application.Commands.CourseAssignment;
using SchoolApp.Admin.Application.Commands.Enrollment;
using SchoolApp.Admin.Application.Commands.Faculty;
using SchoolApp.Admin.Application.Commands.Student;
using SchoolApp.Admin.Infrastructure;
using SchoolApp.Admin.Infrastructure.Idempotency;
using SchoolApp.EventBus.Extensions;

namespace SchoolApp.Admin.Application.Commands;

/// <summary>
/// Provides a base implementation for handling duplicate request and ensuring idempotent updates, in the cases where
/// a requestid sent by client is used to detect duplicate requests.
/// </summary>
/// <typeparam name="T">Type of the command handler that performs the operation if request is not duplicated</typeparam>
/// <typeparam name="R">Return value of the inner command handler</typeparam>
public abstract class IdentifiedCommandHandler<T, R> : IRequestHandler<IdentifiedCommand<T, R>, R>
    where T : IRequest<R>
{
    private readonly IMediator _mediator;
    private readonly IRequestManager _requestManager;
    private readonly ILogger<IdentifiedCommandHandler<T, R>> _logger;

    protected IdentifiedCommandHandler(
        IMediator mediator,
        IRequestManager requestManager,
        ILogger<IdentifiedCommandHandler<T, R>> logger)
    {
        ArgumentNullException.ThrowIfNull(logger);
        _mediator = mediator;
        _requestManager = requestManager;
        _logger = logger;
    }

    /// <summary>
    /// Creates the result value to return if a previous request was found
    /// </summary>
    /// <returns></returns>
    protected abstract R CreateResultForDuplicateRequest();

    /// <summary>
    /// This method handles the command. It just ensures that no other request exists with the same ID, and if this is the case
    /// just enqueues the original inner command.
    /// </summary>
    /// <param name="message">IdentifiedCommand which contains both original command & request ID</param>
    /// <param name="cancellationToken">IdentifiedCommand which contains both original command & request ID</param>
    /// <returns>Return value of inner command or default value if request same ID was found</returns>
    public async Task<R> Handle(IdentifiedCommand<T, R> message, CancellationToken cancellationToken)
    {
        var alreadyExists = await _requestManager.ExistAsync(message.Id);
        if (alreadyExists)
        {
            return CreateResultForDuplicateRequest();
        }
        else
        {
            await _requestManager.CreateRequestForCommandAsync<T>(message.Id);
            try
            {
                var command = message.Command;
                var commandName = command.GetGenericTypeName();
                var idProperty = string.Empty;
                var commandId = string.Empty;

                switch (command)
                {
                    case CreateEnrollmentCommand createEnrollmentCommand:
                        idProperty = nameof(createEnrollmentCommand.CourseId);
                        commandId = createEnrollmentCommand.CourseId;
                        break;

                    case CancelEnrollmentCommand cancelEnrollmentCommand:
                        idProperty = nameof(cancelEnrollmentCommand.EnrollmentNumber);
                        commandId = $"{cancelEnrollmentCommand.EnrollmentNumber}";
                        break;

                    case ApprovedEnrollmentCommand approvedEnrollmentCommand:
                        idProperty = nameof(approvedEnrollmentCommand.EnrollmentNumber);
                        commandId = $"{approvedEnrollmentCommand.EnrollmentNumber}";
                        break;
                    case CreateStudentCommand createStudentCommand:
                        idProperty = nameof(createStudentCommand.FirstName);
                        commandId = createStudentCommand.FirstName;
                        break;

                    case CreateFacultyCommand createFacultyCommand:
                        idProperty = nameof(createFacultyCommand.FirstName);
                        commandId = createFacultyCommand.FirstName;
                        break;

                    case CreateCourseAssignmentCommand createCourseAssignmentCommand:
                        idProperty = nameof(createCourseAssignmentCommand.CourseId);
                        commandId = createCourseAssignmentCommand.CourseId.ToString();
                        break;

                    default:
                        idProperty = "Id?";
                        commandId = "n/a";
                        break;
                }

                _logger.LogInformation(
                    "Sending command: {CommandName} - {IdProperty}: {CommandId} ({@Command})",
                    commandName,
                    idProperty,
                    commandId,
                    command);

                // Send the embedded business command to mediator so it runs its related CommandHandler 
                var result = await _mediator.Send(command, cancellationToken);

                _logger.LogInformation(
                    "Command result: {@Result} - {CommandName} - {IdProperty}: {CommandId} ({@Command})",
                    result,
                    commandName,
                    idProperty,
                    commandId,
                    command);

                return result;
            }
            catch
            {
                return default;
            }
        }
    }
}