using System.Reflection;

namespace iman.Domain;

public class CommandBus : ICommandBus
{
    private readonly IServiceFactory _serviceFactory;

    public CommandBus(IServiceFactory serviceFactory)
    {
        _serviceFactory = serviceFactory;
    }

    public async Task<TResult> ExecuteAsync<TResult>(ICommand<TResult> command, CancellationToken cancellationToken = default)
    {
        var handler = typeof(IMessageProcessor<,>).MakeGenericType(
            command.GetType(), typeof(TResult));
        var instance = _serviceFactory.GetInstance(handler);
        var method = instance.GetType().GetTypeInfo()
            .GetMethod(nameof(IMessageProcessor<ICommand<TResult>, TResult>.HandleAsync));
        
        if (method == null)
        {
            throw new InvalidCommandHandlerInstanceException(handler.Name);
        }

        return await (Task<TResult>)method.Invoke(instance, new object[]
        {
            command, cancellationToken
        })!;
    }

    public async Task ExecuteAsync(ICommand command, CancellationToken cancellationToken = default)
    {
        var handler =  typeof(IMessageProcessor<>).MakeGenericType(
            command.GetType());
        var instance = _serviceFactory.GetInstance(handler);
        var method = instance.GetType().GetTypeInfo()
            .GetMethod(nameof(IMessageProcessor<ICommand>.HandleAsync));
        if (method == null)
        {
            throw new InvalidCommandHandlerInstanceException(handler.Name);
        }

        await (Task)method.Invoke(instance, new object[]
        {
            command, cancellationToken
        })!;
    }
}