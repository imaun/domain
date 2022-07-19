namespace iman.Domain;

public interface IEntity
{
    
}

public interface IEntity<TId>
{
    TId Id { get; }
}