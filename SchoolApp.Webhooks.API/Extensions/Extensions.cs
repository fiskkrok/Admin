using Admin.ServiceDefaults;
using Microsoft.Data.SqlClient;
using SchoolApp.Webhooks.API.IntegrationEvents;


internal static class Extensions
{
    public static void AddApplicationServices(this IHostApplicationBuilder builder, string? connectionString = null)
    {
        builder.AddDefaultAuthentication();

        builder.AddRabbitMqEventBus("eventbus")
               .AddEventBusSubscriptions();

            if (connectionString == null)
            {
                SqlConnectionStringBuilder sqlBuilder = new();
            //sqlBuilder.DataSource = "localhost";
            sqlBuilder.DataSource = "FISKKROK\\SQLEXPRESS";
            sqlBuilder.InitialCatalog = "SchoolSystem";
                sqlBuilder.TrustServerCertificate = true;
                sqlBuilder.MultipleActiveResultSets = true;
                sqlBuilder.IntegratedSecurity = true;
                connectionString = sqlBuilder.ConnectionString;
            }
            builder.Services.AddDbContext<WebhooksDbContext>(options =>
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
            builder.Services.AddMigration<WebhooksDbContext>();

        builder.Services.AddTransient<IGrantUrlTesterService, GrantUrlTesterService>();
        builder.Services.AddTransient<IWebhooksRetriever, WebhooksRetriever>();
        builder.Services.AddTransient<IWebhooksSender, WebhooksSender>();
    }

    private static void AddEventBusSubscriptions(this IEventBusBuilder eventBus)
    {
        eventBus.AddSubscription<ProductPriceChangedIntegrationEvent, ProductPriceChangedIntegrationEventHandler>();
        eventBus.AddSubscription<OrderStatusChangedToShippedIntegrationEvent, OrderStatusChangedToShippedIntegrationEventHandler>();
        eventBus.AddSubscription<OrderStatusChangedToPaidIntegrationEvent, OrderStatusChangedToPaidIntegrationEventHandler>();
    }
}
