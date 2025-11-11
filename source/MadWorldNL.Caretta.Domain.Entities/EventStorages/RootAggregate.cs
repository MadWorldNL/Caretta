namespace MadWorldNL.Caretta.EventStorages;

public abstract class RootAggregate
{
    public string AggregateId => $"{AggregateType}-{Id}";
    public abstract string AggregateType { get; }
    
    public Guid Id { get; protected set; }
    
    private readonly List<IDomainEvent> _domainEvents = new();
    public IReadOnlyCollection<IDomainEvent> DomainEvents => _domainEvents.AsReadOnly();
    
    protected void AddDomainEvent(IDomainEvent @event)
    {
        _domainEvents.Add(@event);
    }

    public void ClearDomainEvents() => _domainEvents.Clear();
    
    public abstract void Apply(IDomainEvent @event);
}