using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Moedelo.InfrastructureV2.Domain.Interfaces.Redis;
using Moedelo.InfrastructureV2.Domain.Models.Redis;
using Moedelo.InfrastructureV2.Json;

namespace Moedelo.InfrastructureV2.Domain.Extensions;

public static class RedisDbExecutorExtensions
{
    public static Task<bool> SetRedisValueAsync<TValue>(
        this IRedisDbExecuter dbExecutor,
        string key,
        TValue value,
        TimeSpan? expiry = null,
        bool keepTtl = false,
        [CallerMemberName] string memberName = "",
        [CallerFilePath] string sourceFilePath = "",
        [CallerLineNumber] int sourceLineNumber = 0)
    {
        var jsonValue = new RedisValue<TValue>(value).ToJsonString();

        return dbExecutor.SetValueForKeyAsync(key, jsonValue, expiry, keepTtl,
            // ReSharper disable ExplicitCallerInfoArgument
            memberName, sourceFilePath, sourceLineNumber
            // ReSharper restore ExplicitCallerInfoArgument
        );
    }

    public static async Task<RedisValue<TValue>> GetRedisValueAsync<TValue>(
        this IRedisDbExecuter dbExecutor,
        string key,
        [CallerMemberName] string memberName = "",
        [CallerFilePath] string sourceFilePath = "",
        [CallerLineNumber] int sourceLineNumber = 0)
    {
        var result = await dbExecutor
            // ReSharper disable ExplicitCallerInfoArgument
            .GetValueByKeyAsync(key, memberName, sourceFilePath, sourceLineNumber)
            // ReSharper restore ExplicitCallerInfoArgument
            .ConfigureAwait(false);

        return string.IsNullOrEmpty(result)
            ? default
            : result.FromJsonStringOrDefault<RedisValue<TValue>>();
    }
}
