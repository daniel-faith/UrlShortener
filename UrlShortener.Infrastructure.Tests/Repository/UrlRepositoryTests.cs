using Microsoft.EntityFrameworkCore;
using UrlShortener.Common.Exceptions;
using UrlShortener.Infrastructure.Context;
using UrlShortener.Infrastructure.Models;
using UrlShortener.Infrastructure.Repository;

namespace UrlShortener.Infrastructure.Tests.Repository;

public class UrlRepositoryTests
{
    private UrlDbContext _dbContext;
    private UrlRepository _urlRepository;

    [SetUp]
    public void SetUp()
    {
        var options = new DbContextOptionsBuilder<UrlDbContext>()
            .UseInMemoryDatabase("UrlShortenerTest")
            .Options;
        
        _dbContext = new UrlDbContext(options);

        _dbContext.UrlShortCodes.Add(new UrlShortCode
        {
            Id = Guid.NewGuid(),
            Url = "http://www.google.com",
            ShortCode = "123456",
            IsExpired = false,
        });

        _dbContext.SaveChanges();
        
        _urlRepository = new UrlRepository(_dbContext);
    }

    [TearDown]
    public void TearDown()
    {
        _dbContext.Dispose();
    }

    [Test]
    public async Task GetUrlFromShortCodeAsync_ForNoMatchingShortCode_ThrowsEntityNotFoundException()
    {
        var act = () => _urlRepository.GetUrlFromShortCodeAsync("test", CancellationToken.None);
        await act.Should().ThrowAsync<EntityNotFoundException>("Entity not found");
    }

    [Test]
    public async Task CreateShortenedUrlWithShortCodeAsync_AddsToDatabase()
    {
        _dbContext.UrlShortCodes.Should().HaveCount(1);
        await _urlRepository.CreateShortenedUrlWithShortCodeAsync("http://www.testcase.com", "098765", CancellationToken.None);
        _dbContext.UrlShortCodes.Should().HaveCount(2);
    }

    [Test]
    public async Task DoesShortCodeExistsForUrlAsync_ReturnsTrueForExisting()
    {
        var exists = await _urlRepository.DoesShortCodeExistsForUrlAsync("http://www.google.com", "123456", CancellationToken.None);
        exists.Should().BeTrue();
    }

    [Test]
    public async Task DoesShortCodeExistsForUrlAsync_ReturnsFalseForNonExisting()
    {
        var exists = await _urlRepository.DoesShortCodeExistsForUrlAsync("http://www.bing.com", "098765", CancellationToken.None);
        exists.Should().BeTrue();
    }
}