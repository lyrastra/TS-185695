namespace Moedelo.CommonV2.Xss.WebApi.Extensions;

/// <summary>
/// Режим обработки обнаруженных XSS-атак
/// </summary>
public enum XssValidationMode
{
    /// <summary>
    /// Отклонить запросы с XSS-атаками на уровне filter/handler
    /// </summary>
    RejectSuspiciousRequests,
    /// <summary>
    /// Пробросить исключение <see cref="XssValidationException"/> (должно быть обработано на уровне приложения)
    /// </summary>
    ThrowXssException
}
