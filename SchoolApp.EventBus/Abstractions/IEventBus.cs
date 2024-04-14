using System.Threading.Tasks;

namespace SchoolApp.EventBus.Abstractions;

public interface IEventBus
{
    Task PublishAsync(IntegrationEvent @event);
}
