namespace Moedelo.AccountV2.Dto.ExternalApi;

public enum ApiKeyAuthorizationStatus
{
    /// <summary>
    /// Авторизован
    /// </summary>
    Authorized = 0,
    /// <summary>
    /// Недопустимая метка времени
    /// </summary>
    InvalidTimestamp = 1,
    /// <summary>
    /// Неверная подпись
    /// </summary>
    InvalidDigest = 2,
    /// <summary>
    /// Предъявлен невалидный токен
    /// </summary>
    TokenIsInvalid = 3,
    /// <summary>
    /// Ключ найден, но он уже отозван или отключен
    /// </summary>
    ApiKeyIsInactive = 4
}