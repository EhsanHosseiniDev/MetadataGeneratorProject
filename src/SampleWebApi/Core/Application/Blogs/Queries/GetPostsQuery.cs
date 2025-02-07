using MetadataGenerator.Definitions;

namespace SampleWebApi.Core.Application.Blogs.Queries;

public class GetPostsQuery : Pagination, IQuery<PostResponse>, IPagination
{
    public string? Title { get; set; }
}