
using Admin.Application.Commands;
using Admin.ServiceDefaults;
using FluentValidation;
using SchoolApp.Admin.Services.IntegrationEvents.EventHandling;

using Microsoft.Data.SqlClient; // SqlConnectionStringBuilder
// UseSqlServer
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.VisualStudio.Web.CodeGeneration.Design;
using SchoolApp.Admin.Application.Behaviors;
using SchoolApp.Admin.Application.Commands.Course;
using SchoolApp.Admin.Application.Commands.CourseAssignment;
using SchoolApp.Admin.Application.Commands.Enrollment;
using SchoolApp.Admin.Application.Commands.Faculty;
using SchoolApp.Admin.Application.Commands.Student;
using SchoolApp.Admin.Application.Queries.CourseAssignments;
using SchoolApp.Admin.Application.Queries.Courses;
using SchoolApp.Admin.Application.Queries.CoursesAssignment;
using SchoolApp.Admin.Application.Queries.Enrollments;
using SchoolApp.Admin.Application.Queries.Faculties;
using SchoolApp.Admin.Application.Queries.Students;
using SchoolApp.Admin.Application.Validations;
using SchoolApp.Admin.Domain.SeedWork;
using SchoolApp.Admin.Infrastructure;
using SchoolApp.Admin.Infrastructure.Idempotency;
using SchoolApp.Admin.Infrastructure.Repositories;
using SchoolApp.Admin.Services.IntegrationEvents;
using SchoolApp.IntegrationEventLogEF.Services; // IServiceCollection

namespace SchoolApp.Admin.Services.Extensions;
public static class Extensions
{
    public static void AddApplicationServices(this IHostApplicationBuilder builder,
        string? connectionString = null)
    {
        var services = builder.Services;
        builder.AddDefaultAuthentication();
                if (connectionString == null)
                {
                    SqlConnectionStringBuilder sqlBuilder = new();
                    sqlBuilder.DataSource = "FISKKROK\\SQLEXPRESS";
                    sqlBuilder.InitialCatalog = "AdminSystem";
                    sqlBuilder.TrustServerCertificate = true;
                    sqlBuilder.MultipleActiveResultSets = true;
                    sqlBuilder.IntegratedSecurity = true;
                    connectionString = sqlBuilder.ConnectionString;
                }

                builder.Services.AddDbContext<AdminDbContext>(options =>
                {
                    options.UseSqlServer(connectionString)
                        .EnableSensitiveDataLogging();
                    // Log to console when executing EF Core commands.
                    options.LogTo(Console.WriteLine,
                        new[]
                        {
                            Microsoft.EntityFrameworkCore
                                .Diagnostics.RelationalEventId.CommandExecuting
                        });
                });
        // Uncomment för att köra migrations
        builder.Services.AddMigration<AdminDbContext, AdminDbContextSeed>();
        builder.Services.AddTransient<IIntegrationEventLogService, IntegrationEventLogService<AdminDbContext>>();
        builder.Services.AddTransient<IAdminIntegrationEventService, AdminIntegrationEventService>();
        services.AddHttpContextAccessor();
        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssemblyContaining(typeof(Program));

            cfg.AddOpenBehavior(typeof(LoggingBehavior<,>));
            cfg.AddOpenBehavior(typeof(ValidatorBehavior<,>));
            //cfg.AddOpenBehavior(typeof(TransactionBehavior<,>));
        });
        services.AddSingleton<IValidator<CancelEnrollmentCommand>, CancelEnrollmentCommandValidator>();
        services.AddSingleton<IValidator<DeleteStudentCommand>, DeleteStudentCommandValidator>();
        services.AddSingleton<IValidator<DeleteCourseCommand>, DeleteCourseCommandValidator>();
        services.AddSingleton<IValidator<DeleteFacultyCommand>, DeleteFacultyCommandValidator>();
        services.AddSingleton<IValidator<DeleteCourseAssignmentCommand>, DeleteCourseAssignmentCommandValidator>();
        services.AddSingleton<IValidator<CreateCourseAssignmentCommand>, CreateCourseAssignmentCommandValidator>();
        services.AddSingleton<IValidator<CreateStudentCommand>, CreateStudentCommandValidator>();
        services.AddSingleton<IValidator<CreateCourseCommand>, CreateCourseCommandValidator>();
        services.AddSingleton<IValidator<CreateFacultyCommand>, CreateFacultyCommandValidator>();
        services.AddSingleton<IValidator<CreateEnrollmentCommand>, CreateEnrollmentCommandValidator>();
        services.AddSingleton<IValidator<IdentifiedCommand<CreateStudentCommand, bool>>, IdentifiedCommandValidator>();
        services.AddScoped<ICourseQueries, CourseQueries>();
        services.AddScoped<ICourseAssignmentQueries, CourseAssignmentQueries>();
        services.AddScoped<IEnrollmentQueries, EnrollmentQueries>();
        services.AddScoped<IFacultyQueries, FacultyQueries>();
        services.AddScoped<IStudentQueries, StudentQueries>();
        services.AddScoped<ICourseRepository, CourseRepository>();
        services.AddScoped<ICourseAssignmentRepository, CourseAssignmentRepository>();
        services.AddScoped<IEnrollmentRepository, EnrollmentRepository>();
        services.AddScoped<IFacultyRepository, FacultyRepository>();
        services.AddScoped<IStudentRepository, StudentRepository>();
        services.AddScoped<IRequestManager, RequestManager>();
        builder.AddRabbitMqEventBus("eventbus").AddEventBusSubscriptions();



        builder.Services.AddScoped(typeof(IRepository<>), typeof(EfRepository<>));
        builder.Services.AddScoped(typeof(IReadRepository<>), typeof(EfRepository<>));

    }
        private static void AddEventBusSubscriptions(this IEventBusBuilder eventBus)
        {
            eventBus
                .AddSubscription<OrderStatusChangedToAwaitingValidationIntegrationEvent,
                    OrderStatusChangedToAwaitingValidationIntegrationEventHandler>();
        eventBus.AddSubscription<OrderStatusChangedToPaidIntegrationEvent, OrderStatusChangedToPaidIntegrationEventHandler>();
    }
}