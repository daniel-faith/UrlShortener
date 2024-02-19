using System.Diagnostics.CodeAnalysis;
using System.Runtime.Serialization;

namespace UrlShortener.Common.Exceptions;

[Serializable]
[ExcludeFromCodeCoverage(Justification = "Exception")]
public class ValidationException : Exception
{
    public ValidationException()
    {
    }

    protected ValidationException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }

    public ValidationException(string? message) : base(message)
    {
    }

    public ValidationException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}