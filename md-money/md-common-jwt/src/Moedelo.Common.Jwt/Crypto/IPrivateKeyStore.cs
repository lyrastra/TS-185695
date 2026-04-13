using System;
using System.Collections.Generic;

namespace Moedelo.Common.Jwt.Crypto;

/// <summary>
/// Хранилище приватных ключей RSA, извлеченных из сертификатов X.509.
/// Управляет жизненным циклом сертификатов и связанных с ними RSA ключей.
/// </summary>
public interface IPrivateKeyStore : IDisposable
{
    /// <summary>
    /// Коллекция приватных ключей RSA в виде объектов.
    /// Ключи остаются валидными на протяжении всего времени жизни хранилища.
    /// </summary>
    IReadOnlyCollection<object> Keys { get; }
}