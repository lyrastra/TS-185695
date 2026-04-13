using System;

namespace Moedelo.Infrastructure.Consul.Exceptions;

/// <summary>
/// Исключение, возникающее при проблемах с созданием сессии Consul.
/// </summary>
public class ConsulSessionCreationException : Exception
{
    /// <summary>
    /// Имя сессии, которую не удалось создать.
    /// </summary>
    public string SessionName { get; }

    public ConsulSessionCreationException(string sessionName, string message)
        : base(message)
    {
        SessionName = sessionName;
    }

    public ConsulSessionCreationException(string sessionName, string message, Exception innerException)
        : base(message, innerException)
    {
        SessionName = sessionName;
    }
}
