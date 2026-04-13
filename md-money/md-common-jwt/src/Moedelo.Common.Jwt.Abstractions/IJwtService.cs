using System.Collections.Generic;

namespace Moedelo.Common.Jwt.Abstractions;

/// <summary>
/// Сервис для работы с JWT токенами: кодирование, декодирование и проверка подписи.
/// </summary>
public interface IJwtService
{
    /// <summary>
    /// Декодирует JWT токен и возвращает payload в виде объекта типа <typeparamref name="T"/>.
    /// </summary>
    /// <typeparam name="T">Тип объекта для десериализации payload.</typeparam>
    /// <param name="token">JWT токен в формате Base64.</param>
    /// <returns>Объект с данными из payload токена.</returns>
    /// <exception cref="Exceptions.JwtException">Если токен невалиден, подпись не совпадает или произошла ошибка декодирования.</exception>
    T Decode<T>(string token) where T : class;

    /// <summary>
    /// Кодирует объект в JWT токен с опциональными заголовками.
    /// </summary>
    /// <param name="payload">Объект для кодирования в payload токена.</param>
    /// <param name="headers">Дополнительные заголовки JWT (опционально).</param>
    /// <returns>JWT токен в формате Base64.</returns>
    string Encode(object payload, IDictionary<string, object> headers = null);

    /// <summary>
    /// Извлекает заголовки (headers) из JWT токена без проверки подписи.
    /// </summary>
    /// <param name="token">JWT токен в формате Base64.</param>
    /// <returns>Словарь с заголовками токена.</returns>
    IDictionary<string, object> Headers(string token);

    /// <summary>
    /// Проверяет, является ли токен приватным (содержит заголовок "IsPrivate").
    /// </summary>
    /// <param name="token">JWT токен в формате Base64.</param>
    /// <returns><c>true</c>, если токен содержит заголовок "IsPrivate"; иначе <c>false</c>.</returns>
    bool IsPrivate(string token);
}