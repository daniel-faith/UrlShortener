using System.Diagnostics.CodeAnalysis;
using System.Runtime.Serialization;

namespace UrlShortener.Common.Exceptions;

[Serializable]
[ExcludeFromCodeCoverage(Justification = "Exception")]
public class ShortCodeGenerationException : Exception
{
    public ShortCodeGenerationException()
    {
    }

    protected ShortCodeGenerationException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }

    public ShortCodeGenerationException(string? message) : base(message)
    {
    }

    public ShortCodeGenerationException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}