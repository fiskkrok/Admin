
using Microsoft.Extensions.Hosting;
namespace SchoolApp.Admin.Services.Extensions;

public class AdminDbContextSeed(
    IHostEnvironment env,
    ILogger<AdminDbContextSeed> logger) : IDbSeeder<AdminDbContext>
{
    public async Task SeedAsync(AdminDbContext context)
    {
        var contentRootPath = env.ContentRootPath;
        context.Database.OpenConnection();
        var options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true,

        };
        options.Converters.Add(new DateOnlyJsonConverter("yyyy/MM/dd")); // Adjust the format as needed
        options.IncludeFields = true;
        if (!context.Faculties.Any())
        {
            var sourceFacultiesPath = Path.Combine(contentRootPath, "Setup", "Faculties.json");
            var sourceFacultiesJson = File.ReadAllText(sourceFacultiesPath);
            var sourceFacultiesItems = JsonSerializer.Deserialize<Faculty[]>(sourceFacultiesJson, options);
            await context.Faculties.AddRangeAsync(sourceFacultiesItems ?? throw new InvalidOperationException($"{nameof(sourceFacultiesItems)} can't be empty"));
            await context.SaveChangesAsync();
            logger.LogInformation("Seeded faculties");
        }

        if (!context.Courses.Any())
        {
            var sourceCoursesPath = Path.Combine(contentRootPath, "Setup", "Courses.json");
            var sourceCoursesJson = File.ReadAllText(sourceCoursesPath);
            var sourceCoursesItems = JsonSerializer.Deserialize<Course[]>(sourceCoursesJson, options);
            await context.Courses.AddRangeAsync(sourceCoursesItems ?? throw new InvalidOperationException($"{nameof(sourceCoursesItems)} can't be empty"));
            await context.SaveChangesAsync();
            logger.LogInformation("Seeded courses");
        }

        if (!context.Students.Any())
        {
            var sourceStudentsPath = Path.Combine(contentRootPath, "Setup", "Students.json");
            var sourceStudentsJson = File.ReadAllText(sourceStudentsPath);
            var sourceStudentsItems = JsonSerializer.Deserialize<Student[]>(sourceStudentsJson, options);
            await context.Students.AddRangeAsync(sourceStudentsItems ?? throw new InvalidOperationException($"{nameof(sourceStudentsItems)} can't be empty")) ;
            await context.SaveChangesAsync();
            logger.LogInformation("Seeded students");
        }

        if (!context.CourseAssignments.Any())
        {
            var sourceCourseAssignmentsPath = Path.Combine(contentRootPath, "Setup", "CourseAssignment.json");
            var sourceCourseAssignmentsJson = File.ReadAllText(sourceCourseAssignmentsPath);
            var sourceCourseAssignmentsItems =
                JsonSerializer.Deserialize<CourseAssignment[]>(sourceCourseAssignmentsJson, options);
            await context.CourseAssignments.AddRangeAsync(sourceCourseAssignmentsItems ?? throw new InvalidOperationException($"{nameof(sourceCourseAssignmentsItems)} can't be empty"));
            await context.SaveChangesAsync();
            logger.LogInformation("Seeded courseassignments");
        }

        if (!context.Enrollments.Any())
        {
            var sourceEnrollmentsPath = Path.Combine(contentRootPath, "Setup", "Enrollments.json");
            var sourceEnrollmentsJson = File.ReadAllText(sourceEnrollmentsPath);
            var sourceEnrollmentsItems = JsonSerializer.Deserialize<Enrollment[]>(sourceEnrollmentsJson, options);
            await context.Enrollments.AddRangeAsync(sourceEnrollmentsItems ?? throw new InvalidOperationException($"{nameof(sourceEnrollmentsItems)} can't be empty"));
            await context.SaveChangesAsync();
            logger.LogInformation("Seeded enrollments");
        }
        await context.SaveChangesAsync();
    }
    
}

internal class DateOnlyJsonConverter : JsonConverter<DateOnly>
{
    private readonly string _format;

    public DateOnlyJsonConverter(string format = "yyyy/MM/dd")
    {
        _format = format;
    }

    public override DateOnly Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        return DateOnly.ParseExact(reader.GetString()!, _format);
    }

    public override void Write(Utf8JsonWriter writer, DateOnly value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.ToString(_format));
    }
}
