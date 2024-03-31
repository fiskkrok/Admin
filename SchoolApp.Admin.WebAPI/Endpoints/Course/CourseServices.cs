

using MediatR;
using SchoolApp.Admin.Application.Queries.Courses;

namespace SchoolApp.Admin.WebAPI.Endpoints.Course;

public class CourseServices(ILogger logger, IMediator mediator, ICourseQueries queries)
{
    public ILogger Logger { get; set; } = logger;
    public IMediator Mediator { get; set; } = mediator;
    public ICourseQueries Queries { get; set; } = queries;
}