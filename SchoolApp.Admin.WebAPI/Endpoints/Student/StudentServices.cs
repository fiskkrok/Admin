using MediatR;
using SchoolApp.Admin.Application.Queries.Students;
using SchoolApp.Admin.Services.Services;


namespace SchoolApp.Admin.WebAPI.Endpoints.Student;

public class StudentServices(ILogger<StudentServices> logger, IMediator mediator, IStudentQueries queries,  IIdentityService identityService)
{
public ILogger<StudentServices> Logger { get; set; } = logger;
public IMediator Mediator { get; set; } = mediator;
public IStudentQueries Queries { get; set; } = queries;
    public IIdentityService IdentityService { get; } = identityService;
}