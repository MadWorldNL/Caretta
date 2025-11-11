using MadWorldNL.Caretta;

var builder = DistributedApplication.CreateBuilder(args);

var eventStore = builder
    .AddContainer(ContainerDefinitions.EventStoreDb, "docker.kurrent.io/kurrent-latest/kurrentdb:25.1")
    .WithEnvironment(e =>
    {
        e.EnvironmentVariables["KURRENTDB_CLUSTER_SIZE"] = 1;
        e.EnvironmentVariables["KURRENTDB_RUN_PROJECTIONS"] = "All";
        e.EnvironmentVariables["KURRENTDB_START_STANDARD_PROJECTIONS"] = true;
        e.EnvironmentVariables["KURRENTDB_INSECURE"] = true;
        e.EnvironmentVariables["KURRENTDB_ENABLE_ATOM_PUB_OVER_HTTP"] = true;
    })
    .WithEndpoint(2113, 2113);

var api = builder.AddProject<Projects.Api>(ProjectDefinitions.Api)
    .WithUrlForEndpoint("http", c => c.DisplayText = "ApiInsecure")
    .WithUrlForEndpoint("https", c => c.DisplayText = "ApiSecure")
    .WithExternalHttpEndpoints()
    .WaitFor(eventStore);

builder.AddProject<Projects.Web>(ProjectDefinitions.Web)
    .WithUrlForEndpoint("http", c => c.DisplayText = "WebInsecure")
    .WithUrlForEndpoint("https", c => c.DisplayText = "WebSecure")
    .WithExternalHttpEndpoints()
    .WaitFor(api);

builder.Build().Run();