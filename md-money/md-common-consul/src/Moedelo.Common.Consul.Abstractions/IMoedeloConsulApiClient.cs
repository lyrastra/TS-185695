using Moedelo.Infrastructure.Consul.Abstraction.Models;

namespace Moedelo.Common.Consul.Abstractions;

/// <summary>
/// Клиент для работы с key-value хранилищем Consul по протоколу Http
/// </summary>
public interface IMoedeloConsulApiClient
{
    /// <summary>
    /// Получить значение ключа по http протоколу
    /// </summary>
    /// <param name="keyPath">путь до ключа</param>
    /// <param name="uriQuery">query часть uri запроса</param>
    /// <param name="cancellationToken">токен отмены операции</param>
    /// <param name="auditTrailSpanNameSuffix">(опционально) суффикс для названия спана в auditTrail</param>
    /// <returns>http ответ запроса</returns>
    ValueTask<HttpResponseMessage> GetAsync(
        string keyPath,
        string uriQuery,
        CancellationToken cancellationToken,
        string? auditTrailSpanNameSuffix = null);

    /// <summary>
    /// Сохранить значение ключа
    /// </summary>
    /// <param name="keyPath">путь до ключа</param>
    /// <param name="value">сохраняемое значение</param>
    /// <param name="cancellationToken">токен отмены операции</param>
    /// <param name="auditTrailSpanNameSuffix">(опционально) суффикс для названия спана в auditTrail</param>
    ValueTask SaveKeyValueAsync(
        string keyPath,
        string value,
        CancellationToken cancellationToken,
        string? auditTrailSpanNameSuffix = null);
    
    /// <summary>
    /// Обновить значение ключа по указанному пути.
    /// Значение будет вставлено в виде JSON
    /// </summary>
    /// <param name="keyPath">Путь до ключа</param>
    /// <param name="value">значение</param>
    /// <param name="cancellationToken">токен отмены операции</param>
    /// <param name="auditTrailSpanNameSuffix">(опционально) суффикс для названия спана в auditTrail</param>
    /// <typeparam name="TValue">тип значения</typeparam>
    ValueTask SaveKeyJsonValueAsync<TValue>(
        string keyPath,
        TValue value,
        CancellationToken cancellationToken,
        string? auditTrailSpanNameSuffix = null);

    /// <summary>
    /// Получить все ключи по указанному пути.
    /// Все значения будут десериализованы в тип TValue из JSON-представления
    /// </summary>
    /// <param name="keyPath">путь в хранилище</param>
    /// <param name="cancellationToken">токен отмены операции</param>
    /// <param name="auditTrailSpanNameSuffix">(опционально) суффикс для названия спана в auditTrail</param>
    /// <typeparam name="TValue">тип значений</typeparam>
    public ValueTask<ConsulKeyValue<TValue>[]> ListKeysAsAsync<TValue>(
        string keyPath,
        CancellationToken cancellationToken,
        string? auditTrailSpanNameSuffix = default);

    /// <summary>
    /// Удалить ключи по указанному префиксу
    /// </summary>
    /// <param name="keyPrefix">префикс, по которому будут удалены все ключи</param>
    /// <param name="cancellationToken">токен отмены операции</param>
    ValueTask DeleteKeysByPrefixAsync(string keyPrefix, CancellationToken cancellationToken);
    
    /// <summary>
    /// Удалить ключи по указанному префиксу
    /// </summary>
    /// <param name="keyPrefix">префикс, по которому будут удалены все ключи</param>
    /// <param name="cancellationToken">токен отмены операции</param>
    /// <param name="auditTrailSpanNameSuffix">суффикс для названия спана в auditTrail</param>
    ValueTask DeleteKeysByPrefixAsync(string keyPrefix, CancellationToken cancellationToken,
        string auditTrailSpanNameSuffix);
}
