namespace iman.Domain;

public class AggregateRoot : IAggregateRoot
{
    protected readonly Queue<IDomainEvent> _uncommittedEvents;

    protected AggregateRoot(int version = 1)
    {
        _uncommittedEvents = new();
        Version = version;
    }

    public int Version { get; }
    public void When(object @event)
    {
        
    }

    public IDomainEvent[] DequeueUncommittedEvents()
    {
        var dequeuedEvents = _uncommittedEvents.ToArray();
        _uncommittedEvents.Clear();
        return dequeuedEvents;
    }

    protected void Enqueue(IDomainEvent @event)
    {
        _uncommittedEvents.Enqueue(@event);
    }
}

public class AggregateRoot<TId> : AggregateRoot, IAggregateRoot<TId> where TId : notnull
{

    public AggregateRoot() : base(version: 1)
    {
        
    }
    
    public TId Id { get; protected set; } = default!;
   
}