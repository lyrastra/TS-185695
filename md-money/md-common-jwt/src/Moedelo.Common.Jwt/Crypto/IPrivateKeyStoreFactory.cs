using System.Collections.Generic;

namespace Moedelo.Common.Jwt.Crypto;

/// <summary>
/// Фабрика для создания хранилищ приватных ключей (<see cref="IPrivateKeyStore"/>).
/// Загружает сертификаты из хранилища Windows (LocalMachine\My) по серийным номерам.
/// </summary>
public interface IPrivateKeyStoreFactory
{
    /// <summary>
    /// Создает хранилище приватных ключей на основе серийных номеров сертификатов X.509.
    /// </summary>
    /// <param name="x509serials">Коллекция серийных номеров сертификатов для загрузки.</param>
    /// <returns>Экземпляр <see cref="IPrivateKeyStore"/> с загруженными ключами.</returns>
    IPrivateKeyStore CreateStore(IReadOnlyCollection<string> x509serials);
}
