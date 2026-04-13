using System;

namespace Moedelo.Common.Jwt.Abstractions.Exceptions;

/// <summary>
/// Исключение, возникающее при ошибках работы с JWT токенами (декодирование, верификация подписи).
/// </summary>
public class JwtException : Exception
{
    /// <summary>
    /// Создает новый экземпляр исключения <see cref="JwtException"/> без внутреннего исключения.
    /// </summary>
    public JwtException()
        : this(null)
    {
    }

    /// <summary>
    /// Создает новый экземпляр исключения <see cref="JwtException"/> с внутренним исключением.
    /// </summary>
    /// <param name="inner">Внутреннее исключение, вызвавшее ошибку работы с JWT.</param>
    public JwtException(Exception inner)
        : base("Unable to decode JWT", inner)
    {
    }
}