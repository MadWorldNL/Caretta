namespace MadWorldNL.Caretta.DomainDrivenDevelopments.DefaultEntities;

public class UniqueId : ValueObject
{
    public Guid Value { get; private init; }

    private UniqueId() { }
    
    public UniqueId(Guid value)
    {
        Value = value;
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}