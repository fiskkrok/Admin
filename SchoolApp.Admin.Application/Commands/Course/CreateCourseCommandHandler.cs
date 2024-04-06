using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SchoolApp.Admin.Infrastructure.Data;

namespace SchoolApp.Admin.Application.Commands.Course;
public class CreateCourseCommandHandler : IRequestHandler<CreateCourseCommand, bool>
{
    private readonly AdminDbContext _context;

    public CreateCourseCommandHandler(AdminDbContext context)
    {
        _context = context;
    }

    public async Task<bool> Handle(CreateCourseCommand request, CancellationToken cancellationToken)
    {
        var course = Domain.AggregateModels.CourseAggregate.Course.CreateInstance(
            Guid.NewGuid().ToString(),
            request.CourseCode,
            request.CourseName,
            request.Credits
        );

        _context.Courses.Add(course);

        var success = await _context.SaveChangesAsync(cancellationToken) > 0;

        return success;
    }
}