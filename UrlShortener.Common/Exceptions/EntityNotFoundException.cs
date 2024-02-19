using System.Diagnostics.CodeAnalysis;
using System.Runtime.Serialization;

namespace UrlShortener.Common.Exceptions;

[Serializable]
[ExcludeFromCodeCoverage(Justification = "Exception")]
public class EntityNotFoundException : Exception
{
    public EntityNotFoundException()
    {
    }

    protected EntityNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }

    public EntityNotFoundException(string? message) : base(message)
    {
    }

    public EntityNotFoundException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}