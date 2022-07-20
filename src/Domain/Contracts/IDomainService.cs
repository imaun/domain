namespace iman.Domain;

public interface IDomainService<T> : IDomainService where T : IAggregateRoot
{
    
}

public interface IDomainService
{
    
}