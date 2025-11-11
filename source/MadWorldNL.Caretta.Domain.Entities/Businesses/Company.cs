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
}