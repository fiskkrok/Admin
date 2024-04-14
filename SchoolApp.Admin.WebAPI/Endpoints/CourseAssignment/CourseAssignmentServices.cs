using MediatR;

using SchoolApp.Admin.Application.Queries.CourseAssignments;
using SchoolApp.Admin.Application.Queries.CoursesAssignment;
using SchoolApp.Admin.Services.Services;


namespace SchoolApp.Admin.WebAPI.Endpoints.CourseAssignment;

public class CourseAssignmentServices
    (ILogger<CourseAssignmentServices> logger, IMediator mediator, ICourseAssignmentQueries queries , IIdentityService identityService)
{
    public ILogger<CourseAssignmentServices> Logger { get; set; } = logger;
    public IMediator Mediator { get; set; } = mediator;
    public ICourseAssignmentQueries Queries { get; set; } = queries;
    public IIdentityService IdentityService { get; } = identityService;
}

