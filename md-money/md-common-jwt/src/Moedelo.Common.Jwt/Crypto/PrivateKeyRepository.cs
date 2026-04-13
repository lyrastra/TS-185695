using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using Moedelo.Common.Jwt.Extensions;
using Moedelo.Common.Settings.Abstractions;
using Moedelo.Common.Settings.Abstractions.Models;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;

namespace Moedelo.Common.Jwt.Crypto;

/// <summary>
/// Репозиторий для управления приватными ключами RSA из сертификатов X.509.
/// Кеширует хранилища ключей (<see cref="IPrivateKeyStore"/>) по серийным номерам сертификатов.
/// Singleton на весь lifetime приложения, освобождает ресурсы при Dispose (shutdown приложения).
/// </summary>
[InjectAsSingleton(typeof(IPrivateKeyRepository))]
internal sealed class PrivateKeyRepository : IPrivateKeyRepository, IDisposable
{
    private readonly SettingValue certificateSerialSetting;
    private readonly ConcurrentDictionary<string, IPrivateKeyStore> keyStores = new();
    private readonly Func<string, IPrivateKeyStore> storeFactory;

    /// <summary>
    /// Инициализирует экземпляр <see cref="PrivateKeyRepository"/>.
    /// </summary>
    /// <param name="settingRepository">Репозиторий настроек для получения серийных номеров сертификатов (IdX509SerialNumber).</param>
    /// <param name="keyStoreFactory">Фабрика для создания хранилищ ключей.</param>
    public PrivateKeyRepository(
        ISettingRepository settingRepository,
        IPrivateKeyStoreFactory keyStoreFactory)
    {
        this.storeFactory = serials => keyStoreFactory.CreateStore(serials.Split(','));
        certificateSerialSetting = settingRepository.GetCertificateSerial()
            .ThrowExceptionIfNull(true);
    }

    /// <summary>
    /// Освобождает все кешированные хранилища ключей и связанные с ними ресурсы (сертификаты, RSA ключи).
    /// Вызывается при shutdown приложения.
    /// </summary>
    public void Dispose()
    {
        var stores = keyStores.Values.ToArray();
        keyStores.Clear();

        foreach (var store in stores)
        {
            store.Dispose();
        }
    }

    /// <summary>
    /// Получает коллекцию приватных ключей из кеша или создает новое хранилище при изменении конфигурации.
    /// </summary>
    private IReadOnlyCollection<object> GetPrivateKeys()
    {
        var keyStore = keyStores.GetOrAdd(certificateSerialSetting.Value, storeFactory);

        return keyStore.Keys;
    }

    /// <inheritdoc />
    public object PrivateKey => GetPrivateKeys().FirstOrDefault();
}
