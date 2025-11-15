namespace MadWorldNL.Caretta.Businesses;

public static class CompanyExtensions
{
    public static LoadCompanyResponse ToResponse(this Company company)
    {
        return new LoadCompanyResponse(company.Id.Value, company.Name.Value, company.FoundedAt.Value);
    }
}