using System.Linq.Expressions;

namespace iman.Domain;

/// <summary>
/// Repository over an <see cref="IAggregateRoot"/> or an <see cref="IEntity"/>
/// with the specified <see cref="TKey"/>
/// </summary>
/// <typeparam name="T"></typeparam>
/// <typeparam name="TKey"></typeparam>
public interface IDomainRepository<T, TKey> 
    where T : IAggregateRoot<TKey>, IEntity<TKey>
{
    void Add(T entity);

    void Remove(T entity);

    T FindByKey(TKey key);

    Task<T> FindByKeyAsync(TKey key);

    T FirstOrDefault(Expression<Func<T, bool>> predicate);

    Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> predicate);

    T SingleOrDefault(Expression<Func<T, bool>> predicate);

    Task<T> SingleOrDefaultAsync(Expression<Func<T, bool>> predicate);

    IEnumerable<T> GetAll();

    Task<IEnumerable<T>> GetAllAsync();

    IEnumerable<T> FindAll(Expression<Func<T, bool>> predicate);

    Task<IEnumerable<T>> FindAllAsync(Expression<Func<T, bool>> predicate);
    
}