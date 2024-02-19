using System.Diagnostics.CodeAnalysis;
using Microsoft.Extensions.DependencyInjection;

namespace UrlShortener.Application.Configuration;

[ExcludeFromCodeCoverage(Justification = "Dependency Injection")]
public static class DependencyInjectionExtensions
{
    public static void AddApplicationDependencies(this IServiceCollection serviceCollection) =>
        serviceCollection.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(DependencyInjectionExtensions).Assembly));
}