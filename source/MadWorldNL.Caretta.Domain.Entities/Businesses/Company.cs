using MadWorldNL.Caretta.Businesses.ValueObjects;
using MadWorldNL.Caretta.DomainDrivenDevelopments.DefaultEntities;
using MadWorldNL.Caretta.EventStorages;

namespace MadWorldNL.Caretta.Businesses;

public class Company : RootAggregate
{
    public override string AggregateType { get; } = nameof(Company);

    public Name Name { get; private set; } = Name.Empty;
    public FoundedTime FoundedAt { get; private set; } = FoundedTime.Empty;
    public UpdatedTime UpdatedAt { get; private set; } = UpdatedTime.Empty;
    
    [UsedImplicitly]
    private Company() { } // for ORM

    private Company(string name)
    {
        Id = new UniqueId(Guid.NewGuid());
        Name = new Name(name);
        FoundedAt = new FoundedTime(DateTime.UtcNow);
        UpdatedAt = new UpdatedTime(FoundedAt.Value);

        AddDomainEvent(new CompanyFoundedEvent(Id, Name, FoundedAt, UpdatedAt));
    }

    public static Company Found(string name)
    {
        return new Company(name);
    }

    public void Rename(string name)
    {
        Name = new Name(name);
        UpdatedAt = new UpdatedTime(DateTime.UtcNow);
        
        AddDomainEvent(new CompanyRenamedEvent(Name, UpdatedAt));
    }
    
    public override void Apply(IDomainEvent @event)
    {
        switch (@event)
        {
            case CompanyFoundedEvent companyFoundedEvent: 
                Apply(companyFoundedEvent); 
                break;
            case CompanyRenamedEvent companyRenamedEvent:
                Apply(companyRenamedEvent);
                break;
        }
    }

    private void Apply(CompanyFoundedEvent @event)
    {
        Id = @event.CompanyId;
        Name = @event.Name;
        FoundedAt = @event.FoundedAt;
        UpdatedAt = @event.UpdatedTime;
    }

    private void Apply(CompanyRenamedEvent @event)
    {
        Name = @event.Name;
        UpdatedAt = @event.UpdatedAt;
    }
}