using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Moedelo.Infrastructure.Consul.Abstraction;

/// <summary>
/// Клиент для работы со службой Consul по протоколу Http
/// </summary>
public interface IConsulHttpApiClient : IDisposable
{
    /// <summary>
    /// Получить значение ключа по http протоколу
    /// </summary>
    /// <param name="keyPath">путь до ключа</param>
    /// <param name="uriQuery">query часть uri запроса</param>
    /// <param name="cancellationToken">токен отмены операции</param>
    /// <returns>http ответ запроса</returns>
    Task<HttpResponseMessage> GetAsync(
        string keyPath,
        string uriQuery,
        CancellationToken cancellationToken);

    /// <summary>
    /// Сохранить значение ключа
    /// </summary>
    /// <param name="keyPath">путь до ключа</param>
    /// <param name="value">сохраняемое значение</param>
    /// <param name="cancellationToken">токен отмены операции</param>
    Task SaveKeyValueAsync(string keyPath, string value, CancellationToken cancellationToken);

    /// <summary>
    /// Удалить ключи по указанному префиксу
    /// </summary>
    /// <param name="keyPrefix">префикс, по которому будут удалены все ключи</param>
    /// <param name="cancellationToken">токен отмены операции</param>
    Task DeleteKeysByPrefixAsync(string keyPrefix, CancellationToken cancellationToken);

    IConsulAgentApiClient AgentApi { get; }
    IConsulCatalogApiClient CatalogApi { get; }

    /// <summary>
    /// Создать клиент для работы с ключами в рамках сессии Consul
    /// Внимание: созданный клиент должен быть уничтожен (DisposeAsync) после использования. Это ответственность вызывающей стороны.
    /// </summary>
    /// <param name="sessionNamingStrategy">стратегия выдачи имени сессии</param>
    /// <param name="consulSessionMonitoring"></param>
    /// <returns>новый экземпляр клиента</returns>
    IConsulSessionalKeyValueApi CreateSessionalKeyValueApiClient(IConsulSessionNamingStrategy sessionNamingStrategy,
        IConsulSessionMonitoring consulSessionMonitoring);
}