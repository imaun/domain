namespace iman.Domain;

public class DomainEvent : IDomainEvent
{
    public DomainEvent()
    {
        EventId = Guid.NewGuid();
        PublishedDate = DateTime.UtcNow;
    }
    
    public Guid EventId { get; }
    public DateTime PublishedDate { get; }
    public int Version { get; }
}

public class DomainEvent<TId> : DomainEvent, IDomainEvent<TId> where TId : notnull
{
    public DomainEvent() : base()
    {
    }

    public DomainEvent(TId entityId) : base()
    {
        EntityId = entityId;
    }
    
    public TId EntityId { get; }
}