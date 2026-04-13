using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.Infrastructure.Redis.Abstractions.Models;

namespace Moedelo.Infrastructure.Redis.Abstractions.Interfaces;

public interface IRedisBatch
{
    Task ExecuteAsync();

    #region String operations

    RedisBatchResult<string> StringGet(string key);
    RedisBatchResult<string> StringGetDelete(string key);
    RedisBatchResult<bool> StringSet(string key, string value, TimeSpan? expiry = null, bool keepTtl = false);
    RedisBatchResult<long> StringIncrement(string key);
    RedisBatchResult<long> StringDecrement(string key);

    #endregion

    #region Key operations

    RedisBatchResult<bool> KeyDelete(string key);
    RedisBatchResult<bool> KeyExpire(string key, TimeSpan expiry);
    RedisBatchResult<bool> KeyExists(string key);
    RedisBatchResult<TimeSpan?> KeyTimeToLive(string key);

    #endregion

    #region List operations

    RedisBatchResult<long> ListRightPush(string key, string value);
    RedisBatchResult<string> ListLeftPop(string key);
    RedisBatchResult<string[]> ListRange(string key);
    RedisBatchResult<long?> ListPosition(string key, string value);
    RedisBatchResult<long> ListRemove(string key, string value);
    RedisBatchResult<long> ListLeftPush(string key, string value);
    RedisBatchResult ListTrim(string key, long start, long stop);

    #endregion

    #region Set operations

    RedisBatchResult<bool> SetAdd(string key, string value);
    RedisBatchResult<bool> SetRemove(string key, string value);
    RedisBatchResult<string[]> SetMembers(string key);

    #endregion

    #region Hash operations

    RedisBatchResult<Dictionary<string, string>> HashGetAll(string key);
    RedisBatchResult<string> HashGet(string key, string field);
    RedisBatchResult<bool> HashSet(string key, string field, string value);
    RedisBatchResult HashSet(string key, IReadOnlyCollection<KeyValuePair<string, string>> fieldValues);
    RedisBatchResult<bool> HashDelete(string key, string field);
    RedisBatchResult<long> HashDelete(string key, IReadOnlyCollection<string> fields);

    #endregion

    #region Sorted Set operations

    RedisBatchResult<bool> SortedSetAdd(string key, double score, string member);
    RedisBatchResult<string[]> SortedSetRangeByScore(string key, double minScore, double maxScore = double.PositiveInfinity);
    RedisBatchResult<long> SortedSetRemoveRangeByScore(string key, double minScore, double maxScore);
    RedisBatchResult<long> SortedSetLength(string key);
    RedisBatchResult<long> SortedSetRemoveRangeByRank(string key, long start, long stop);
    RedisBatchResult<bool> SortedSetRemove(string key, string member);
    RedisBatchResult<double?> SortedSetScore(string key, string member);

    #endregion
}
