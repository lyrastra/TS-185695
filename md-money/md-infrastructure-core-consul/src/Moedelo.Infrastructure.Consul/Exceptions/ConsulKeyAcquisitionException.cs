using System;

namespace Moedelo.Infrastructure.Consul.Exceptions;

/// <summary>
/// Исключение, возникающее при проблемах с захватом ключей в Consul.
/// </summary>
public class ConsulKeyAcquisitionException : Exception
{
    /// <summary>
    /// ID сессии, с которой возникла проблема.
    /// </summary>
    public string SessionId { get; }

    /// <summary>
    /// Путь к ключу, который не удалось захватить.
    /// </summary>
    public string KeyPath { get; }

    public ConsulKeyAcquisitionException(string sessionId, string keyPath, string message)
        : base(message)
    {
        SessionId = sessionId;
        KeyPath = keyPath;
    }

    public ConsulKeyAcquisitionException(string sessionId, string keyPath, string message, Exception innerException)
        : base(message, innerException)
    {
        SessionId = sessionId;
        KeyPath = keyPath;
    }
}
