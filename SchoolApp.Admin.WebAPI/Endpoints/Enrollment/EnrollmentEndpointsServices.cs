using MediatR;
using SchoolApp.Admin.Application.Queries.Enrollments;
using SchoolApp.Admin.Services.Services;

namespace SchoolApp.Admin.WebAPI.Endpoints.Enrollment;

public class EnrollmentServices
    (ILogger<EnrollmentServices> logger, IMediator mediator, IEnrollmentQueries queries, IIdentityService identityService)
{
    public ILogger<EnrollmentServices> Logger { get; set; } = logger;
    public IMediator Mediator { get; set; } = mediator;
    public IEnrollmentQueries Queries { get; set; } = queries;
    public IIdentityService IdentityService { get; } = identityService;
}

