using MediatR;
using Microsoft.AspNetCore.Mvc;
using UrlShortener.Application.Commands;
using UrlShortener.Application.Requests;
using UrlShortener.Common.Constants;
using UrlShortener.Common.Exceptions;

namespace UrlShortener.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UrlController(ISender mediator, ILogger<UrlController> logger) : ControllerBase
{
    private readonly ILogger _logger = logger;

    [HttpPost("/api/Url/shortenUrl")]
    public async Task<IActionResult> CreateShortenedUrlAsync(CreateShortenedUrlCommand createShortenedUrlCommand)
    {
        _logger.LogInformation($"Entering {nameof(CreateShortenedUrlAsync)}");

        try
        {
            var shortenedUrl = await mediator.Send(createShortenedUrlCommand);
            
            _logger.LogInformation($"Leaving {nameof(CreateShortenedUrlAsync)}");
            
            return new OkObjectResult(UrlShortenerConstants.ApiBaseUrl + shortenedUrl);
        }
        catch (Exception exception) when (exception is ValidationException or ShortCodeGenerationException)
        {
            _logger.LogError(exception, "Error generating short code");
            return new BadRequestObjectResult(exception.Message);
        }
    }
    
    [HttpGet("/{shortCode}")]
    public async Task<IActionResult> GetUrlRedirectByShortCodeAsync(string shortCode)
    {
        _logger.LogInformation($"Entering {nameof(GetUrlRedirectByShortCodeAsync)}");

        try
        {
            var url = await mediator.Send(new GetUrlFromShortCodeRequest { ShortCode = shortCode });

            _logger.LogInformation($"Leaving {nameof(GetUrlRedirectByShortCodeAsync)}");

            return Redirect(url);
        }
        catch (EntityNotFoundException exception)
        {
            _logger.LogError("Url for Short code {ShortCode} was not found", shortCode);
            return new NotFoundObjectResult(exception.Message);
        }
    }
}