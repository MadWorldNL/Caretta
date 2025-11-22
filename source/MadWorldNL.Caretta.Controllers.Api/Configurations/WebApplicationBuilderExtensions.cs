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
        var connectionString = builder.Configuration.GetValue<string>("KurrentDB:ConnectionString") ?? throw new Exception("No connection string found for KurrentDB");
        var dbSettings = KurrentDBClientSettings.Create(connectionString);
        
        builder.Services.AddSingleton(new KurrentDBClient(dbSettings));
        builder.Services.AddSingleton<IEventStorage, EventDbContext>();
    }
}