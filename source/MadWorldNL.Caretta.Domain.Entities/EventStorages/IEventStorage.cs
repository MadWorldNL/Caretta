namespace MadWorldNL.Caretta.EventStorages;

public interface IEventStorage
{
    Task Store(RootAggregate aggregate);
}