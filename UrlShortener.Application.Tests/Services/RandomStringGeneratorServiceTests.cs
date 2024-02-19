using UrlShortener.Application.Services;

namespace UrlShortener.Application.Tests.Services;

public class RandomStringGeneratorServiceTests
{
    // Todo: Inject random service for better mocking in tests (can seed randomizer?)
    [Test]
    public void GenerateRandomShortCodeString_ShouldGenerateDifferentStrings()
    {
        for (var i = 0; i < 100; i++)
        {
            var firstShortCode = RandomStringGeneratorService.GenerateRandomShortCodeString();
            var secondShortCode = RandomStringGeneratorService.GenerateRandomShortCodeString();
            firstShortCode.Should().NotBe(secondShortCode);
        }
    }
}