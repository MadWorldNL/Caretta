using MadWorldNL.Caretta.Businesses.ValueObjects;
using MadWorldNL.Caretta.DomainDrivenDevelopments.DefaultEntities;
using MadWorldNL.Caretta.EventStorages;

namespace MadWorldNL.Caretta.Businesses;

public class Company : RootAggregate
{
    public override string AggregateType { get; } = nameof(Company);

    public Name Name { get; private set; } = Name.Empty;
    public FoundedTime FoundedAt { get; private set; } = FoundedTime.Empty;
    
    private Company() { } // for ORM

    private Company(string name)
    {
        Id = new UniqueId(Guid.NewGuid());
        Name = new Name(name);
        FoundedAt = new FoundedTime(DateTime.UtcNow);

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