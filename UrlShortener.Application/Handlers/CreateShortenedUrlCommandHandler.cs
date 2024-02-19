using MediatR;
using Microsoft.Extensions.Logging;
using UrlShortener.Application.Commands;
using UrlShortener.Application.Interfaces;
using UrlShortener.Application.Services;
using UrlShortener.Common.Constants;
using UrlShortener.Common.Exceptions;

namespace UrlShortener.Application.Handlers;

public class CreateShortenedUrlCommandHandler : IRequestHandler<CreateShortenedUrlCommand, string>
{
    private readonly IUrlRepository _urlRepository;
    private readonly ILogger _logger;

    public CreateShortenedUrlCommandHandler(IUrlRepository urlRepository, ILogger<CreateShortenedUrlCommandHandler> logger)
    {
        _urlRepository = urlRepository;
        _logger = logger;
    }

    public async Task<string> Handle(CreateShortenedUrlCommand request, CancellationToken cancellationToken)
    {
        ValidationService.IsUrlValid(request.Url);

        var shortCode = RandomStringGeneratorService.GenerateRandomShortCodeString();

        var attempts = 0;
        bool shortCodeAlreadyExists;
        do
        {
            shortCodeAlreadyExists = await _urlRepository.DoesShortCodeExistsForUrlAsync(request.Url, shortCode, cancellationToken);
            shortCode = RandomStringGeneratorService.GenerateRandomShortCodeString();
            attempts++;
        } while (attempts < UrlShortenerConstants.ShortCodeGenerationMaxAttempts);

        if (shortCodeAlreadyExists)
        {
            throw new ShortCodeGenerationException("There was an issue generating the short code, please try again later");
        }

        _logger.LogInformation("Creating short code for Url: {Url}", request.Url);
        return await _urlRepository.CreateShortenedUrlWithShortCodeAsync(request.Url, RandomStringGeneratorService.GenerateRandomShortCodeString(), cancellationToken);
    }
}