using SchoolApp.EventBus.Events;

namespace SchoolApp.Admin.Services.IntegrationEvents.Events;

public record OrderStockRejectedIntegrationEvent(int OrderId, List<ConfirmedOrderStockItem> OrderStockItems) : IntegrationEvent;
