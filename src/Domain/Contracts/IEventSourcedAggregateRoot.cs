namespace iman.Domain;

public interface IEventSourcedAggregateRoot : IAggregateRoot
{
    int Version { get; }
}

public interface IEventSourcedAggregateRoot<TId> : IEventSourcedAggregateRoot, IAggregateRoot<TId>
{
    
}