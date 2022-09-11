namespace iman.Domain;

public interface IEvent : IMessage
{
    
}

public interface IEvent<TResult>
{
}