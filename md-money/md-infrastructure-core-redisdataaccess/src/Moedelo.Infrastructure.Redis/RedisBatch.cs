using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Moedelo.Infrastructure.Redis.Abstractions.Interfaces;
using Moedelo.Infrastructure.Redis.Abstractions.Models;
using StackExchange.Redis;

namespace Moedelo.Infrastructure.Redis;

public class RedisBatch : IRedisBatch
{
    private readonly IBatch batch;
    private readonly List<Task> tasks = new();
    private bool executed;

    public RedisBatch(IBatch batch)
    {
        this.batch = batch;
    }

    public Task ExecuteAsync()
    {
        if (!executed)
        {
            executed = true;
            batch.Execute();
        }
        return Task.WhenAll(tasks);
    }

    // String operations
    public RedisBatchResult<string> StringGet(string key)
    {
        var task = batch.StringGetAsync(key);
        tasks.Add(task);

        return RedisBatchResult.From(task.ToStringAsync());
    }

    public RedisBatchResult<string> StringGetDelete(string key)
    {
        var task = batch.StringGetDeleteAsync(key);
        tasks.Add(task);

        return RedisBatchResult.From(task.ToStringAsync());
    }

    public RedisBatchResult<bool> StringSet(string key, string value, TimeSpan? expiry = null, bool keepTtl = false)
    {
        var task = batch.StringSetAsync(key, value, expiry, keepTtl: keepTtl);
        tasks.Add(task);

        return RedisBatchResult.From(task);
    }

    public RedisBatchResult<long> StringIncrement(string key)
    {
        var task = batch.StringIncrementAsync(key);
        tasks.Add(task);

        return RedisBatchResult.From(task);
    }

    public RedisBatchResult<long> StringDecrement(string key)
    {
        var task = batch.StringDecrementAsync(key);
        tasks.Add(task);

        return RedisBatchResult.From(task);
    }

    // Key operations
    public RedisBatchResult<bool> KeyDelete(string key)
    {
        var task = batch.KeyDeleteAsync(key);
        tasks.Add(task);

        return RedisBatchResult.From(task);
    }

    public RedisBatchResult<bool> KeyExpire(string key, TimeSpan expiry)
    {
        var task = batch.KeyExpireAsync(key, expiry);
        tasks.Add(task);

        return RedisBatchResult.From(task);
    }

    public RedisBatchResult<bool> KeyExists(string key)
    {
        var task = batch.KeyExistsAsync(key);
        tasks.Add(task);

        return RedisBatchResult.From(task);
    }

    public RedisBatchResult<TimeSpan?> KeyTimeToLive(string key)
    {
        var task = batch.KeyTimeToLiveAsync(key);
        tasks.Add(task);

        return RedisBatchResult.From(task);
    }

    // List operations
    public RedisBatchResult<long> ListRightPush(string key, string value)
    {
        var task = batch.ListRightPushAsync(key, value);
        tasks.Add(task);

        return RedisBatchResult.From(task);
    }

    public RedisBatchResult<string> ListLeftPop(string key)
    {
        var task = batch.ListLeftPopAsync(key);
        tasks.Add(task);

        return RedisBatchResult.From(task.ToStringAsync());
    }

    public RedisBatchResult<string[]> ListRange(string key)
    {
        var task = batch.ListRangeAsync(key);
        tasks.Add(task);

        return RedisBatchResult.From(task.ToStringArrayAsync());
    }

    public RedisBatchResult<long?> ListPosition(string key, string value)
    {
        var task = batch.ListPositionAsync(key, value);
        tasks.Add(task);

        return RedisBatchResult.From(task.ToNullableLongAsync());
    }

    public RedisBatchResult<long> ListRemove(string key, string value)
    {
        var task = batch.ListRemoveAsync(key, value);
        tasks.Add(task);

        return RedisBatchResult.From(task);
    }

    public RedisBatchResult<long> ListLeftPush(string key, string value)
    {
        var task = batch.ListLeftPushAsync(key, value);
        tasks.Add(task);

        return RedisBatchResult.From(task);
    }

