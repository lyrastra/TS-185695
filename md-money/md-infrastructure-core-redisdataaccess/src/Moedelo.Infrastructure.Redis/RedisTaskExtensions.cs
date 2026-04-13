using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using StackExchange.Redis;

namespace Moedelo.Infrastructure.Redis;

internal static class RedisTaskExtensions
{
    public static Task<string> ToStringAsync(this Task<RedisValue> task)
    {
        return task.ContinueWith(static t => (string)t.Result, TaskContinuationOptions.OnlyOnRanToCompletion);
    }

    public static Task<string[]> ToStringArrayAsync(this Task<RedisValue[]> task)
    {
        return task.ContinueWith(static t => t.Result?.ToStringArray() ?? Array.Empty<string>(), TaskContinuationOptions.OnlyOnRanToCompletion);
    }

    public static Task<Dictionary<string, string>> ToDictionaryAsync(this Task<HashEntry[]> task)
    {
        return task.ContinueWith(static t => t.Result.ToStringDictionary(), TaskContinuationOptions.OnlyOnRanToCompletion);
    }

    public static Task<long?> ToNullableLongAsync(this Task<long> task)
    {
        return task.ContinueWith(static t => t.Result >= 0 ? (long?)t.Result : null, TaskContinuationOptions.OnlyOnRanToCompletion);
    }
}

