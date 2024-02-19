using System.Diagnostics.CodeAnalysis;

namespace UrlShortener.Infrastructure.Models;

[ExcludeFromCodeCoverage(Justification = "DbContext Model")]
public class UrlShortCode
{
    public Guid Id { get; set; }
    
    public string Url { get; set; }
    
    public string ShortCode { get; set; }
    
    public bool IsExpired { get; set; }
}