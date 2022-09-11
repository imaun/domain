namespace iman.Domain;

public interface IDomainEvent : IEvent
{
    Guid EventId { get; }
    
    DateTime PublishedDate { get; }
    
    int Version { get; }
    
}

public interface IDomainEvent<TId> : IDomainEvent where TId : notnull
{
    TId EntityId { get; }
}