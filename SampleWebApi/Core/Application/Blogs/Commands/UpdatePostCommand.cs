using MetadataGenerator.Definitions;

namespace SampleWebApi.Core.Application.Blogs.Commands;

public class UpdatePostCommand : ICommand
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
}