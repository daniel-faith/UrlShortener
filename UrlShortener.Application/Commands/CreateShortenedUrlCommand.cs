using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using MediatR;
using UrlShortener.Common.Constants;

namespace UrlShortener.Application.Commands;

[ExcludeFromCodeCoverage(Justification = "Request Model")]
public class CreateShortenedUrlCommand : IRequest<string>
{
    [Required]
    [MaxLength(UrlShortenerConstants.MaxUrlLength)]
    public string Url { get; set; } = null!;
}