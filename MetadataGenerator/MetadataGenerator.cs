using System;
using System.Linq;
using System.Reflection;
using MetadataGenerator.Extensions;
using MetadataGenerator.Models;

namespace MetadataGenerator;

internal class MetadataGenerator(Assembly[] assemblies, string name, string description, Guid version) : IMetadataGenerator
{
    private ApplicationModel? _applicationDescription;
    public ApplicationModel ApplicationDescription => _applicationDescription ?? GenerateDescriptionAndFillProperty();

    private ApplicationModel GenerateDescriptionAndFillProperty()
    {
        var controllerTypes = assemblies.GetControllers();

        return _applicationDescription = new()
        {
            Name = name,
            Description = description,
            Version = version,
            Controllers = assemblies.GetControllers().GetControllerModels().ToList()
        };
    }
}