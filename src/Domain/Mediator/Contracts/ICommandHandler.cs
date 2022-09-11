namespace iman.Domain;

public interface ICommandHandler<in TCommand, TResult> : IMessageHandler<TCommand, TResult> 
    where TCommand : ICommand<TResult> 
{
}

public interface ICommandHandler<in TCommand> : IMessageHandler<TCommand>
    where TCommand : ICommand
{
}