using Microsoft.EntityFrameworkCore;
using SampleWebApi.Core.Domain;

namespace SampleWebApi.Infrastructure;

public class ApplicationDbContext(DbContextOptions options) : DbContext(options)
{
    public DbSet<Post> Posts { get; set; }
}