using MediatR;
using SchoolApp.Admin.Application.Queries.Faculties;


namespace SchoolApp.Admin.WebAPI.Endpoints.Faculty;

public class FacultyServices(ILogger logger, IMediator mediator, FacultyQueries queries)
{
    public ILogger Logger { get; set; } = logger;
    public IMediator Mediator { get; set; } = mediator;
    public FacultyQueries Queries { get; set; } = queries;
}
