namespace iman.Domain;

public interface IEventHandler<in TEvent> : IMessageHandler<TEvent> where TEvent : IEvent
{
    
}