namespace MadWorldNL.Caretta.Endpoints;

public static class CompanyEndpoints
{
    public static void MapCompanyEndpoints(this WebApplication app)
    {
        var companyEndpoints = app.MapGroup("Company");

        companyEndpoints.MapGet("/Load", () => "Hello World!");
        companyEndpoints.MapPost("/StartNewCompany", () => "Hello World!");
    }
}