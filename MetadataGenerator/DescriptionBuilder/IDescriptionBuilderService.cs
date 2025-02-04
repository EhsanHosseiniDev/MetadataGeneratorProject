using System.Collections.Generic;
using System.Reflection;
using MetadataGenerator.Models;

namespace MetadataGenerator.DescriptionBuilder;

public interface IDescriptionBuilderService
{
    ICollection<PageControllerModel> Generate(Assembly[] assembly, string? key = null);
}