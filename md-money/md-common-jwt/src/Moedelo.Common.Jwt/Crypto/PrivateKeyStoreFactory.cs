using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;

namespace Moedelo.Common.Jwt.Crypto;

/// <summary>
/// Фабрика для создания хранилищ приватных ключей (<see cref="RsaPrivateKeyStore"/>).
/// Загружает сертификаты X.509 из хранилища Windows (LocalMachine\My) по серийным номерам.
/// </summary>
[InjectAsSingleton(typeof(IPrivateKeyStoreFactory))]
internal sealed class PrivateKeyStoreFactory : IPrivateKeyStoreFactory
{
    /// <inheritdoc />
    public IPrivateKeyStore CreateStore(IReadOnlyCollection<string> x509serials)
    {
        using var x509store = new X509Store(StoreName.My, StoreLocation.LocalMachine);
        x509store.Open(OpenFlags.ReadOnly);

        var serialsHashSet = new HashSet<string>(x509serials, StringComparer.OrdinalIgnoreCase);

        // ВАЖНО: храним сертификаты, а не RSA ключи напрямую
        // Это предотвращает ObjectDisposedException когда GC собирает сертификаты
        var certificates = x509store.Certificates
            .Cast<X509Certificate2>()
            .Where(certificate => certificate?.SerialNumber != null)
            .Where(certificate => serialsHashSet.Contains(certificate.SerialNumber))
            .ToArray();

        return new RsaPrivateKeyStore(certificates);
    }
}
