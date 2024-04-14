using SchoolApp.EventBus.Events;

namespace SchoolApp.Admin.Application.IntegrationEvents.Events;

public record OrderStatusChangedToPaidIntegrationEvent(int OrderId, IEnumerable<OrderStockItem> OrderStockItems) : IntegrationEvent;
