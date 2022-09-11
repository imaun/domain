namespace iman.Domain;

public class InvalidCommandHandlerInstanceException : Exception
{
    public InvalidCommandHandlerInstanceException(string commandHandlerName)
        : base(message: $"The instance is not the proper handler '{commandHandlerName}' ")
    {
    }
    
    public string CommandHandlerName { get; }
}