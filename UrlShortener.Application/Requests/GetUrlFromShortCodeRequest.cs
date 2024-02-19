using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using MediatR;

namespace UrlShortener.Application.Requests;

[ExcludeFromCodeCoverage(Justification = "Request Model")]
public class GetUrlFromShortCodeRequest : IRequest<string>
{
    [Required]
    public string ShortCode { get; set; } = null!;
}