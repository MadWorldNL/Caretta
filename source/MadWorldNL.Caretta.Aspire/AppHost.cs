using MadWorldNL.Caretta;

var builder = DistributedApplication.CreateBuilder(args);

builder.AddProject<Projects.Api>(ProjectDefinitions.Api)
    .WithUrlForEndpoint("http", c => c.DisplayText = "ApiInsecure")
    .WithUrlForEndpoint("https", c => c.DisplayText = "ApiSecure")
    .WithExternalHttpEndpoints();

builder.Build().Run();