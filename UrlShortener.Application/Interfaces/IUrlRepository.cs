namespace UrlShortener.Application.Interfaces;

public interface IUrlRepository
{
    Task<string> GetUrlFromShortCodeAsync(string shortCode, CancellationToken cancellationToken);

    Task<string> CreateShortenedUrlWithShortCodeAsync(string url, string shortCode, CancellationToken cancellationToken);

    Task<bool> DoesShortCodeExistsForUrlAsync(string url, string shortCode, CancellationToken cancellationToken);
}