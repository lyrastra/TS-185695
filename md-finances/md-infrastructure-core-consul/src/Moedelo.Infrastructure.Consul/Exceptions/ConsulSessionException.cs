using System;

namespace Moedelo.Infrastructure.Consul.Exceptions;

/// <summary>
/// Исключение, возникающее при проблемах с сессией Consul.
/// </summary>
public class ConsulSessionException : Exception
{
    /// <summary>
    /// ID сессии, с которой возникла проблема.
    /// </summary>
    public string SessionId { get; }

    /// <summary>
    /// Тип проблемы с сессией.
    /// </summary>
    public ConsulSessionProblemType ProblemType { get; }

    public ConsulSessionException(string sessionId, ConsulSessionProblemType problemType, string message)
        : base(message)
    {
        SessionId = sessionId;
        ProblemType = problemType;
    }

    public ConsulSessionException(string sessionId, ConsulSessionProblemType problemType, string message, Exception innerException)
        : base(message, innerException)
    {
        SessionId = sessionId;
        ProblemType = problemType;
    }
}