namespace iman.Domain;

public abstract class EventHandler<TEvent> : IEventHandler<TEvent> where TEvent : IMessage
{
    public async Task HandleAsync(
        TEvent message,
        CancellationToken cancellationToken)
    {
        await HandleEventAsync(message, cancellationToken);
    }

    protected abstract Task HandleEventAsync(
        TEvent @event,
        CancellationToken cancellationToken);
}