namespace MadWorldNL.Caretta.DomainDrivenDevelopments.DefaultExceptions;

public class NotFoundException<TType>() : Exception($"{typeof(TType)} is not found.")
{
    
}