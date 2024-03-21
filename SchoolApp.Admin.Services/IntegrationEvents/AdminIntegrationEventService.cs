﻿using Admin.Infrastructure.Data;
using Microsoft.Extensions.Logging;
using SchoolApp.EventBus.Abstractions;
using SchoolApp.EventBus.Events;
using SchoolApp.IntegrationEventLogEF.Services;
using SchoolApp.IntegrationEventLogEF.Utilities;

namespace SchoolApp.Admin.Services.IntegrationEvents;

public sealed class AdminIntegrationEventService(ILogger<AdminIntegrationEventService> logger,
    IEventBus eventBus,
    AdminDbContext adminDbContext,
    IIntegrationEventLogService integrationEventLogService)
    : IAdminIntegrationEventService, IDisposable
{
    private volatile bool disposedValue;

    public async Task PublishThroughEventBusAsync(IntegrationEvent evt)
    {
        try
        {
            logger.LogInformation("Publishing integration event: {IntegrationEventId_published} - ({@IntegrationEvent})", evt.Id, evt);

            await integrationEventLogService.MarkEventAsInProgressAsync(evt.Id);
            await eventBus.PublishAsync(evt);
            await integrationEventLogService.MarkEventAsPublishedAsync(evt.Id);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error Publishing integration event: {IntegrationEventId} - ({@IntegrationEvent})", evt.Id, evt);
            await integrationEventLogService.MarkEventAsFailedAsync(evt.Id);
        }
    }

    public async Task SaveEventAndAdminDbContextChangesAsync(IntegrationEvent evt)
    {
        logger.LogInformation("CatalogIntegrationEventService - Saving changes and integrationEvent: {IntegrationEventId}", evt.Id);

        //Use of an EF Core resiliency strategy when using multiple DbContexts within an explicit BeginTransaction():
        //See: https://docs.microsoft.com/en-us/ef/core/miscellaneous/connection-resiliency            
        await ResilientTransaction.New(adminDbContext).ExecuteAsync(async () =>
        {
            // Achieving atomicity between original catalog database operation and the IntegrationEventLog thanks to a local transaction
            await adminDbContext.SaveChangesAsync();
            await integrationEventLogService.SaveEventAsync(evt, adminDbContext.Database.CurrentTransaction);
        });
    }

    private void Dispose(bool disposing)
    {
        if (!disposedValue)
        {
            if (disposing)
            {
                (integrationEventLogService as IDisposable)?.Dispose();
            }

            disposedValue = true;
        }
    }

    public void Dispose()
    {
        Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }
}
