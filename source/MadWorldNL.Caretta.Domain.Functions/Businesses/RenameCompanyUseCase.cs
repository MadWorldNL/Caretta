using MadWorldNL.Caretta.DomainDrivenDevelopments.DefaultEntities;
using MadWorldNL.Caretta.DomainDrivenDevelopments.DefaultExceptions;
using MadWorldNL.Caretta.EventStorages;

namespace MadWorldNL.Caretta.Businesses;

public class RenameCompanyUseCase(IEventStorage eventStorage)
{
    public async Task<UniqueId> Execute(Guid id, string name)
    {
        var company = eventStorage.GetById<Company>(id);

        return await company.Match(
            async c => await RenameCompany(c, name), 
            () => throw new NotFoundException<Company>());
    }

    private async Task<UniqueId> RenameCompany(Company company, string name)
    {
        company.Rename(name);
        await eventStorage.Store(company);
        
        return company.Id;
    }
}