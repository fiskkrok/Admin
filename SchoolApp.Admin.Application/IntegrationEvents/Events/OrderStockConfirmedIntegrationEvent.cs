using SchoolApp.EventBus.Events;

namespace SchoolApp.Admin.Application.IntegrationEvents.Events;



public record OrderStockConfirmedIntegrationEvent(int OrderId) : IntegrationEvent;
