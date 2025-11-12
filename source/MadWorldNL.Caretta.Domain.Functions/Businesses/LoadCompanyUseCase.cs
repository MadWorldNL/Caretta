using MadWorldNL.Caretta.EventStorages;

namespace MadWorldNL.Caretta.Businesses;

public class LoadCompanyUseCase(IEventStorage eventStorage)
{
    public async Task<Company> Query(Guid id)
    {
        return await eventStorage.GetById<Company>(id);
    }
}