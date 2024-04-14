

using MediatR;
using SchoolApp.Admin.Application.Queries.Courses;
using SchoolApp.Admin.Services.Services;


namespace SchoolApp.Admin.WebAPI.Endpoints.Course;

public class CourseServices(ILogger<CourseServices> logger, IMediator mediator, ICourseQueries queries, IIdentityService identityService)
{
    public ILogger<CourseServices> Logger { get;  } = logger;
    public IMediator Mediator { get;  } = mediator;
    public ICourseQueries Queries { get; } = queries;
    public IIdentityService IdentityService { get; } = identityService;
}