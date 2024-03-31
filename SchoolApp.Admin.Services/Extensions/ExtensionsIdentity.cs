﻿using Microsoft.AspNetCore.Identity;
using Microsoft.Data.SqlClient; // SqlConnectionStringBuilder
// UseSqlServer
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SchoolApp.Admin.Infrastructure.Identity;
using SchoolApp.Admin.Services.IntegrationEvents;
using SchoolApp.Admin.Domain.SeedWork;


using SchoolApp.IntegrationEventLogEF.Services; // IServiceCollection

namespace SchoolApp.Admin.Services.Extensions;
public static class ExtensionsIdentity
{
    public static void AddApplicationIdentity(this IHostApplicationBuilder builder,
        string? connectionString = null)
    {
        if (connectionString == null)
        {
            SqlConnectionStringBuilder sqlBuilder = new();
            sqlBuilder.DataSource = "FISKKROK\\SQLEXPRESS";
            sqlBuilder.InitialCatalog = "IdentitySystem";
            sqlBuilder.TrustServerCertificate = true;
            sqlBuilder.MultipleActiveResultSets = true;
            sqlBuilder.IntegratedSecurity = true;
            connectionString = sqlBuilder.ConnectionString;
        }
        builder.Services.AddDbContext<AppIdentityDbContext>(options =>
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
        builder.Services.AddMigration<AppIdentityDbContext, AppIdentityDbContextSeed>();
        builder.Services.AddTransient<IIntegrationEventLogService, IntegrationEventLogService<AppIdentityDbContext>>();
        builder.Services.AddTransient<IAdminIntegrationEventService, AdminIntegrationEventService>();

        
        builder.Services.AddScoped<ITokenClaimsService, IdentityTokenClaimService>();


    }
}