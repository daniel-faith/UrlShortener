using Microsoft.Extensions.Logging;
using UrlShortener.Application.Commands;
using UrlShortener.Application.Handlers;
using UrlShortener.Application.Interfaces;
using UrlShortener.Common.Constants;
using UrlShortener.Common.Exceptions;

namespace UrlShortener.Application.Tests.Handlers;

public class CreateShortenedUrlCommandHandlerTests
{
    private readonly IUrlRepository _urlRepository = Substitute.For<IUrlRepository>();
    private readonly ILogger<CreateShortenedUrlCommandHandler> _logger = Substitute.For<ILogger<CreateShortenedUrlCommandHandler>>();
    private CreateShortenedUrlCommandHandler _handler;

    [SetUp]
    public void SetUp() =>
        _handler = new CreateShortenedUrlCommandHandler(_urlRepository, _logger);

    [Test]
    public async Task Handle_TooManyGenerationAttempts_ThrowsGenerationException()
    {
        // Arrange
        _urlRepository.ClearReceivedCalls();
        _urlRepository
            .DoesShortCodeExistsForUrlAsync(Arg.Any<string>(), Arg.Any<string>(), Arg.Any<CancellationToken>())
            .ReturnsForAnyArgs(true);

        // Act
        var request = new CreateShortenedUrlCommand { Url = "http://www.test.com" };

        var act = ()  => _handler.Handle(request, CancellationToken.None);

        // Assert
        await act.Should().ThrowAsync<ShortCodeGenerationException>();
        await _urlRepository.Received(UrlShortenerConstants.ShortCodeGenerationMaxAttempts).DoesShortCodeExistsForUrlAsync(Arg.Any<string>(), Arg.Any<string>(), Arg.Any<CancellationToken>());
    }

    [Test]
    public async Task Handle_ValidUrl_Succeeds()
    {
        // Arrange
        const string expectedShortCode = "123456";
        _urlRepository
            .DoesShortCodeExistsForUrlAsync(Arg.Any<string>(), Arg.Any<string>(), Arg.Any<CancellationToken>())
            .ReturnsForAnyArgs(false);

        _urlRepository
            .CreateShortenedUrlWithShortCodeAsync(Arg.Any<string>(), Arg.Any<string>(), Arg.Any<CancellationToken>())
            .ReturnsForAnyArgs(expectedShortCode);

        // Act
        var request = new CreateShortenedUrlCommand { Url = "http://www.test.com" };

        var shortCode = await _handler.Handle(request, CancellationToken.None);

        // Assert
        shortCode.Should().NotBeNullOrEmpty();
        shortCode.Should().Be(expectedShortCode);
    }
}