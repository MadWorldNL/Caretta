using LanguageExt;
using MadWorldNL.Caretta.Businesses;
using MadWorldNL.Caretta.Default;
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
            return company.Match(
                c => Results.Ok(c.ToResponse()), 
                () => Results.NotFound());
        });
        
        companyEndpoints.MapPost("/StartNewCompany", async ([FromBody] StartNewCompanyRequest request, [FromServices] StartNewCompanyUseCase useCase) =>
        {
            var id = await useCase.Execute(request.Name);
            return new ChangedResponse(id.Value);
        });

        companyEndpoints.MapPost("/RenameCompany",
            async ([FromBody] RenameCompanyRequest request, [FromServices] RenameCompanyUseCase useCase) =>
            {
                var id = await useCase.Execute(request.Id, request.Name);
                return new ChangedResponse(id.Value);
            });
    }
}