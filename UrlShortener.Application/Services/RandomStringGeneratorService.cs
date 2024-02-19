using UrlShortener.Common.Constants;

namespace UrlShortener.Application.Services;

public static class RandomStringGeneratorService
{
    private static readonly Random Random = new();

    public static string GenerateRandomShortCodeString()
    {
        const string characters = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";

        var randomChars = new char[UrlShortenerConstants.ShortCodeLength];

        return new string(Enumerable.Range(1, randomChars.Length)
            .Select(_ => characters[Random.Next(characters.Length)])
            .ToArray());
    }
}