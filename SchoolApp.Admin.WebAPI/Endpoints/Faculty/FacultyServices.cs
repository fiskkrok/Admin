using MediatR;
using SchoolApp.Admin.Application.Queries.Faculties;
using SchoolApp.Admin.Infrastructure.Identity.Services;


namespace SchoolApp.Admin.WebAPI.Endpoints.Faculty;

public class FacultyServices(ILogger<FacultyServices> logger, IMediator mediator, IFacultyQueries queries /*, IIdentityService identityService*/)
{
    public ILogger<FacultyServices> Logger { get; set; } = logger;
    public IMediator Mediator { get; set; } = mediator;
    public IFacultyQueries Queries { get; set; } = queries;
    //public IIdentityService IdentityService { get; } = identityService;
}
