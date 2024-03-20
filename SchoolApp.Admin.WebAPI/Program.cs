using Admin.Application.SeedWork;
using SchoolApp.Admin.WebAPI;
using SchoolApp.ServiceDefaults;

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
builder.Services.AddCors();
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
app.UseCors(policyBuilder =>
{
    policyBuilder.AllowAnyHeader().AllowAnyMethod().WithOrigins("https://localhost:7114");
});

app.Run();
