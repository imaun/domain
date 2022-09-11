namespace iman.Domain;

public interface IAggregateRoot
{
    int Version { get; }
    
    void When(object @event);
    
    IDomainEvent[] DequeueUncommittedEvents();

    IReadOnlyList<IDomainEvent> GetAllEvents();

    void Enqueue(IDomainEvent @event);

    Task PublishEventsAsync(CancellationToken cancellationToken = default);
}
