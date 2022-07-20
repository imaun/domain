namespace iman.Domain;

public interface IDoaminEvent
{
    Guid EventId { get; }
    
    DateTime PublishedDate { get; }
    
    int Version { get; }
    
}

public interface IDomainEvent<TId> : IDomainEvent where TId : notnull
{
    TId EntityId { get; }
}