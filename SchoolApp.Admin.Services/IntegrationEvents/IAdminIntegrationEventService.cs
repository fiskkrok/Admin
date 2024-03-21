using SchoolApp.EventBus.Events;

namespace SchoolApp.Admin.Services.IntegrationEvents;

public interface IAdminIntegrationEventService
{
    Task SaveEventAndAdminDbContextChangesAsync(IntegrationEvent evt);
    Task PublishThroughEventBusAsync(IntegrationEvent evt);
}
