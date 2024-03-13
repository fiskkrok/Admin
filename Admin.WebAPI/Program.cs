using System.Configuration;
using Admin.Application.SeedWork;
using Admin.Domain.SeedWork;
using Admin.Infrastructure;
using Admin.Infrastructure.Data;
using Admin.WebAPI;
using Admin.WebAPI.Endpoints.Course;
using Admin.WebAPI.Endpoints.CourseAssignment;
using Admin.WebAPI.Endpoints.Enrollment;
using Admin.WebAPI.Endpoints.Faculty;
using Admin.WebAPI.Endpoints.Student;
using Google.Protobuf.WellKnownTypes;

using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity;

var builder = WebApplication.CreateBuilder(args);
builder.AddServiceDefaults();
Admin.Infrastructure.Common.AdminContextExtensions.AddAdminContext(builder.Services,null);
// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped(typeof(IRepository<>), typeof(EfRepository<>));
builder.Services.AddScoped(typeof(IReadRepository<>), typeof(EfRepository<>));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();
builder.Services.AddAutoMapper(typeof(MappingProfile).Assembly);

var app = builder.Build();

app.MapDefaultEndpoints();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.Logger.LogInformation("AdminApi App created...");

app.Logger.LogInformation("Seeding Database...");
app.UseHttpsRedirection();

app.MapCourseEndpoints();
app.MapEnrollmentEndpoints();
app.MapFacultyEndpoints();
app.MapCourseAssignmentEndpoints();
app.MapStudentEndpoints();

app.Run();
