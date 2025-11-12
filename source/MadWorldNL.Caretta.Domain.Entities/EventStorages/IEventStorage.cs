namespace MadWorldNL.Caretta.EventStorages;

public interface IEventStorage
{
    Task<TRootAggregate> GetById<TRootAggregate>(Guid id) where TRootAggregate : RootAggregate;
    Task Store(RootAggregate aggregate);
}