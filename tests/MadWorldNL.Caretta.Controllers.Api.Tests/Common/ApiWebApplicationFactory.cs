using DotNet.Testcontainers.Containers;
using MadWorldNL.Caretta.Common.TestContainers;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace MadWorldNL.Caretta.Common;

public class ApiWebApplicationFactory : WebApplicationFactory<Program>
{
    private const int KurrentDbInternalPort = 2113;
    private readonly IContainer _kurrentDbContainer = new KurrentDbContainerBuilder()
        .WithVersion("25.1")
        .WithPort(KurrentDbInternalPort)
        .Build();

    protected override IHost CreateHost(IHostBuilder builder)
    {
        _kurrentDbContainer.StartAsync().GetAwaiter().GetResult();

        var dbPublicPort = _kurrentDbContainer.GetMappedPublicPort(KurrentDbInternalPort);
        builder.ConfigureHostConfiguration(config =>
        {
            var testSettings = new Dictionary<string, string?>
            {
                ["KurrentDB:ConnectionString"] = $"kurrentdb://admin:changeit@localhost:{dbPublicPort}?tls=false&tlsVerifyCert=false"
            };
            
            config.AddInMemoryCollection(testSettings);
        });

        return base.CreateHost(builder);
    }

    public override async ValueTask DisposeAsync()
    {
        await _kurrentDbContainer.DisposeAsync();
        
        await base.DisposeAsync();
        GC.SuppressFinalize(this);
    }
}