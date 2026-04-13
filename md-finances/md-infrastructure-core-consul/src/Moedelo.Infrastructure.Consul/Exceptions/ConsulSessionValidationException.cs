using System;

namespace Moedelo.Infrastructure.Consul.Exceptions;

/// <summary>
/// Исключение, возникающее при проблемах с валидацией сессии Consul.
/// </summary>
public class ConsulSessionValidationException : Exception
{
    /// <summary>
    /// ID сессии, которую не удалось валидировать.
    /// </summary>
    public string SessionId { get; }

    public ConsulSessionValidationException(string sessionId, string message)
        : base(message)
    {
        SessionId = sessionId;
    }

    public ConsulSessionValidationException(string sessionId, string message, Exception innerException)
        : base(message, innerException)
    {
        SessionId = sessionId;
    }
}