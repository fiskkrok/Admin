
using Microsoft.AspNetCore.Identity;



using SchoolApp.Admin.Services.Extensions;
using SchoolApp.Admin.WebAPI;
using SchoolApp.ServiceDefaults;

var builder = WebApplication.CreateBuilder(args);



builder.AddServiceDefaults();
builder.AddDefaultOpenApi();
builder.AddApplicationServices( null);
builder.Services.AddProblemDetails();
var app = builder.Build();

app.UseDefaultOpenApi();
app.MapDefaultEndpoints();

app.MapGroup("/api/v1/admin")
    .WithTags("Admin API")
    .MapCourseEndpoints()
    .MapFacultyEndpoints()
    .MapCourseAssignmentEndpoints()
    .MapStudentEndpoints()
.MapEnrollmentEndpoints()
    .RequireAuthorization();

app.Run();
