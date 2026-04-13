using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;

namespace Moedelo.Common.Jwt.Crypto;

/// <summary>
/// Хранилище приватных ключей RSA, извлеченных из сертификатов X.509.
/// Управляет жизненным циклом сертификатов и RSA ключей, предотвращая преждевременный Dispose и ObjectDisposedException.
/// </summary>
/// <remarks>
/// Критически важно хранить сертификаты вместе с RSA ключами, так как ключи привязаны к handle'ам сертификатов.
/// При Dispose сначала освобождаются RSA ключи, затем сертификаты.
/// </remarks>
internal sealed class RsaPrivateKeyStore : IPrivateKeyStore
{
    private readonly X509Certificate2[] certificates;
    private readonly RSA[] rsaKeys;

    /// <summary>
    /// Инициализирует экземпляр <see cref="RsaPrivateKeyStore"/> с сертификатами.
    /// </summary>
    /// <param name="certificates">Коллекция сертификатов X.509, из которых будут извлечены приватные ключи RSA.</param>
    public RsaPrivateKeyStore(IEnumerable<X509Certificate2> certificates)
    {
        // Храним сертификаты, чтобы предотвратить преждевременный Dispose их ключей
        this.certificates = certificates.ToArray();
        
        // Jose.JWT ожидает RSA объекты, извлекаем их из сертификатов
        // Важно: GetRSAPrivateKey() возвращает новый RSA экземпляр, который нужно диспозить
        this.rsaKeys = this.certificates
            .Select(cert => cert.GetRSAPrivateKey())
            .ToArray();
        
        Keys = rsaKeys.Cast<object>().ToArray();
    }

    /// <summary>
    /// Освобождает ресурсы: сначала RSA ключи, затем сертификаты.
    /// Порядок критически важен для предотвращения ObjectDisposedException.
    /// </summary>
    public void Dispose()
    {
        // Сначала диспозим RSA ключи (пока сертификаты еще живы)
        foreach (var key in rsaKeys)
        {
            key?.Dispose();
        }
        
        // Затем диспозим сертификаты
        foreach (var cert in certificates)
        {
            cert?.Dispose();
        }
    }

    /// <inheritdoc />
    public IReadOnlyCollection<object> Keys { get; }
}
