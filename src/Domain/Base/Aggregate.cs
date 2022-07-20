namespace iman.Domain;

public class Aggregate<TId> : IAggregateRoot<TId> where TId: notnull
{
    public TId Id { get; protected set; } = default!;
    
    public int Version { get; }

    private readonly Queue<object> uncommittedEvents = new();
    
    public virtual void When(object @event) { }
    
    public object[] DequeueUncommittedEvents()
    {
        var dequeuedEvents = uncommittedEvents.ToArray();
        uncommittedEvents.Clear();
        return dequeuedEvents;
    }

    protected void Enqueue(object @event)
    {
        uncommittedEvents.Enqueue(@event);
    }
}