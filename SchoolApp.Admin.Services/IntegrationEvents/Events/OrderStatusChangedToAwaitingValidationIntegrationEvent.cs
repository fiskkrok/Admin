using SchoolApp.EventBus.Events;

namespace SchoolApp.Admin.Services.IntegrationEvents.Events;

public record OrderStatusChangedToAwaitingValidationIntegrationEvent(int OrderId, IEnumerable<OrderStockItem> OrderStockItems) : IntegrationEvent;
