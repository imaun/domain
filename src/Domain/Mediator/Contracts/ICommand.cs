namespace iman.Domain;

public interface ICommand : IMessage
{
}

public interface ICommand<TResult> : IMessage<TResult>
{
}