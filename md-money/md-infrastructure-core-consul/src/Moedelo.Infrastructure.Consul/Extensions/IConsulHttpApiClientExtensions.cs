using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Moedelo.Infrastructure.Consul.Abstraction;
using Moedelo.Infrastructure.Consul.Abstraction.Models;
using Moedelo.Infrastructure.Json;

namespace Moedelo.Infrastructure.Consul.Extensions;

/// <summary>
/// Extension methods для IConsulHttpApiClient, реализующие IConsulNonSessionalKeyValueApi
/// </summary>
public static class ConsulHttpApiClientExtensions
{
    /// <summary>
    /// Получить все ключи по указанному пути.
    /// Все значения будут десериализованы в тип TValue из JSON-представления
    /// </summary>
    /// <param name="client">HTTP-клиент Consul</param>
    /// <param name="path">путь в хранилище</param>
    /// <param name="cancellationToken">токен отмены операции</param>
    /// <typeparam name="TValue">тип значений</typeparam>
    public static async Task<ConsulKeyValue<TValue>[]> ListKeysAsAsync<TValue>(
        this IConsulHttpApiClient client,
        string path,
        CancellationToken cancellationToken)
    {
        using var response = await client.GetAsync(path, "recurse=true", cancellationToken);

        if (!response.IsSuccessStatusCode)
        {
            return [];
        }

        var keyValues = await response.DeserializeJsonContentAsync<KeyValue[]>(cancellationToken: cancellationToken);
        
        return keyValues?
            .Select(kv => new ConsulKeyValue<TValue>(
                kv.Key, 
                string.IsNullOrEmpty(kv.Value) ? default : kv.Value.GetUtf8StringFromBase64String().FromJsonString<TValue>()))
            .ToArray() ?? [];
    }

    // Внутренняя структура для десериализации ответа Consul
    private struct KeyValue
    {
        public string Key { get; set; }
        public string Value { get; set; }
    }
} 