namespace iman.Domain;

public class MessageProcessor<TMessage> : IMessageProcessor<TMessage>
    where TMessage : IMessage
{
    private readonly IEnumerable<IMessageHandler<TMessage>> _messageHandlers;
    private readonly IEnumerable<IMiddleware<TMessage>> _middlewares;
    
    public MessageProcessor(IServiceFactory serviceFactory)
    {
        _messageHandlers = (IEnumerable<IMessageHandler<TMessage>>)
            serviceFactory.GetInstance(typeof(IEnumerable<IMessageHandler<TMessage>>));

        _middlewares = (IEnumerable<IMiddleware<TMessage>>)
            serviceFactory.GetInstance(typeof(IEnumerable<IMiddleware<TMessage>>));
    }
    
    public Task HandleAsync(TMessage message, CancellationToken cancellationToken) 
        => RunMiddleware(message, HandleMessageAsync, cancellationToken);

    private async Task HandleMessageAsync(TMessage message, CancellationToken cancellationToken)
    {
        var type = typeof(TMessage);

        if (!_messageHandlers.Any())
        {
            throw new ArgumentException($"No handler of signature {typeof(IMessageHandler<,>).Name} was found for {typeof(TMessage).Name}", typeof(TMessage).FullName);
        }

        if (typeof(IEvent).IsAssignableFrom(type))
        {
            var tasks = _messageHandlers.Select(_ => _.HandleAsync(message, cancellationToken));
            foreach (var t in tasks) await t;

            return;
        }

        if (typeof(ICommand).IsAssignableFrom(type))
        {
            await _messageHandlers.Single().HandleAsync(message, cancellationToken);
            return;
        }
        
        throw new ArgumentException($"{typeof(TMessage).Name} is not a known type of {typeof(IMessage<>).Name} - Query, Command or Event", typeof(TMessage).FullName);
    }

    private Task RunMiddleware(
        TMessage message, 
        HandleMessageDelegate<TMessage> handleMessageDelegate,
        CancellationToken cancellationToken)
    {
        HandleMessageDelegate<TMessage> next = null;
        next = _middlewares.Reverse().Aggregate(handleMessageDelegate, (messageDelegate, middleware) =>
            ((req, ct) => middleware.RunAsync(req, ct, messageDelegate)));

        return next.Invoke(message, cancellationToken);
    }
    
}