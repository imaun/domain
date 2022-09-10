namespace iman.Domain;

public delegate object ServiceFactoryDelegate(Type type);

public interface IServiceFactory
{
    object GetInstance(Type T);
}