using System.Diagnostics.CodeAnalysis;

namespace UrlShortener.Common.Constants;

[ExcludeFromCodeCoverage(Justification = "Constants")]
public static class UrlShortenerConstants
{
    // TODO: Buy snazzy domain name
    private const int ApiBasePort = 5277;
    public static readonly string ApiBaseUrl = $"http://localhost:{ApiBasePort}/";
    public const int MaxUrlLength = 2048;
    public const int ShortCodeGenerationMaxAttempts = 3;
    public const int ShortCodeLength = 6;
}