namespace iman.Domain;

public interface IValueObject
{
}

public interface IValueObjectCollection<TValue>
{ 
    ICollection<TValue> Items { get; }

    void Add(TValue value);

    void ClearAll();
}