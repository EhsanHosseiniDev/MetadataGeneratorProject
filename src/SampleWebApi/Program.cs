using MetadataGenerator;
using Microsoft.EntityFrameworkCore;
using SampleWebApi.Infrastructure;

namespace SampleWebApi;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddDbContext<ApplicationDbContext>(option =>
        {
            option.UseInMemoryDatabase("Sample");
        });
        builder.Services.AddControllers();

        builder.Services.AddMetadataGeneration(
            AppDomain.CurrentDomain.GetAssemblies(),
            name: "sample",
            description: "description",
            version: Guid.NewGuid()
        );

        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();


        app.MapControllers();

        app.Run();
    }
}