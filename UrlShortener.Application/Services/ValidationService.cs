using UrlShortener.Common.Constants;
using UrlShortener.Common.Exceptions;

namespace UrlShortener.Application.Services;

public static class ValidationService
{
    public static void IsUrlValid(string url)
    {
        var uriCreated = Uri.TryCreate(url, UriKind.Absolute, out var uriResult)
                         && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);

        if (!uriCreated)
        {
            throw new ValidationException("Url is not in the correct format");
        }

        if (url.Contains(UrlShortenerConstants.ApiBaseUrl))
        {
            throw new ValidationException("Cannot shorten Url shortener Urls");
        }
    }
}