using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.Redis;
using Moedelo.InfrastructureV2.Domain.Models.Redis;
using IRawRedisBatch = Moedelo.Infrastructure.Redis.Abstractions.Interfaces.IRedisBatch;

namespace Moedelo.InfrastructureV2.RedisDataAccess.Common;

public sealed class MoedeloRedisBatch : IRedisBatch
{
    private readonly IRawRedisBatch redisBatch;
    private readonly IAuditSpanBuilder spanBuilder;
    private readonly Func<string, string> mutateKey;
    private readonly List<string> commandDescriptionList = [];

    public MoedeloRedisBatch(IRawRedisBatch redisBatch,
        IAuditSpanBuilder spanBuilder,
        Func<string, string> mutateKey)
    {
        this.redisBatch = redisBatch;
        this.spanBuilder = spanBuilder;
        this.mutateKey = mutateKey;
    }

    public async Task ExecuteAsync()
    {
        using var auditScope = spanBuilder
            .WithTag("Commands.Count", commandDescriptionList.Count)
            .WithTag("Commands", commandDescriptionList)
            .Start();
        try
        {
            await redisBatch.ExecuteAsync().ConfigureAwait(false);
        }
        catch (Exception exception)
        {
            auditScope.Span.SetError(exception);
        }
    }

    public IRedisBatch StringGet(string key, out RedisBatchResult<string> result)
    {
        commandDescriptionList.Add($"StringGet {key}");
        var rawResult = redisBatch.StringGet(mutateKey(key));
        result = RedisBatchResult.From(rawResult.Task);
        return this;
    }

    public IRedisBatch StringGetDelete(string key, out RedisBatchResult<string> result)
    {
        commandDescriptionList.Add($"StringGetDelete {key}");
        var rawResult = redisBatch.StringGetDelete(mutateKey(key));
        result = RedisBatchResult.From(rawResult.Task);
        return this;
    }

    public IRedisBatch StringSet(string key, string value, TimeSpan? expiry, bool keepTtl, out RedisBatchResult<bool> result)
    {
        var expiryDesc = expiry.HasValue ? $" expiry={expiry.Value.TotalSeconds}s" : keepTtl ? " keepTtl" : "";
        commandDescriptionList.Add($"StringSet {key}{expiryDesc}");
        var rawResult = redisBatch.StringSet(mutateKey(key), value, expiry, keepTtl);
        result = RedisBatchResult.From(rawResult.Task);
        return this;
    }

    public IRedisBatch StringIncrement(string key, out RedisBatchResult<long> result)
    {
        commandDescriptionList.Add($"StringIncrement {key}");
        var rawResult = redisBatch.StringIncrement(mutateKey(key));
        result = RedisBatchResult.From(rawResult.Task);
        return this;
    }

    public IRedisBatch StringDecrement(string key, out RedisBatchResult<long> result)
    {
        commandDescriptionList.Add($"StringDecrement {key}");
        var rawResult = redisBatch.StringDecrement(mutateKey(key));
        result = RedisBatchResult.From(rawResult.Task);
        return this;
    }

    // Key operations
    public IRedisBatch KeyDelete(string key, out RedisBatchResult<bool> result)
    {
        commandDescriptionList.Add($"KeyDelete {key}");
        var rawResult = redisBatch.KeyDelete(mutateKey(key));
        result = RedisBatchResult.From(rawResult.Task);
        return this;
    }

    public IRedisBatch KeyExpire(string key, TimeSpan expiry, out RedisBatchResult<bool> result)
    {
        commandDescriptionList.Add($"KeyExpire {key} expiry={expiry.TotalSeconds}s");
        var rawResult = redisBatch.KeyExpire(mutateKey(key), expiry);
        result = RedisBatchResult.From(rawResult.Task);
        return this;
    }

    public IRedisBatch KeyExists(string key, out RedisBatchResult<bool> result)
    {
        commandDescriptionList.Add($"KeyExists {key}");
        var rawResult = redisBatch.KeyExists(mutateKey(key));
        result = RedisBatchResult.From(rawResult.Task);
        return this;
    }

