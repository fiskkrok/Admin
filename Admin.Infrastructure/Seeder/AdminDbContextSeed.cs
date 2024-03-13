using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

using Admin.Application.AggregateModels.CourseAggregate;
using Admin.Application.AggregateModels.CourseAssignmentAggregate;
using Admin.Application.AggregateModels.EnrollmentAggregate;
using Admin.Application.AggregateModels.FacultyAggregate;
using Admin.Application.AggregateModels.StudentAggregate;
using Admin.Infrastructure.Common;

using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
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

        if (!context.Faculties.Any())
        {
            var sourceFacultiesPath = Path.Combine(contentRootPath, "Setup", "Faculties.json");
            var sourceFacultiesJson = File.ReadAllText(sourceFacultiesPath);
            var sourceFacultiesItems = JsonSerializer.Deserialize<List<Faculty>>(sourceFacultiesJson);
            await context.Faculties.AddRangeAsync(sourceFacultiesItems);
            await context.SaveChangesAsync();
            logger.LogInformation("Seeded faculties");
        }

        if (!context.Courses.Any())
        {
            var sourceCoursesPath = Path.Combine(contentRootPath, "Setup", "Courses.json");
            var sourceCoursesJson = File.ReadAllText(sourceCoursesPath);
            var sourceCoursesItems = JsonSerializer.Deserialize<List<Course>>(sourceCoursesJson).Distinct();
            await context.Courses.AddRangeAsync(sourceCoursesItems);
            await context.SaveChangesAsync();
            logger.LogInformation("Seeded courses");
        }

        if (!context.Students.Any())
        {
            var sourceStudentsPath = Path.Combine(contentRootPath, "Setup", "Students.json");
            var sourceStudentsJson = File.ReadAllText(sourceStudentsPath);
            var sourceStudentsItems = JsonSerializer.Deserialize<List<Student>>(sourceStudentsJson).Distinct();
            await context.Students.AddRangeAsync(sourceStudentsItems);
            await context.SaveChangesAsync();
            logger.LogInformation("Seeded students");
        }

        if (!context.CourseAssignments.Any())
        {
            var sourceCourseAssignmentAssignmentsPath = Path.Combine(contentRootPath, "Setup", "CourseAssignment.json");
            var sourceCourseAssignmentAssignmentsJson = File.ReadAllText(sourceCourseAssignmentAssignmentsPath);
            var sourceCourseAssignmentAssignmentsItems = 
                JsonSerializer.Deserialize<List<CourseAssignment>>(sourceCourseAssignmentAssignmentsJson).Distinct();
            await context.CourseAssignments.AddRangeAsync(sourceCourseAssignmentAssignmentsItems);
            await context.SaveChangesAsync();
            logger.LogInformation("Seeded courseassignments");
        }

        if (!context.Enrollments.Any())
        {
            var sourceEnrollmentsPath = Path.Combine(contentRootPath, "Setup", "Enrollments.json");
            var sourceEnrollmentsJson = File.ReadAllText(sourceEnrollmentsPath);
            var sourceEnrollmentsItems = JsonSerializer.Deserialize<List<Enrollment>>(sourceEnrollmentsJson).Distinct();
            await context.Enrollments.AddRangeAsync(sourceEnrollmentsItems);
            await context.SaveChangesAsync();
            logger.LogInformation("Seeded enrollments");
        }
        await context.SaveChangesAsync();
    }
}

//public async Task SeedAsync(AdminDbContext context)
//{

//    var contentRootPath = env.ContentRootPath;
//    context.Database.OpenConnection();
//    context.Database.GetDbConnection();

//    if (!context.CourseAssignments.Any()
//        && !context.Students.Any()
//        && !context.Courses.Any()
//        && !context.Faculties.Any()
//        && !context.Enrollments.Any())
//    {
//        var sourceCoursesPath = Path.Combine(contentRootPath, "Setup", "Courses.json");
//        var sourceStudentsPath = Path.Combine(contentRootPath, "Setup", "Students.json");
//        var sourceCourseAssignmentsPath = Path.Combine(contentRootPath, "Setup", "CourseAssignments.json");
//        var sourceEnrollmentsPath = Path.Combine(contentRootPath, "Setup", "Enrollments.json");
//        var sourceFacultiesPath = Path.Combine(contentRootPath, "Setup", "Faculties.json");

//        var sourceCoursesJson = File.ReadAllText(sourceCoursesPath);
//        var sourceStudentsJson = File.ReadAllText(sourceStudentsPath);
//        var sourceCourseAssignmentsJson = File.ReadAllText(sourceCourseAssignmentsPath);
//        var sourceEnrollmentsJson = File.ReadAllText(sourceEnrollmentsPath);
//        var sourceFacultiesJson = File.ReadAllText(sourceFacultiesPath);

//        var sourceCoursesItems = JsonSerializer.Deserialize<List<Course>>(sourceCoursesJson);
//        var sourceStudentsItems = JsonSerializer.Deserialize<List<Student>>(sourceStudentsJson);
//        var sourceCourseAssignmentsItems = JsonSerializer.Deserialize<List<CourseAssignment>>(sourceCourseAssignmentsJson);
//        var sourceEnrollmentsItems = JsonSerializer.Deserialize<List<Enrollment>>(sourceEnrollmentsJson);
//        var sourceFacultiesItems = JsonSerializer.Deserialize<List<Faculty>>(sourceFacultiesJson);

//        context.Courses.RemoveRange(context.Courses);
//        context.Students.RemoveRange(context.Students);
//        context.CourseAssignments.RemoveRange(context.CourseAssignments);
//        context.Enrollments.RemoveRange(context.Enrollments);
//        context.Faculties.RemoveRange(context.Faculties);

//        await context.Courses.AddRangeAsync(sourceCoursesItems);
//        logger.LogInformation("Seeded {NumCourses} courses", context.Courses.Count());

//        await context.Students.AddRangeAsync(sourceStudentsItems);
//        logger.LogInformation("Seeded {NumStudents} students", context.Students.Count());

//        await context.CourseAssignments.AddRangeAsync(sourceCourseAssignmentsItems);
//        logger.LogInformation("Seeded {NumAssignments} course assignments", context.CourseAssignments.Count());

//        await context.Enrollments.AddRangeAsync(sourceEnrollmentsItems);
//        logger.LogInformation("Seeded {NumEnrollments} enrollments", context.Enrollments.Count());

//        await context.Faculties.AddRangeAsync(sourceFacultiesItems);
//        logger.LogInformation("Seeded {NumFaculties} faculties", context.Faculties.Count());

//        await context.SaveChangesAsync();
//    }
//}