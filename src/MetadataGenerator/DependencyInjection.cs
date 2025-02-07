using System;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace MetadataGenerator;

public static class DependencyInjection
{
    public static void AddMetadataGeneration(this IServiceCollection services, Assembly[] assemblies, string name, string description, Guid version)
    {
        var metadataGeneration = new MetadataGenerator(assemblies, name, description, version);
        services.AddSingleton<IMetadataGenerator>(metadataGeneration);
    }
}