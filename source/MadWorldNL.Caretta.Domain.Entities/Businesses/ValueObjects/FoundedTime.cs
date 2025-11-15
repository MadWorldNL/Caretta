using System.Text.Json.Serialization;
using MadWorldNL.Caretta.DomainDrivenDevelopments.DefaultExceptions;

namespace MadWorldNL.Caretta.Businesses.ValueObjects;

public class FoundedTime : ValueObject
{
    public DateTime Value { get; private init; } = DateTime.MinValue;
    
    private FoundedTime(){} // for ORM
    
    public FoundedTime(DateTime value)
    {
        if (value == DateTime.MinValue)
        {
            throw new EmptyException<FoundedTime>();
        }

        Value = value;
    }
    
    public static FoundedTime Empty => new()
    {
        Value = DateTime.MinValue
    };
    
    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}