using Admin.Infrastructure.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Data.SqlClient; // SqlConnectionStringBuilder
using Microsoft.EntityFrameworkCore; // UseSqlServer
using Microsoft.Extensions.DependencyInjection; // IServiceCollection

namespace Admin.Infrastructure.Common;
public static class AdminContextExtensions
{
    public static IServiceCollection AddAdminContext(
        this IServiceCollection services,
        string? connectionString = null)
    {
        if (connectionString == null)
        {
            SqlConnectionStringBuilder builder = new();
            builder.DataSource = "FISKKROK\\SQLEXPRESS"; // "ServerName\InstanceName" e.g. @".\sqlexpress"
            builder.InitialCatalog = "AdminSystem";
            builder.TrustServerCertificate = true;
            builder.MultipleActiveResultSets = true;
            // Because we want to fail faster. Default is 15 seconds.
            builder.ConnectTimeout = 3;
            // If using Windows Integrated authentication.
            builder.IntegratedSecurity = true;
            // If using SQL Server authentication.
            // builder.UserID = Environment.GetEnvironmentVariable("MY_SQL_USR");
            // builder.Password = Environment.GetEnvironmentVariable("MY_SQL_PWD");
            connectionString = builder.ConnectionString;
        }
        services.AddDbContext<AdminDbContext>(options =>
            {
                options.UseSqlServer(connectionString)
                    .EnableSensitiveDataLogging();
                // Log to console when executing EF Core commands.
                options.LogTo(Console.WriteLine,
                    new[] { Microsoft.EntityFrameworkCore
                        .Diagnostics.RelationalEventId.CommandExecuting });
            },
            // Register with a transient lifetime to avoid concurrency 
            // issues with Blazor Server projects.
            contextLifetime: ServiceLifetime.Transient,
            optionsLifetime: ServiceLifetime.Transient);
        services.AddMigration<AdminDbContext, AdminDbContextSeed>();
        return services;
    }
}
