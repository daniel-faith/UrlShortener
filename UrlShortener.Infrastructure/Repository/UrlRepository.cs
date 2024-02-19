using Microsoft.EntityFrameworkCore;
using UrlShortener.Application.Interfaces;
using UrlShortener.Common.Exceptions;
using UrlShortener.Infrastructure.Context;
using UrlShortener.Infrastructure.Models;

namespace UrlShortener.Infrastructure.Repository;

public class UrlRepository : IUrlRepository
{
    private readonly UrlDbContext _dbContext;

    public UrlRepository(UrlDbContext dbContext) =>
        _dbContext = dbContext;

    public async Task<string> GetUrlFromShortCodeAsync(string shortCode, CancellationToken cancellationToken)
    {
        var urlShortCode = await _dbContext.UrlShortCodes.FirstOrDefaultAsync(x => x.ShortCode.Equals(shortCode), cancellationToken: cancellationToken);
        if (urlShortCode == null)
        {
            throw new EntityNotFoundException("Entity not found");
        }

        return urlShortCode.Url;
    }

    public async Task<string> CreateShortenedUrlWithShortCodeAsync(string url, string shortCode, CancellationToken cancellationToken)
    {
        _dbContext.UrlShortCodes.Add(new UrlShortCode
        {
            Id = Guid.NewGuid(),
            Url = url,
            ShortCode = shortCode,
            IsExpired = false,
        });

        await _dbContext.SaveChangesAsync(cancellationToken);

        return shortCode;
    }

    public async Task<bool> DoesShortCodeExistsForUrlAsync(string url, string shortCode, CancellationToken cancellationToken)
    {
        var urlShortCode = await _dbContext.UrlShortCodes.FirstOrDefaultAsync(x => x.ShortCode.Equals(shortCode), cancellationToken: cancellationToken);
        return urlShortCode is not null;
    }
}