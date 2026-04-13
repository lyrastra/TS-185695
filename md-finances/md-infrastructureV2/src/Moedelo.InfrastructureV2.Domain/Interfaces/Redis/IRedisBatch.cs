using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.InfrastructureV2.Domain.Models.Redis;

namespace Moedelo.InfrastructureV2.Domain.Interfaces.Redis;

public interface IRedisBatch
{
    Task ExecuteAsync();

    #region String operations

    IRedisBatch StringGet(string key, out RedisBatchResult<string> result);
    IRedisBatch StringGetDelete(string key, out RedisBatchResult<string> result);
    IRedisBatch StringSet(string key, string value, TimeSpan? expiry, bool keepTtl, out RedisBatchResult<bool> result);
    IRedisBatch StringIncrement(string key, out RedisBatchResult<long> result);
    IRedisBatch StringDecrement(string key, out RedisBatchResult<long> result);

    #endregion

    #region Key operations

    IRedisBatch KeyDelete(string key, out RedisBatchResult<bool> result);
    IRedisBatch KeyExpire(string key, TimeSpan expiry, out RedisBatchResult<bool> result);
    IRedisBatch KeyExists(string key, out RedisBatchResult<bool> result);
    IRedisBatch KeyTimeToLive(string key, out RedisBatchResult<TimeSpan?> result);

    #endregion

    #region List operations

    IRedisBatch ListRightPush(string key, string value, out RedisBatchResult<long> result);
    IRedisBatch ListLeftPop(string key, out RedisBatchResult<string> result);
    IRedisBatch ListRange(string key, out RedisBatchResult<string[]> result);
    IRedisBatch ListPosition(string key, string value, out RedisBatchResult<long?> result);
    IRedisBatch ListRemove(string key, string value, out RedisBatchResult<long> result);
    IRedisBatch ListLeftPush(string key, string value, out RedisBatchResult<long> result);
    IRedisBatch ListTrim(string key, long start, long stop, out RedisBatchResult result);

    #endregion

    #region Set operations

    IRedisBatch SetAdd(string key, string value, out RedisBatchResult<bool> result);
    IRedisBatch SetRemove(string key, string value, out RedisBatchResult<bool> result);
    IRedisBatch SetMembers(string key, out RedisBatchResult<string[]> result);

    #endregion

    #region Hash operations

    IRedisBatch HashGetAll(string key, out RedisBatchResult<Dictionary<string, string>> result);
    IRedisBatch HashGet(string key, string field, out RedisBatchResult<string> result);
    IRedisBatch HashSet(string key, string field, string value, out RedisBatchResult<bool> result);
    IRedisBatch HashSet(string key, IReadOnlyCollection<KeyValuePair<string, string>> fieldValues, out RedisBatchResult result);
    IRedisBatch HashDelete(string key, string field, out RedisBatchResult<bool> result);
    IRedisBatch HashDelete(string key, IReadOnlyCollection<string> fields, out RedisBatchResult<long> result);

    #endregion

    #region Sorted Set operations

    IRedisBatch SortedSetAdd(string key, double score, string member, out RedisBatchResult<bool> result);
    IRedisBatch SortedSetRangeByScore(string key, double minScore, double maxScore, out RedisBatchResult<string[]> result);
    IRedisBatch SortedSetRemoveRangeByScore(string key, double minScore, double maxScore, out RedisBatchResult<long> result);
    IRedisBatch SortedSetLength(string key, out RedisBatchResult<long> result);
    IRedisBatch SortedSetRemoveRangeByRank(string key, long start, long stop, out RedisBatchResult<long> result);
    IRedisBatch SortedSetRemove(string key, string member, out RedisBatchResult<bool> result);
    IRedisBatch SortedSetScore(string key, string member, out RedisBatchResult<double?> result);

    #endregion
}
