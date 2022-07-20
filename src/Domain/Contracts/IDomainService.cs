namespace iman.Domain;

/// <summary>
/// Domain Service for <see cref="IAggregateRoot"/> or <see cref="IEntity"/> models.
/// </summary>
/// <typeparam name="T"></typeparam>
public interface IDomainService<T> : IDomainService where T : IAggregateRoot, IEntity
{
    
}

/// <inheritdoc cref="IDomainService{T}"/> 
public interface IDomainService
{
    
}