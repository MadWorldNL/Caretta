namespace MadWorldNL.Caretta.DomainDrivenDevelopments.DefaultExceptions;

public class EmptyException<TType>() : Exception($"{typeof(TType)} cannot contain empty value.")
{
    
}