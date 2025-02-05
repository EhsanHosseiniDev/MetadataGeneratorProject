using MetadataGenerator.Definitions;

namespace SampleWebApi.Core.Application.Blogs.Commands;

public class DeletePostCommand : ICommand
{
    public int Id { get; set; }
}