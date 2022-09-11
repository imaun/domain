namespace iman.Domain;

public class AggregateRoot<TId> : Entity<TId>, IAggregateRoot
{
    protected readonly Queue<IDomainEvent> _uncommittedEvents;
    protected readonly IMediator _mediator;

    protected AggregateRoot(IMediator mediator, int version = 1)
    {
        _mediator = mediator 
                    ?? throw new ArgumentNullException(
                        "Mediator is null, please consider register it to the ServiceCollection or any other container you use.");
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

    public IReadOnlyList<IDomainEvent> GetAllEvents()
    {
        return _uncommittedEvents.ToList();
    }

    public void Enqueue(IDomainEvent @event)
    {
        _uncommittedEvents.Enqueue(@event);
    }

    public async Task PublishEventsAsync(CancellationToken cancellationToken = default)
    {
        foreach (var @event in _uncommittedEvents)
        {
            await _mediator.PublishAsync(@event, cancellationToken);
        }
    }
    
 }