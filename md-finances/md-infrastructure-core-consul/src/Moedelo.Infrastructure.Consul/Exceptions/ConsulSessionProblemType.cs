namespace Moedelo.Infrastructure.Consul.Exceptions;

/// <summary>
/// Типы проблем с сессией Consul.
/// </summary>
public enum ConsulSessionProblemType
{
    /// <summary>
    /// Сессия не найдена (404 Not Found).
    /// </summary>
    NotFound = 0,
    
    /// <summary>
    /// Сессия недействительна (500 Internal Server Error с "invalid session").
    /// </summary>
    Invalid = 1,
    
    /// <summary>
    /// Другая проблема с сессией.
    /// </summary>
    Other = 2
}
