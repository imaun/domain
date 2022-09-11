namespace iman.Domain;

public abstract class CommandHandler<TCommand, TResult> : ICommandHandler<TCommand, TResult> 
    where TCommand : ICommand<TResult> where TResult : new()
{
    public async Task<TResult> HandleAsync(TCommand message,
        CancellationToken cancellationToken)
    {
        await HandleCommandAsync(message, cancellationToken);
        return new TResult();
    }

    protected abstract Task HandleCommandAsync(
        TCommand command,
        CancellationToken cancellationToken);
}

public abstract class CommandHandler<TCommand> : ICommandHandler<TCommand>
    where TCommand : ICommand
{
    public async Task HandleAsync(TCommand message, CancellationToken cancellationToken)
    {
        await HandleCommandAsync(message, cancellationToken);
    }
    
    protected abstract Task HandleCommandAsync(
        TCommand command,
        CancellationToken cancellationToken);
}