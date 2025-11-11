namespace MadWorldNL.Caretta.EventStorages;

public abstract class RootAggregate
{
    public abstract string AggregateType { get; }
    
    public Guid Id { get; protected init; }
    
    private readonly List<IDomainEvent> _domainEvents = new();
    public IReadOnlyCollection<IDomainEvent> DomainEvents => _domainEvents.AsReadOnly();
    
    protected void AddDomainEvent(IDomainEvent @event)
    {
        _domainEvents.Add(@event);
    }

    public void ClearDomainEvents() => _domainEvents.Clear();
}