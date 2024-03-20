using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Admin.Application.Entities.CourseLogic;
using Admin.Application.Entities.IdentityLogic;
using Microsoft.Extensions.Logging;

namespace Admin.Application.Entities.StudentLogic;
public class CourseService(
    IMediator mediator,
    ICourseQueries queries,
    IIdentityService identityService,
    ILogger<CourseService> logger)
{
    public IMediator Mediator { get; set; } = mediator;
    public ILogger<CourseService> Logger { get; } = logger;
    public ICourseQueries Queries { get; } = queries;
    public IIdentityService IdentityService { get; } = identityService;
}
