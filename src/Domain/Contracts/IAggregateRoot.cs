namespace iman.Domain;

public interface IAggregateRoot : IEntity
{
    int Version { get; }
    
    void When(object @event);
    
    IDomainEvent[] DequeueUncommittedEvents();

    IReadOnlyList<IDomainEvent> GetAllEvents();

    void Enqueue(IDomainEvent @event);
}

public interface IAggregateRoot<TId> : IAggregateRoot, IEntity<TId>
{
    
    TId Id { get;}
}