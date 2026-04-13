namespace Moedelo.Common.Consul.Abstractions;

public interface IMoedeloConsulSessionalKeyValueApiClient
{
    /// <summary>
    /// Идентификатор сессии работы с Consul
    /// </summary>
    string ConsulSessionId { get; }

    /// <summary>
    /// Завладеть ключом в эксклюзивном режиме
    /// </summary>
    /// <param name="keyPath">путь до ключа</param>
    /// <param name="value">значение, которое будет помещено в по указанному ключу</param>
    /// <param name="cancellationToken">токен отмена операции</param>
    /// <returns>true - удалось захватить значение, false - не удалось захватить (скорее всего, значение захвачено кем-то другим)</returns>
    ValueTask<bool> AcquireKeyValueAsync(
        string keyPath,
        string value,
        CancellationToken cancellationToken);

    /// <summary>
    /// Освободить ранее захваченный ключ.
    /// Вы можете освободить ключ только, если вы его захватили. В ином случае, вызов не приводит ни к каким последствиям.
    /// </summary>
    /// <param name="keyPath">путь до ключа</param>
    /// <param name="cancellationToken">токен отмены операции</param>
    /// <returns></returns>
    ValueTask ReleaseAcquiredKeyValueAsync(
        string keyPath,
        CancellationToken cancellationToken);
}
