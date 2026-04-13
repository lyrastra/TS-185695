using System.Security.Cryptography;

namespace Moedelo.Common.Jwt.Crypto;

/// <summary>
/// Репозиторий для управления приватными ключами, используемыми для подписи и верификации JWT токенов.
/// Ключи загружаются из хранилища сертификатов Windows на основе серийных номеров из конфигурации.
/// </summary>
public interface IPrivateKeyRepository
{
    /// <summary>
    /// Получает первый доступный приватный ключ для подписи/верификации JWT.
    /// Ключ извлекается из сертификата X509Certificate2 и представлен в виде RSA объекта.
    /// </summary>
    /// <returns>Приватный ключ в виде <see cref="object"/> (обычно <see cref="RSA"/>).</returns>
    object PrivateKey { get; }
}