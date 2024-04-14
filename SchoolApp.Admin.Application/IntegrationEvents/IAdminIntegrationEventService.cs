using SchoolApp.EventBus.Events;

namespace SchoolApp.Admin.Application.IntegrationEvents;

public interface IAdminIntegrationEventService
{
    Task PublishEventsThroughEventBusAsync(Guid transactionId);
    Task AddAndSaveEventAsync(IntegrationEvent evt);
}
