using MetadataGenerator.Definitions;

namespace SampleWebApi.Core.Application.Blogs.Commands;

public class PublishPostCommand : ICommand
{
    public int Id { get; set; }
}