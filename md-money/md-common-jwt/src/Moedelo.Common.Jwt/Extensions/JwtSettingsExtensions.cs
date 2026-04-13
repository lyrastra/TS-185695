using Moedelo.Common.Settings.Abstractions;
using Moedelo.Common.Settings.Abstractions.Models;

namespace Moedelo.Common.Jwt.Extensions;

/// <summary>
/// Методы расширения для работы с настройками JWT из <see cref="ISettingRepository"/>.
/// </summary>
internal static class JwtSettingsExtensions
{
    /// <summary>
    /// Получает серийный номер(а) сертификата X.509 для подписи JWT.
    /// </summary>
    /// <param name="settingRepository">Репозиторий настроек.</param>
    /// <returns>Значение настройки "IdX509SerialNumber" (может содержать несколько серийных номеров через запятую).</returns>
    internal static SettingValue GetCertificateSerial(this ISettingRepository settingRepository)
    {
        return settingRepository.Get("IdX509SerialNumber");
    }

    /// <summary>
    /// Получает настройку проверки подписи JWT.
    /// </summary>
    /// <param name="settingRepository">Репозиторий настроек.</param>
    /// <returns>Значение настройки "CheckJwtSign" (true - проверять подпись, false - не проверять).</returns>
    internal static SettingValue GetSignJwt(this ISettingRepository settingRepository)
    {
        return settingRepository.Get("CheckJwtSign");
    }
}