namespace iman.Domain;

public class InvalidQueryHandlerInstanceException : Exception
{
    public InvalidQueryHandlerInstanceException(string handlerName)
        :base(handlerName)
    {
    }
    
}