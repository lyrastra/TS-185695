namespace Moedelo.Common.Consul.Abstractions;

public interface IMoedeloConsulCatalogApiClient
{
    /// <summary>
    /// Получить список идентификаторов сервисов, зарегистрированных в Consul Service Discovery
    /// </summary>
    /// <param name="cancellationToken">токен отмены операции</param>
    ValueTask<IReadOnlyCollection<string>> GetServiceIdsListAsync(CancellationToken cancellationToken);
}
