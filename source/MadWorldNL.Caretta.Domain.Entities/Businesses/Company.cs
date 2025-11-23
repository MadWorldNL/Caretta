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
        
        var now = DateTime.UtcNow;
        FoundedAt = new FoundedTime(now);
        UpdatedAt = new UpdatedTime(now);

        Raise(new CompanyFoundedEvent(Id, Name, FoundedAt, UpdatedAt));
    }

    public static Company Found(string name)
    {
        return new Company(name);
    }

    public void Rename(string name)
    {
        if (Name.Value == name)
            return;
        
        Name = new Name(name);
        UpdatedAt = new UpdatedTime(DateTime.UtcNow);
        
        Raise(new CompanyRenamedEvent(Id, Name, UpdatedAt));
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
            default:
                throw new ArgumentOutOfRangeException(nameof(@event), @event, null);
        }
    }

    private void Apply(CompanyFoundedEvent @event)
    {
        Id = @event.CompanyId;
        Name = @event.Name;
        FoundedAt = @event.FoundedAt;
        UpdatedAt = @event.UpdatedAt;
    }

    private void Apply(CompanyRenamedEvent @event)
    {
        _ = @event.CompanyId;
        Name = @event.Name;
        UpdatedAt = @event.UpdatedAt;
    }
}