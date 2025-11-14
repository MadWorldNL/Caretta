using MadWorldNL.Caretta.EventStorages;

namespace MadWorldNL.Caretta.Businesses;

public class LoadCompanyUseCase(IEventStorage eventStorage)
{
    public Option<Company> Query(Guid id)
    {
        return eventStorage.GetById<Company>(id);
    }
}