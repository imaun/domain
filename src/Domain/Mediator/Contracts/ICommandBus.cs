namespace iman.Domain;

public interface ICommandBus
{
    Task<TResult> ExecuteAsync<TResult>(
        ICommand<TResult> command,
        CancellationToken cancellationToken = default);

    Task ExecuteAsync(ICommand command, CancellationToken cancellationToken = default);
}