    public RedisBatchResult ListTrim(string key, long start, long stop)
    {
        var task = batch.ListTrimAsync(key, start, stop);
        tasks.Add(task);

        return RedisBatchResult.From(task);
    }

    // Set operations
    public RedisBatchResult<bool> SetAdd(string key, string value)
    {
        var task = batch.SetAddAsync(key, value);
        tasks.Add(task);

        return RedisBatchResult.From(task);
    }

    public RedisBatchResult<bool> SetRemove(string key, string value)
    {
        var task = batch.SetRemoveAsync(key, value);
        tasks.Add(task);

        return RedisBatchResult.From(task);
    }

    public RedisBatchResult<string[]> SetMembers(string key)
    {
        var task = batch.SetMembersAsync(key);
        tasks.Add(task);

        return RedisBatchResult.From(task.ToStringArrayAsync());
    }

    // Hash operations
    public RedisBatchResult<Dictionary<string, string>> HashGetAll(string key)
    {
        var task = batch.HashGetAllAsync(key);
        tasks.Add(task);

        return RedisBatchResult.From(task.ToDictionaryAsync());
    }

    public RedisBatchResult<string> HashGet(string key, string field)
    {
        var task = batch.HashGetAsync(key, field);
        tasks.Add(task);

        return RedisBatchResult.From(task.ToStringAsync());
    }

    public RedisBatchResult<bool> HashSet(string key, string field, string value)
    {
        var task = batch.HashSetAsync(key, field, value);
        tasks.Add(task);

        return RedisBatchResult.From(task);
    }

    public RedisBatchResult HashSet(string key, IReadOnlyCollection<KeyValuePair<string, string>> fieldValues)
    {
        var hashEntries = fieldValues.Select(kv => new HashEntry(kv.Key, kv.Value)).ToArray();
        var task = batch.HashSetAsync(key, hashEntries);
        tasks.Add(task);

        return RedisBatchResult.From(task);
    }

    public RedisBatchResult<bool> HashDelete(string key, string field)
    {
        var task = batch.HashDeleteAsync(key, field);
        tasks.Add(task);

        return RedisBatchResult.From(task);
    }

    RedisBatchResult<long> IRedisBatch.HashDelete(string key, IReadOnlyCollection<string> fields)
    {
        var redisValues = fields.Select(f => (RedisValue)f).ToArray();
        var task = batch.HashDeleteAsync(key, redisValues);
        tasks.Add(task);

        return RedisBatchResult.From(task);
    }

    // Sorted Set operations
    public RedisBatchResult<bool> SortedSetAdd(string key, double score, string member)
    {
        var task = batch.SortedSetAddAsync(key, member, score);
        tasks.Add(task);

        return RedisBatchResult.From(task);
    }

    public RedisBatchResult<string[]> SortedSetRangeByScore(string key, double minScore, double maxScore = double.PositiveInfinity)
    {
        var task = batch.SortedSetRangeByScoreAsync(key, minScore, maxScore);
        tasks.Add(task);

        return RedisBatchResult.From(task.ToStringArrayAsync());
    }

    public RedisBatchResult<long> SortedSetRemoveRangeByScore(string key, double minScore, double maxScore)
    {
        var task = batch.SortedSetRemoveRangeByScoreAsync(key, minScore, maxScore);
        tasks.Add(task);

        return RedisBatchResult.From(task);
    }

    public RedisBatchResult<long> SortedSetLength(string key)
    {
        var task = batch.SortedSetLengthAsync(key);
        tasks.Add(task);

        return RedisBatchResult.From(task);
    }

    public RedisBatchResult<long> SortedSetRemoveRangeByRank(string key, long start, long stop)
    {
        var task = batch.SortedSetRemoveRangeByScoreAsync(key, start, stop);
        tasks.Add(task);

        return RedisBatchResult.From(task);
    }

    public RedisBatchResult<bool> SortedSetRemove(string key, string member)
    {
        var task = batch.SortedSetRemoveAsync(key, member);
        tasks.Add(task);

        return RedisBatchResult.From(task);
    }

    public RedisBatchResult<double?> SortedSetScore(string key, string member)
    {
        var task = batch.SortedSetScoreAsync(key, member);
        tasks.Add(task);

        return RedisBatchResult.From(task);
    }
}
