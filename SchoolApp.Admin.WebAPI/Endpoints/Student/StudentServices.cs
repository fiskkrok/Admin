using MediatR;
using SchoolApp.Admin.Application.Queries.Student;

namespace SchoolApp.Admin.WebAPI.Endpoints.Student;

public class StudentServices(ILogger logger, IMediator mediator, IStudentQueries queries)
{
public ILogger Logger { get; set; } = logger;
public IMediator Mediator { get; set; } = mediator;
public IStudentQueries Queries { get; set; } = queries;
}