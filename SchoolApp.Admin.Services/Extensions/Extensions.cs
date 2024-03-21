

using Admin.Application.SeedWork;
using Admin.Infrastructure;
using Admin.Infrastructure.Data;
using SchoolApp.Admin.Services.IntegrationEvents.EventHandling;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Data.SqlClient; // SqlConnectionStringBuilder
using Microsoft.EntityFrameworkCore; // UseSqlServer
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SchoolApp.Admin.Services.IntegrationEvents;
using SchoolApp.Admin.Services.IntegrationEvents.Events;
using SchoolApp.IntegrationEventLogEF.Services; // IServiceCollection

namespace SchoolApp.Admin.Services.Exstensions;
public static class Extensions
{
    public static void AddApplicationServices(this IHostApplicationBuilder builder,
        string? connectionString = null)
    {
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
                    new[] { Microsoft.EntityFrameworkCore
                        .Diagnostics.RelationalEventId.CommandExecuting });
            },
            contextLifetime: ServiceLifetime.Transient,
            optionsLifetime: ServiceLifetime.Transient);
        // Uncomment för att köra migrations
        builder.Services.AddMigration<AdminDbContext, AdminDbContextSeed>();

        builder.Services.AddTransient<IIntegrationEventLogService, IntegrationEventLogService<AdminDbContext>>();

        builder.Services.AddTransient<IAdminIntegrationEventService, AdminIntegrationEventService>();

        builder.AddRabbitMqEventBus("eventbus")
            .AddSubscription<OrderStatusChangedToAwaitingValidationIntegrationEvent, OrderStatusChangedToAwaitingValidationIntegrationEventHandler>()
            .AddSubscription<OrderStatusChangedToPaidIntegrationEvent, OrderStatusChangedToPaidIntegrationEventHandler>();

        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        builder.Services.AddScoped(typeof(IRepository<>), typeof(EfRepository<>));
        builder.Services.AddScoped(typeof(IReadRepository<>), typeof(EfRepository<>));

    }
}
