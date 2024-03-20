using Admin.Web.Services;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

namespace SchoolApp.Admin.Web.Services;

public class ServiceExstension
{
    public static void AddServices(WebAssemblyHostBuilder Builder)
    {
        Builder.Services.AddScoped<CourseService>();
        Builder.Services.AddScoped<StudentService>();
        Builder.Services.AddScoped<FacultyService>();
        Builder.Services.AddScoped<CourseAssignmentService>();
        Builder.Services.AddScoped<EnrollmentService>();
    }

}
