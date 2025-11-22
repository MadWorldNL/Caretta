using DotNet.Testcontainers.Builders;
using DotNet.Testcontainers.Containers;

namespace MadWorldNL.Caretta.Common.TestContainers;

public class KurrentDbContainerBuilder
{
    private ContainerBuilder _builder = new();
    
    public KurrentDbContainerBuilder()
    {
        _builder = _builder.WithEnvironment("KURRENTDB_CLUSTER_SIZE", "1")
        .WithEnvironment("KURRENTDB_RUN_PROJECTIONS", "All")
        .WithEnvironment("KURRENTDB_START_STANDARD_PROJECTIONS", "true")
        .WithEnvironment("KURRENTDB_INSECURE", "true")
        .WithEnvironment("KURRENTDB_ENABLE_ATOM_PUB_OVER_HTTP", "true");
    }
    
    public KurrentDbContainerBuilder WithVersion(string version)
    {
        _builder = _builder.WithImage($"docker.kurrent.io/kurrent-latest/kurrentdb:{version}");
        
        return this;
    }

    public KurrentDbContainerBuilder WithPort(int port)
    {
        _builder =  _builder.WithPortBinding(port, true)
            .WithWaitStrategy(
                Wait.ForUnixContainer()
                    .UntilInternalTcpPortIsAvailable(port));
        
        return this;
    }
    
    public IContainer Build()
    {
        return _builder.Build();
    }
}