using MetadataGenerator.Definitions;

namespace SampleWebApi.Core.Application.Blogs.Commands;

public class CreatePostCommand : ICommand<int>
{
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
}