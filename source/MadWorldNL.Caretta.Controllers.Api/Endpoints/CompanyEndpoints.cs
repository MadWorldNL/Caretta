using LanguageExt;
using MadWorldNL.Caretta.Businesses;
using Microsoft.AspNetCore.Mvc;

namespace MadWorldNL.Caretta.Endpoints;

public static class CompanyEndpoints
{
    public static void MapCompanyEndpoints(this WebApplication app)
    {
        var companyEndpoints = app.MapGroup("Company");

        companyEndpoints.MapGet("/Load/{Id}", ([FromQuery] string id, [FromServices] LoadCompanyUseCase useCase) =>
        {
            var guidId = Guid.Parse(id);
            
            var company = useCase.Query(guidId);
            return company.Match(Results.Ok, () => Results.NotFound());
        });
        
        companyEndpoints.MapPost("/StartNewCompany", ([FromBody] StartNewCompanyRequest request, [FromServices] StartNewCompanyUseCase useCase) =>
        {
            return useCase.Execute(request.Name);
        });
    }
}