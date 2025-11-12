using MadWorldNL.Caretta.Businesses;
using Microsoft.AspNetCore.Mvc;

namespace MadWorldNL.Caretta.Endpoints;

public static class CompanyEndpoints
{
    public static void MapCompanyEndpoints(this WebApplication app)
    {
        var companyEndpoints = app.MapGroup("Company");

        companyEndpoints.MapGet("/Load/{Id}", async ([FromQuery] string id, [FromServices] LoadCompanyUseCase useCase) =>
        {
            var guidId = Guid.Parse(id);
            
            return await useCase.Query(guidId);
        });
        
        companyEndpoints.MapPost("/StartNewCompany", () =>
        {
            return "Hello World!";
        });
    }
}