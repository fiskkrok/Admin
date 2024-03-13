using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Admin.Application.Entities.IdentityLogic;
using Microsoft.Extensions.Logging;

namespace Admin.Application.Entities.StudentLogic;
public class StudentService(
    IMediator mediator,
    IStudentQueries queries,
    IIdentityService identityService,
    ILogger<StudentService> logger)
{
    public IMediator Mediator { get; set; } = mediator;
    public ILogger<StudentService> Logger { get; } = logger;
    public IStudentQueries Queries { get; } = queries;
    public IIdentityService IdentityService { get; } = identityService;
}
