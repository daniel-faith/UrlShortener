using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using UrlShortener.Application.Configuration;
using UrlShortener.Infrastructure.Context;
using UrlShortener.Infrastructure.DependencyInjection;

namespace UrlShortener;

[ExcludeFromCodeCoverage(Justification = "Program")]
internal class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddControllers();
        builder.Services.AddDbContext<UrlDbContext>(opt =>
        {
            // Using an in-memory database for the purpose of the tech test
            opt.UseInMemoryDatabase("UrlShortener");
        });
        builder.Services.AddApplicationDependencies();
        builder.Services.AddInfrastructureDependencies();
        builder.Services.AddLogging(opt =>
        {
            opt.AddConsole();
        });

        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        var app = builder.Build();

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}