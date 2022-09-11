namespace iman.Domain;

public interface IQueryBus
{

    Task<TResponse> ExecuteAsync<TResponse>(
        IQuery<TResponse> query,
        CancellationToken cancellationToken = default);
    
}