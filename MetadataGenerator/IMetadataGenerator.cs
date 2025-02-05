using MetadataGenerator.Models;

namespace MetadataGenerator;

public interface IMetadataGenerator
{
    public ApplicationModel ApplicationDescription { get; }
}