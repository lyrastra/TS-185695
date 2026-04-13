using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Moedelo.Infrastructure.Consul.Abstraction;

/// <summary>
/// API доступа к <a href="https://developer.hashicorp.com/consul/api-docs/catalog">Catalog Agent API</a>
/// </summary>
public interface IConsulCatalogApiClient
{
    /// <summary>
    /// Получить список идентификаторов сервисов, зарегистрированных в Consul Service Discovery
    /// </summary>
    /// <param name="cancellationToken">токен отмены операции</param>
    Task<IReadOnlyCollection<string>> GetServiceIdsListAsync(CancellationToken cancellationToken);
}
