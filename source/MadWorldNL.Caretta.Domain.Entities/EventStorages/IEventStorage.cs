namespace MadWorldNL.Caretta.EventStorages;

public interface IEventStorage
{
    Option<TRootAggregate> GetById<TRootAggregate>(Guid id) where TRootAggregate : RootAggregate;
    Task Store(RootAggregate aggregate);
}