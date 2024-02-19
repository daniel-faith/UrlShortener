using MediatR;
using Microsoft.Extensions.Logging;
using UrlShortener.Application.Interfaces;
using UrlShortener.Application.Requests;

namespace UrlShortener.Application.Handlers;

public class GetUrlFromShortCodeRequestHandler : IRequestHandler<GetUrlFromShortCodeRequest, string>
{
    private readonly IUrlRepository _urlRepository;
    private readonly ILogger _logger;

    public GetUrlFromShortCodeRequestHandler(IUrlRepository urlRepository, ILogger<GetUrlFromShortCodeRequestHandler> logger)
    {
        _urlRepository = urlRepository;
        _logger = logger;
    }

    public async Task<string> Handle(GetUrlFromShortCodeRequest request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting Url for short code: {ShortCode}", request.ShortCode);
        return await _urlRepository.GetUrlFromShortCodeAsync(request.ShortCode, cancellationToken);
    }
}