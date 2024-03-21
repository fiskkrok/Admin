using SchoolApp.EventBus.Events;

namespace SchoolApp.Admin.Services.IntegrationEvents.Events;



public record OrderStockConfirmedIntegrationEvent(int OrderId) : IntegrationEvent;
