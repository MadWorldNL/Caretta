using System.Text.Json.Serialization;
using MadWorldNL.Caretta.DomainDrivenDevelopments.DefaultExceptions;

namespace MadWorldNL.Caretta.DomainDrivenDevelopments.DefaultEntities;

public class Name : ValueObject
{
    public string Value { get; private init; } = string.Empty;
    
    private Name() { } // for ORM

    public Name(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new EmptyException<Name>();
        }
        
        Value = value;
    }
    
    public static Name Empty => new()
    {
        Value = string.Empty
    };
    
    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}