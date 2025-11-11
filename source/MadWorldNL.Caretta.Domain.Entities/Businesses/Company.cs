using MadWorldNL.Caretta.EventStorages;

namespace MadWorldNL.Caretta.Businesses;

public class Company : RootAggregate
{
    public override string AggregateType { get; } = nameof(Company);

    public string Name { get; private set; } = string.Empty;
    public DateTime FoundedAt { get; private set; }
    
    private Company() { } // for ORM

    private Company(string name)
    {
        Id = Guid.NewGuid();
        Name = name;
        FoundedAt = DateTime.UtcNow;

        AddDomainEvent(new CompanyFoundedEvent(Id, Name, FoundedAt));
    }

    public static Company Found(string name)
    {
        return new Company(name);
    }
    
    public override void Apply(IDomainEvent @event)
    {
        switch (@event)
        {
            case CompanyFoundedEvent companyFoundedEvent: 
                Apply(companyFoundedEvent); 
                break;
        }
    }

    private void Apply(CompanyFoundedEvent @event)
    {
        Id = @event.CompanyId;
        Name = @event.Name;
        FoundedAt = @event.FoundedAt;
    }
}