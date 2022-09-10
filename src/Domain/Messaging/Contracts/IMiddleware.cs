namespace iman.Domain;

public delegate Task<TResponse> HandleMessageDelegate<in TMessage, TResponse>(
    TMessage message, 
    CancellationToken cancellationToken);

public delegate Task HandleMessageDelegate<in TMessage>(
    TMessage message,
    CancellationToken cancellationToken);

public interface IMiddleware<TMessage, TResponse> where TMessage : IMessage<TResponse>
{
    Task<TResponse> RunAsync(
        TMessage message,
        CancellationToken cancellationToken, 
        HandleMessageDelegate<TMessage, TResponse> next);
}

public interface IMiddleware<TMessage> where TMessage : IMessage
{
    Task RunAsync(
        TMessage message,
        CancellationToken cancellationToken, 
        HandleMessageDelegate<TMessage> next);
}