
using Microsoft.AspNetCore.Identity;
using SchoolApp.Admin.Infrastructure.Identity;
using SchoolApp.Admin.WebAPI.Endpoints.AuthEndpoints;

using SchoolApp.Admin.Services.Extensions;
using SchoolApp.Admin.WebAPI;
using SchoolApp.ServiceDefaults;

var builder = WebApplication.CreateBuilder(args);



builder.AddServiceDefaults();
//builder.AddDefaultOpenApi();
builder.AddApplicationServices( null);
builder.AddApplicationIdentity( null);
builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<AppIdentityDbContext>()
    .AddDefaultTokenProviders();
builder.Services.AddCors();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

//app.UseDefaultOpenApi();
app.MapDefaultEndpoints();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection(); 
//app.MapGroup("/api/v1")
app.MapGroup("/")
    .MapCourseEndpoints()
.MapFacultyEndpoints()
.MapCourseAssignmentEndpoints()
.MapStudentEndpoints()
.MapAuthEndpoints()
    .RequireAuthorization();
//.MapEnrollmentEndpoints()

app.UseCors(policyBuilder =>
{
    policyBuilder.AllowAnyHeader().AllowAnyMethod().WithOrigins("https://localhost:7114");
});
app.Run();
