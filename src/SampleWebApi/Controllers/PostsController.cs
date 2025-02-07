using System.ComponentModel.DataAnnotations;
using MetadataGenerator;
using MetadataGenerator.Models;
using Microsoft.AspNetCore.Mvc;
using SampleWebApi.Core.Application.Blogs.Queries;

namespace SampleWebApi.Controllers;

[Route("api/[controller]/[action]")]
[ApiController]
[Display(Name = "Website Posts")]
public class PostsController(IMetadataGenerator metadataGenerator) : ControllerBase
{
    [HttpGet]
    public ApplicationModel GetApplicationDescription()
    {
        return metadataGenerator.ApplicationDescription;
    }

    [HttpPost]
    public async Task<PostResponse> GetPostsAsync([FromQuery] GetPostsQuery query)
    {
        await Task.Delay(500);
        return new()
        {

        };
    }

    [HttpPost]
    public async Task<PostResponse> CreatePostsAsync([FromQuery] GetPostsQuery query)
    {
        await Task.Delay(500);
        return new()
        {

        };
    }
}