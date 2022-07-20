namespace iman.Domain;

/// <summary>
/// Handler for <see cref="DomainEvent"/>s
/// </summary>
public interface IEventHandler<in TEvent> where TEvent : IDomainEvent
{
    
    Task HandleAsync(TEvent @event, CancellationToken cancelToken);
}