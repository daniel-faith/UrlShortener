using System.Diagnostics.CodeAnalysis;
using Microsoft.Extensions.DependencyInjection;
using UrlShortener.Application.Interfaces;
using UrlShortener.Infrastructure.Repository;

namespace UrlShortener.Infrastructure.DependencyInjection;

[ExcludeFromCodeCoverage(Justification = "Dependency Injection")]
public static class DependencyInjectionExtensions
{
    public static void AddInfrastructureDependencies(this IServiceCollection serviceCollection) =>
        serviceCollection.AddTransient<IUrlRepository, UrlRepository>();
}