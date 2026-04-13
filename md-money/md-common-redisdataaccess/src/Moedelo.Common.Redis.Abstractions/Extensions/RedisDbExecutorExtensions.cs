using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Moedelo.Infrastructure.Json;
using Moedelo.Infrastructure.Redis.Abstractions.Interfaces;
using Moedelo.Infrastructure.Redis.Abstractions.Models;

namespace Moedelo.Common.Redis.Abstractions.Extensions;

internal static class RedisDbExecutorExtensions
{
    /// <summary>
    /// <a href="https://redis.io/commands/getdel/">GETDEL</a>
    /// Получить значение, хранящееся по указанному ключу, и удалить его
    /// Значение должно быть строковым 
    /// </summary>
    /// <param name="executor"></param>
    /// <param name="connection">Соединение</param>
    /// <param name="key">ключ</param>
    /// <returns>строковое значение или null (если значение по ключу отсутствовало)</returns>
    internal static async Task<TResult> GetDeleteAsync<TResult>(
        this IRedisDbExecuter executor,
        IRedisConnection connection,
        string key)  where TResult : class
    {
        var stringValue = await executor.GetDeleteAsync(connection, key)
            .ConfigureAwait(false);

        return stringValue?.FromJsonString<TResult>();
    }

    /// <summary>
    /// получить все значения из набора уникальных значений
    /// </summary>
    /// <param name="executor"></param>
    /// <param name="connection"></param>
    /// <param name="setKey"></param>
    /// <param name="cancellationToken"></param>
    /// <typeparam name="T"></typeparam>
    internal static async Task<HashSet<T>> GetSetAllAsync<T>(
        this IRedisDbExecuter executor,
        IRedisConnection connection,
        string setKey)
    {
        var values = await executor.GetSetAllAsync(connection, setKey);

        return new HashSet<T>(values
            .Where(value => !string.IsNullOrEmpty(value))
            .Select(value => value.FromJsonString<T>()));
    }
}
