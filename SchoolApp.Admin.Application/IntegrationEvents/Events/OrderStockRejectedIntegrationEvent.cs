using SchoolApp.EventBus.Events;

namespace SchoolApp.Admin.Application.IntegrationEvents.Events;

public record OrderStockRejectedIntegrationEvent(int OrderId, List<ConfirmedOrderStockItem> OrderStockItems) : IntegrationEvent;
