using System.ComponentModel.DataAnnotations;
using MediatR;
using Microsoft.Extensions.Logging;
using NSubstitute.ExceptionExtensions;
using NUnit.Framework;
using UrlShortener.Application.Commands;
using UrlShortener.Controllers;

namespace UrlShortener.Api.Tests.Controllers;

public class UrlControllerTests
{
    private UrlController _controller;
    private readonly ILogger<UrlController> _logger = Substitute.For<ILogger<UrlController>>();
    private readonly ISender _sender = Substitute.For<ISender>();
    
    [SetUp]
    public void SetUp()
    {
        _controller = new UrlController(_sender, _logger);
    }

    [Test]
    public void CreateShortenedUrlAsync_UrlRequired()
    {
        var command = new CreateShortenedUrlCommand
        {
            Url = string.Empty,
        };
        _controller.CreateShortenedUrlAsync(command).ThrowsAsync(new ValidationException());
    }
}