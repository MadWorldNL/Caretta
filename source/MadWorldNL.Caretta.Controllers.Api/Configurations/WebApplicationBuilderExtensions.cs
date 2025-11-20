using KurrentDB.Client;
using MadWorldNL.Caretta.Businesses;
using MadWorldNL.Caretta.EventStorages;

namespace MadWorldNL.Caretta.Configurations;

public static class WebApplicationBuilderExtensions
{
    public static void AddFunctions(this WebApplicationBuilder builder)
    {
        builder.Services.AddScoped<StartNewCompanyUseCase>();
        builder.Services.AddScoped<RenameCompanyUseCase>();
        builder.Services.AddScoped<LoadCompanyUseCase>();
    }
    
    public static void AddKurrentDb(this WebApplicationBuilder builder)
    {
        var dbSettings = KurrentDBClientSettings.Create("kurrentdb://admin:changeit@localhost:2113?tls=false&tlsVerifyCert=false");
        
        builder.Services.AddSingleton(new KurrentDBClient(dbSettings));
        builder.Services.AddSingleton<IEventStorage, EventDbContext>();
    }
}