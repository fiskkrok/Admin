using MediatR;

namespace SchoolApp.Admin.WebAPI.Endpoints.Course;

public class UpdateCourseResponse : IRequest<bool>
{
   public int Id  { get; set; }
}
