using UrlShortener.Application.Services;
using UrlShortener.Common.Exceptions;

namespace UrlShortener.Application.Tests.Services;

public class ValidationServiceTests
{
    [TestCase("")]
    [TestCase(" ")]
    [TestCase("test")]
    [TestCase("123")]
    [TestCase("htp://www.google.com")]
    [TestCase("http:/www.google.com")]
    [TestCase("httsp://")]
    [TestCase("httsp://www")]
    [TestCase("www.google.com")] // Validation service assumes fully qualified url with http or https
    public void IsUrlValid_ForInvalidUrl_ThrowsValidationException(string url)
    {
        var act = () => ValidationService.IsUrlValid(url);
        act.Should().Throw<ValidationException>("Url is not in the correct format");
    }

    [TestCase("http://www.google.com")]
    [TestCase("https://www.google.com")]
    public void IsUrlValid_ForValidUrl_Succeeds(string url)
    {
        var act = () => ValidationService.IsUrlValid(url);
        act.Should().NotThrow<ValidationException>("Url is not in the correct format");
    }
}