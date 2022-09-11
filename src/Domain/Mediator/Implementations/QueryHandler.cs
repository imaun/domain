namespace iman.Domain;


public abstract class QueryHandler<TQuery, TResponse> : IQueryHandler<TQuery, TResponse> 
    where TQuery : IQuery<TResponse>
{
    public Task<TResponse> HandleAsync(TQuery message,
        CancellationToken cancellationToken)
    {
        return HandleQueryAsync(message, cancellationToken);
    }

    protected abstract Task<TResponse> HandleQueryAsync(TQuery query, 
        CancellationToken cancellationToken);
}