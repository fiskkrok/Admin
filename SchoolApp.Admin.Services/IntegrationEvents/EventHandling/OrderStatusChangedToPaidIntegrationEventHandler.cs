using SchoolApp.EventBus.Abstractions;

namespace SchoolApp.Admin.Services.IntegrationEvents.EventHandling;

public class OrderStatusChangedToPaidIntegrationEventHandler(
    AdminDbContext adminDbContext,
    ILogger<OrderStatusChangedToPaidIntegrationEventHandler> logger) :
    IIntegrationEventHandler<OrderStatusChangedToPaidIntegrationEvent>
{
    public async Task Handle(OrderStatusChangedToPaidIntegrationEvent @event)
    {
        logger.LogInformation("Handling integration event: {IntegrationEventId} - ({@IntegrationEvent})", @event.Id, @event);

        ////we're not blocking stock/inventory
        //foreach (var orderStockItem in @event.OrderStockItems)
        //{
        //    var catalogItem = adminDbContext.CatalogItems.Find(orderStockItem.ProductId);

        //    catalogItem.RemoveStock(orderStockItem.Units);
        //}

        //await adminDbContext.SaveChangesAsync();
    }
}
