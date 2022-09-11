using System.Reflection;

namespace iman.Domain;

public class Mediator : IMediator
{
    private readonly IServiceFactory _serviceFactory;

    public Mediator(IServiceFactory serviceFactory)
    {
        _serviceFactory = serviceFactory;
    }
    
    public Task<TResponse> PublishAsync<TResponse>(
        IMessage<TResponse> message, 
        CancellationToken cancellationToken = default)
    {
        var targetType = message.GetType(); 
        var targetHandler = typeof(IMessageProcessor<,>).MakeGenericType(targetType, typeof(TResponse));
        var instance = _serviceFactory.GetInstance(targetHandler);
  
        var result = InvokeInstanceAsync(instance, message, targetHandler, cancellationToken);

        return result;
    }

    public async Task PublishAsync(IMessage message, CancellationToken cancellationToken = default)
    {
        var targetType = message.GetType();
        var targetHandler = typeof(IMessageProcessor<>).MakeGenericType(targetType);
        var instance = _serviceFactory.GetInstance(targetHandler);

        var result = InvokeInstanceAsync(instance, message, targetHandler, cancellationToken);
    }
    
    private Task<TResponse> InvokeInstanceAsync<TResponse>(
        object instance, 
        IMessage<TResponse> message, 
        Type targetHandler, 
        CancellationToken cancellationToken)
    {
        var method = instance.GetType()
            .GetTypeInfo()
            .GetMethod(nameof(IMessageProcessor<IMessage<TResponse>, TResponse>.HandleAsync));

        if (method == null)
        {
            throw new ArgumentException(
                $"{instance.GetType().Name} is not a known {targetHandler.Name}",
                instance.GetType().FullName);
        }

        return (Task<TResponse>) method.Invoke(instance, new object[] {message, cancellationToken});
    }

    private Task InvokeInstanceAsync(
        object instance, 
        IMessage message, 
        Type targetHandler,
        CancellationToken cancellationToken)
    {
        var method = instance.GetType()
            .GetTypeInfo()
            .GetMethod(nameof(IMessageProcessor<IMessage>.HandleAsync));

        if (method == null)
        {
            throw new ArgumentException(
                $"{instance.GetType().Name} is not a known {targetHandler.Name}",
                instance.GetType().FullName);
        }

        return (Task)method.Invoke(instance, new object[] { message, cancellationToken });
    }
}