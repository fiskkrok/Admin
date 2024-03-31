using MediatR;
using SchoolApp.Admin.Application.Queries.CoursesAssignment;

namespace SchoolApp.Admin.WebAPI.Endpoints.CourseAssignment;

public class CourseAssignmentServices
    (ILogger logger, IMediator mediator, CourseAssignmentQueries queries)
    {
    public ILogger Logger { get; set; } = logger;
    public IMediator Mediator { get; set; } = mediator;
    public CourseAssignmentQueries Queries { get; set; } = queries;
}

