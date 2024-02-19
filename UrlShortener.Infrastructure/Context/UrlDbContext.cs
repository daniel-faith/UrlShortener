using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using UrlShortener.Infrastructure.Models;

namespace UrlShortener.Infrastructure.Context;

[ExcludeFromCodeCoverage(Justification = "DbContext")]
public class UrlDbContext : DbContext
{
    public UrlDbContext(DbContextOptions options)
        : base(options)
    {
    }

    public DbSet<UrlShortCode> UrlShortCodes { get; set; } = null!;
}