    public IRedisBatch KeyTimeToLive(string key, out RedisBatchResult<TimeSpan?> result)
    {
        commandDescriptionList.Add($"KeyTimeToLive {key}");
        var rawResult = redisBatch.KeyTimeToLive(mutateKey(key));
        result = RedisBatchResult.From(rawResult.Task);
        return this;
    }

    // List operations
    public IRedisBatch ListRightPush(string key, string value, out RedisBatchResult<long> result)
    {
        commandDescriptionList.Add($"ListRightPush {key}");
        var rawResult = redisBatch.ListRightPush(mutateKey(key), value);
        result = RedisBatchResult.From(rawResult.Task);
        return this;
    }

    public IRedisBatch ListLeftPop(string key, out RedisBatchResult<string> result)
    {
        commandDescriptionList.Add($"ListLeftPop {key}");
        var rawResult = redisBatch.ListLeftPop(mutateKey(key));
        result = RedisBatchResult.From(rawResult.Task);
        return this;
    }

    public IRedisBatch ListRange(string key, out RedisBatchResult<string[]> result)
    {
        commandDescriptionList.Add($"ListRange {key}");
        var rawResult = redisBatch.ListRange(mutateKey(key));
        result = RedisBatchResult.From(rawResult.Task);
        return this;
    }

    public IRedisBatch ListPosition(string key, string value, out RedisBatchResult<long?> result)
    {
        commandDescriptionList.Add($"ListPosition {key}");
        var rawResult = redisBatch.ListPosition(mutateKey(key), value);
        result = RedisBatchResult.From(rawResult.Task);
        return this;
    }

    public IRedisBatch ListRemove(string key, string value, out RedisBatchResult<long> result)
    {
        commandDescriptionList.Add($"ListRemove {key}");
        var rawResult = redisBatch.ListRemove(mutateKey(key), value);
        result = RedisBatchResult.From(rawResult.Task);
        return this;
    }

    public IRedisBatch ListLeftPush(string key, string value, out RedisBatchResult<long> result)
    {
        commandDescriptionList.Add($"ListLeftPush {key}");
        var rawResult = redisBatch.ListLeftPush(mutateKey(key), value);
        result = RedisBatchResult.From(rawResult.Task);
        return this;
    }

    public IRedisBatch ListTrim(string key, long start, long stop, out RedisBatchResult result)
    {
        commandDescriptionList.Add($"ListTrim {key} start={start} stop={stop}");
        var rawResult = redisBatch.ListTrim(mutateKey(key), start, stop);
        result = RedisBatchResult.From(rawResult.Task);
        return this;
    }

    // Set operations
    public IRedisBatch SetAdd(string key, string value, out RedisBatchResult<bool> result)
    {
        commandDescriptionList.Add($"SetAdd {key}");
        var rawResult = redisBatch.SetAdd(mutateKey(key), value);
        result = RedisBatchResult.From(rawResult.Task);
        return this;
    }

    public IRedisBatch SetRemove(string key, string value, out RedisBatchResult<bool> result)
    {
        commandDescriptionList.Add($"SetRemove {key}");
        var rawResult = redisBatch.SetRemove(mutateKey(key), value);
        result = RedisBatchResult.From(rawResult.Task);
        return this;
    }

    public IRedisBatch SetMembers(string key, out RedisBatchResult<string[]> result)
    {
        commandDescriptionList.Add($"SetMembers {key}");
        var rawResult = redisBatch.SetMembers(mutateKey(key));
        result = RedisBatchResult.From(rawResult.Task);
        return this;
    }

    // Hash operations
    public IRedisBatch HashGetAll(string key, out RedisBatchResult<Dictionary<string, string>> result)
    {
        commandDescriptionList.Add($"HashGetAll {key}");
        var rawResult = redisBatch.HashGetAll(mutateKey(key));
        result = RedisBatchResult.From(rawResult.Task);
        return this;
    }

    public IRedisBatch HashGet(string key, string field, out RedisBatchResult<string> result)
    {
        commandDescriptionList.Add($"HashGet {key} field={field}");
        var rawResult = redisBatch.HashGet(mutateKey(key), field);
        result = RedisBatchResult.From(rawResult.Task);
        return this;
    }

