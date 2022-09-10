namespace iman.Domain;

public interface IMessageHandler<in TMessage> where TMessage : IMessage
{
    Task HandleAsync(TMessage message, CancellationToken cancellationToken);
}

public interface IMessageHandler<in TMessage, TResponse> where TMessage : IMessage<TResponse> 
{
    
    Task<TResponse> HandleAsync(TMessage message, CancellationToken cancellationToken);
}