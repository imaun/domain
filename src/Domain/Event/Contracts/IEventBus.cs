namespace iman.Domain;

public interface IEventBus
{

    Task PublishAsync(IDomainEvent @event, CancellationToken cancelToken);
}
