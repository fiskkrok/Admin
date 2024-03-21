using SchoolApp.EventBus.Events;

namespace SchoolApp.Admin.Services.IntegrationEvents.Events;

public record OrderStatusChangedToPaidIntegrationEvent(int OrderId, IEnumerable<OrderStockItem> OrderStockItems) : IntegrationEvent;
