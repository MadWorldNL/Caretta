using MadWorldNL.Caretta.EventStorages;

namespace MadWorldNL.Caretta.Businesses;

public class StartNewCompanyUseCase(IEventStorage eventStorage)
{
    public async Task<Guid> Execute(string name)
    {
        var company = Company.Found(name);
        
        await eventStorage.Store(company);
        
        return company.Id;
    }
}