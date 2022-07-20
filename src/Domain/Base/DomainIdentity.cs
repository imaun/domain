namespace iman.Domain;

public class DomainIdentity<TId> : IDomainIdentity<TId> where TId : notnull
{
    
    public TId Value { get; protected set; }

    protected DomainIdentity(TId value) => Value = value;

    public static implicit operator TId(DomainIdentity<TId> id)
        => id.Value;

    public override string ToString()
    {
        return $"{GetType().Name} {Value.ToString()}";
    }
}

public abstract class DomainId : DomainIdentity<Guid>
{
    protected DomainId(Guid value) : base(value)
    {
    }
    
}