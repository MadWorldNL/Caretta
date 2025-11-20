using MadWorldNL.Caretta.DomainDrivenDevelopments.DefaultExceptions;

namespace MadWorldNL.Caretta.DomainDrivenDevelopments.DefaultEntities;

public class UpdatedTime : ValueObject
{
    public DateTime Value { get; private init; } = DateTime.MinValue;
    
    private UpdatedTime(){} // for ORM
    
    public UpdatedTime(DateTime value)
    {
        if (value == DateTime.MinValue)
        {
            throw new EmptyException<UpdatedTime>();
        }

        Value = value;
    }
    
    public static UpdatedTime Empty => new()
    {
        Value = DateTime.MinValue
    };
    
    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}