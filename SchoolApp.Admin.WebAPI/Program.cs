
using SchoolApp.Admin.Services.Exstensions;
using SchoolApp.Admin.WebAPI;
using SchoolApp.ServiceDefaults;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();
builder.AddDefaultOpenApi();
builder.AddApplicationServices( null);
builder.Services.AddAutoMapper(typeof(MappingProfile).Assembly);
builder.Services.AddCors();

var app = builder.Build();

app.UseDefaultOpenApi();
app.MapDefaultEndpoints();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

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
