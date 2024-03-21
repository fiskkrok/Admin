﻿using Admin.Infrastructure.Data;
using Microsoft.Extensions.Logging;
using SchoolApp.Admin.Services.IntegrationEvents;
using SchoolApp.Admin.Services.IntegrationEvents.Events;
using SchoolApp.EventBus.Abstractions;
using SchoolApp.EventBus.Events;

namespace SchoolApp.Admin.Services.IntegrationEvents.EventHandling;

public class OrderStatusChangedToAwaitingValidationIntegrationEventHandler(
    AdminDbContext adminDbContext,
    IAdminIntegrationEventService catalogIntegrationEventService,
    ILogger<OrderStatusChangedToAwaitingValidationIntegrationEventHandler> logger) :
    IIntegrationEventHandler<OrderStatusChangedToAwaitingValidationIntegrationEvent>
{
    public async Task Handle(OrderStatusChangedToAwaitingValidationIntegrationEvent @event)
    {
        logger.LogInformation("Handling integration event: {IntegrationEventId} - ({@IntegrationEvent})", @event.Id, @event);

        //var confirmedOrderStockItems = new List<ConfirmedOrderStockItem>();

        //foreach (var orderStockItem in @event.OrderStockItems)
        //{
        //    var catalogItem = adminDbContext.CatalogItems.Find(orderStockItem.ProductId);
        //    var hasStock = catalogItem.AvailableStock >= orderStockItem.Units;
        //    var confirmedOrderStockItem = new ConfirmedOrderStockItem(catalogItem.Id, hasStock);

        //    confirmedOrderStockItems.Add(confirmedOrderStockItem);
        //}

        //var confirmedIntegrationEvent = confirmedOrderStockItems.Any(c => !c.HasStock)
        //    ? (IntegrationEvent)new OrderStockRejectedIntegrationEvent(@event.OrderId, confirmedOrderStockItems)
        //    : new OrderStockConfirmedIntegrationEvent(@event.OrderId);

        //await catalogIntegrationEventService.SaveEventAndCatalogContextChangesAsync(confirmedIntegrationEvent);
        //await catalogIntegrationEventService.PublishThroughEventBusAsync(confirmedIntegrationEvent);
    }
}
