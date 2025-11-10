namespace MadWorldNL.Caretta.EventStorages;

public interface IEventStorage
{
    void Store(IEvent @event);
}