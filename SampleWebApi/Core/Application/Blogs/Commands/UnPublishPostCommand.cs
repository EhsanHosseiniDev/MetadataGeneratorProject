using MetadataGenerator.Definitions;

namespace SampleWebApi.Core.Application.Blogs.Commands;

public class UnPublishPostCommand : ICommand
{
    public int Id { get; set; }
}