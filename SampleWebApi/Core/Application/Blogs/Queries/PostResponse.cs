using MetadataGenerator.Attributes;
using SampleWebApi.Core.Domain;

namespace SampleWebApi.Core.Application.Blogs.Queries;

public class PostResponse
{
    [IsId,Hide]
    public int Id { get; set; }
    [Searchable,PinAble]
    public string Title { get; set; } = string.Empty;
    [Sortable]
    public DateTime CreateDateTime { get; set; }
    public DateTime? UpdateDateTime { get; set; }
    [PinAble]
    public PostStatus PostStatus { get; set; }
}