    public IRedisBatch HashSet(string key, string field, string value, out RedisBatchResult<bool> result)
    {
        commandDescriptionList.Add($"HashSet {key} field={field}");
        var rawResult = redisBatch.HashSet(mutateKey(key), field, value);
        result = RedisBatchResult.From(rawResult.Task);
        return this;
    }

    public IRedisBatch HashSet(string key, IReadOnlyCollection<KeyValuePair<string, string>> fieldValues, out RedisBatchResult result)
    {
        commandDescriptionList.Add($"HashSet {key} fields={fieldValues.Count}");
        var rawResult = redisBatch.HashSet(mutateKey(key), fieldValues);
        result = RedisBatchResult.From(rawResult.Task);
        return this;
    }

    public IRedisBatch HashDelete(string key, string field, out RedisBatchResult<bool> result)
    {
        commandDescriptionList.Add($"HashDelete {key} field={field}");
        var rawResult = redisBatch.HashDelete(mutateKey(key), field);
        result = RedisBatchResult.From(rawResult.Task);
        return this;
    }

    public IRedisBatch HashDelete(string key, IReadOnlyCollection<string> fields, out RedisBatchResult<long> result)
    {
        commandDescriptionList.Add($"HashDelete {key} fields={fields.Count}");
        var rawResult = redisBatch.HashDelete(mutateKey(key), fields);
        result = RedisBatchResult.From(rawResult.Task);
        return this;
    }

    // Sorted Set operations
    public IRedisBatch SortedSetAdd(string key, double score, string member, out RedisBatchResult<bool> result)
    {
        commandDescriptionList.Add($"SortedSetAdd {key} score={score}");
        var rawResult = redisBatch.SortedSetAdd(mutateKey(key), score, member);
        result = RedisBatchResult.From(rawResult.Task);
        return this;
    }

    public IRedisBatch SortedSetRangeByScore(string key, double minScore, double maxScore, out RedisBatchResult<string[]> result)
    {
        commandDescriptionList.Add($"SortedSetRangeByScore {key} min={minScore} max={maxScore}");
        var rawResult = redisBatch.SortedSetRangeByScore(mutateKey(key), minScore, maxScore);
        result = RedisBatchResult.From(rawResult.Task);
        return this;
    }

    public IRedisBatch SortedSetRemoveRangeByScore(string key, double minScore, double maxScore, out RedisBatchResult<long> result)
    {
        commandDescriptionList.Add($"SortedSetRemoveRangeByScore {key} min={minScore} max={maxScore}");
        var rawResult = redisBatch.SortedSetRemoveRangeByScore(mutateKey(key), minScore, maxScore);
        result = RedisBatchResult.From(rawResult.Task);
        return this;
    }

    public IRedisBatch SortedSetLength(string key, out RedisBatchResult<long> result)
    {
        commandDescriptionList.Add($"SortedSetLength {key}");
        var rawResult = redisBatch.SortedSetLength(mutateKey(key));
        result = RedisBatchResult.From(rawResult.Task);
        return this;
    }

    public IRedisBatch SortedSetRemoveRangeByRank(string key, long start, long stop, out RedisBatchResult<long> result)
    {
        commandDescriptionList.Add($"SortedSetRemoveRangeByRank {key} start={start} stop={stop}");
        var rawResult = redisBatch.SortedSetRemoveRangeByRank(mutateKey(key), start, stop);
        result = RedisBatchResult.From(rawResult.Task);
        return this;
    }

    public IRedisBatch SortedSetRemove(string key, string member, out RedisBatchResult<bool> result)
    {
        commandDescriptionList.Add($"SortedSetRemove {key}");
        var rawResult = redisBatch.SortedSetRemove(mutateKey(key), member);
        result = RedisBatchResult.From(rawResult.Task);
        return this;
    }

    public IRedisBatch SortedSetScore(string key, string member, out RedisBatchResult<double?> result)
    {
        commandDescriptionList.Add($"SortedSetScore {key}");
        var rawResult = redisBatch.SortedSetScore(mutateKey(key), member);
        result = RedisBatchResult.From(rawResult.Task);
        return this;
    }
}
