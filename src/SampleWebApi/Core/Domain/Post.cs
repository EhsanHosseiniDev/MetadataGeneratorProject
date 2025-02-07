using System.ComponentModel.DataAnnotations;

namespace SampleWebApi.Core.Domain;

public class Post
{
    public int Id { get; set; }
    [MaxLength(50)]
    public string Title { get; set; } = string.Empty;
    [MaxLength(1000)]
    public string Description { get; set; } = string.Empty;
    public PostStatus PostStatus { get; set; }
    public DateTime CreateDateTime { get; set; }
    public DateTime UpdateDateTime { get; set; }
}