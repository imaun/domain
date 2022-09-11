namespace iman.Domain;

public class InvalidCommandHandlerInstanceException : Exception
{
    public InvalidCommandHandlerInstanceException(string handlerName)
        : base(message: $"The instance is not the proper handler '{handlerName}' ")
    {
    }
    
}