namespace iman.Domain;

public interface IDomainIdentity<out TId> where TId : notnull
{
    TId Value { get; }
}