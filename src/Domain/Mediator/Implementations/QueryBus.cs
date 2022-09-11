using System.Reflection;

namespace iman.Domain;

public class QueryBus : IQueryBus
{
    private readonly IServiceFactory _serviceFactory;

    public QueryBus(IServiceFactory serviceFactory)
    {
        _serviceFactory = serviceFactory;
    }

    public async Task<TResponse> ExecuteAsync<TResponse>(
        IQuery<TResponse> query, CancellationToken cancellationToken = default)
    {
        var handler = typeof(IMessageProcessor<,>).MakeGenericType(
            query.GetType(), typeof(TResponse));
        var instance = _serviceFactory.GetInstance(handler);
        var method = instance.GetType().GetTypeInfo()
            .GetMethod(nameof(IMessageProcessor<ICommand<TResponse>, TResponse>.HandleAsync));

        if (method == null)
        {
            throw new InvalidQueryHandlerInstanceException(handler.Name);
        }

        return await (Task<TResponse>)method.Invoke(instance, new object[]
        {
            query, cancellationToken
        })!;
    }
}