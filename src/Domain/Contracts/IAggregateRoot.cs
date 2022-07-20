namespace iman.Domain;

public interface IAggregateRoot : IEntity
{
    int Version { get; }
    
    void When(object @event);
    
    IDomainEvent[] DequeueUncommittedEvents();
}

public interface IAggregateRoot<TId> : IAggregateRoot, IEntity<TId>
{
    
}