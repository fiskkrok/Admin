using System.ComponentModel;
using System.Text.Json;
using Admin.Application.AggregateModels.CourseAggregate;
using Admin.Application.AggregateModels.CourseAssignmentAggregate;
using Admin.Application.AggregateModels.EnrollmentAggregate;
using Admin.Application.AggregateModels.FacultyAggregate;
using Admin.Application.AggregateModels.StudentAggregate;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;


namespace Admin.Infrastructure.Data;

public class AdminDbContextSeed(
    IWebHostEnvironment env,
    ILogger<AdminDbContextSeed> logger) : IDbSeeder<AdminDbContext>
{
    public async Task SeedAsync(AdminDbContext context)
    {
        var contentRootPath = env.ContentRootPath;
        context.Database.OpenConnection();
        var options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
            
        };
        
        if (!context.Faculties.Any())
        {
            var sourceFacultiesPath = Path.Combine(contentRootPath, "Setup", "Faculties.json");
            var sourceFacultiesJson = File.ReadAllText(sourceFacultiesPath);
            var sourceFacultiesItems = JsonSerializer.Deserialize<Faculty[]>(sourceFacultiesJson, options);
            if (sourceFacultiesItems != null) await context.Faculties.AddRangeAsync(sourceFacultiesItems);
            await context.SaveChangesAsync();
            logger.LogInformation("Seeded faculties");
        }

        if (!context.Courses.Any())
        {
            var sourceCoursesPath = Path.Combine(contentRootPath, "Setup", "Courses.json");
            var sourceCoursesJson = File.ReadAllText(sourceCoursesPath);
            var sourceCoursesItems = JsonSerializer.Deserialize<Course[]>(sourceCoursesJson, options);
            await context.Courses.AddRangeAsync(sourceCoursesItems);
            await context.SaveChangesAsync();
            logger.LogInformation("Seeded courses");
        }

        if (!context.Students.Any())
        {
            var sourceStudentsPath = Path.Combine(contentRootPath, "Setup", "Students.json");
            var sourceStudentsJson = File.ReadAllText(sourceStudentsPath);
            var sourceStudentsItems = JsonSerializer.Deserialize<Student[]>(sourceStudentsJson, options);
            await context.Students.AddRangeAsync(sourceStudentsItems);
            await context.SaveChangesAsync();
            logger.LogInformation("Seeded students");
        }

        if (!context.CourseAssignments.Any())
        {
            var sourceCourseAssignmentsPath = Path.Combine(contentRootPath, "Setup", "CourseAssignment.json");
            var sourceCourseAssignmentsJson = File.ReadAllText(sourceCourseAssignmentsPath);
            var sourceCourseAssignmentsItems =
                JsonSerializer.Deserialize<CourseAssignment[]>(sourceCourseAssignmentsJson, options);
            await context.CourseAssignments.AddRangeAsync(sourceCourseAssignmentsItems);
            await context.SaveChangesAsync();
            logger.LogInformation("Seeded courseassignments");
        }

        if (!context.Enrollments.Any())
        {
            var sourceEnrollmentsPath = Path.Combine(contentRootPath, "Setup", "Enrollments.json");
            var sourceEnrollmentsJson = File.ReadAllText(sourceEnrollmentsPath);
            var sourceEnrollmentsItems = JsonSerializer.Deserialize<Enrollment[]>(sourceEnrollmentsJson, options);
            await context.Enrollments.AddRangeAsync(sourceEnrollmentsItems);
            await context.SaveChangesAsync();
            logger.LogInformation("Seeded enrollments");
        }
        await context.SaveChangesAsync();
    }
    
}


