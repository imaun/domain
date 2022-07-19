namespace iman.Domain;

public interface IAggregateRoot : IEntity
{
    
}

public interface IAggregateRoot<TId> : IAggregateRoot, IEntity<TId>
{
    
}