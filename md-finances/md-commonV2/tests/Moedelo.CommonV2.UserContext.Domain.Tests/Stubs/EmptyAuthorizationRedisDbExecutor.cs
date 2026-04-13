using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.Interfaces.Redis;
using Moedelo.InfrastructureV2.Domain.Models.Redis;
using Moq;
// ReSharper disable ExplicitCallerInfoArgument

namespace Moedelo.CommonV2.UserContext.Domain.Tests.Stubs;

[InjectAsSingleton(typeof(IAuthorizationRedisDbExecutor))]
internal sealed class EmptyAuthorizationRedisDbExecutor : IAuthorizationRedisDbExecutor
{
    private IAuthorizationRedisDbExecutor impl = Mock.Of<IAuthorizationRedisDbExecutor>();
    public bool IsAvailable(string memberName = "", string sourceFilePath = "", int sourceLineNumber = 0)
    {
        return impl.IsAvailable(memberName, sourceFilePath, sourceLineNumber);
    }

    public Task<TimeSpan?> KeyTimeToLiveAsync(string key, string memberName = "", string sourceFilePath = "", int sourceLineNumber = 0)
    {
        throw new NotImplementedException();
    }

    public Task<bool> SetValueForKeyAsync(string key, string value, TimeSpan? expiry = null, bool keepTtl = false, string memberName = "",
        string sourceFilePath = "", int sourceLineNumber = 0)
    {
        return impl.SetValueForKeyAsync(key, value, expiry, keepTtl, memberName, sourceFilePath, sourceLineNumber);
    }

    public Task<bool> SetValueForKeyAsync<T>(string key, T value, TimeSpan? expiry = null, bool keepTtl = false, string memberName = "",
        string sourceFilePath = "", int sourceLineNumber = 0) where T : class
    {
        return impl.SetValueForKeyAsync(key, value, expiry, keepTtl, memberName, sourceFilePath, sourceLineNumber);
    }

    public Task<long> PushValueToListForKeyAsync(string key, string value, string memberName = "", string sourceFilePath = "",
        int sourceLineNumber = 0)
    {
        return impl.PushValueToListForKeyAsync(key, value, memberName, sourceFilePath, sourceLineNumber);
    }

    public Task<string> PopValueFromListForKeyAsync(string key, string memberName = "", string sourceFilePath = "",
        int sourceLineNumber = 0)
    {
        return impl.PopValueFromListForKeyAsync(key, memberName, sourceFilePath, sourceLineNumber);
    }

    public Task<long> SetValueListForKeyAsync(string key, IReadOnlyCollection<string> valueCollection, string memberName = "",
        string sourceFilePath = "", int sourceLineNumber = 0)
    {
        return impl.SetValueListForKeyAsync(key, valueCollection, memberName, sourceFilePath, sourceLineNumber);
    }

    public Task<string> GetValueByKeyAsync(string key, string memberName = "", string sourceFilePath = "", int sourceLineNumber = 0)
    {
        return impl.GetValueByKeyAsync(key, memberName, sourceFilePath, sourceLineNumber);
    }

    public Task<long?> GetValueIndexInListAsync(string key, string value, string memberName = "", string sourceFilePath = "",
        int sourceLineNumber = 0)
    {
        return impl.GetValueIndexInListAsync(key, value, memberName, sourceFilePath, sourceLineNumber);
    }

    public Task<TR> GetValueByKeyAsync<TR>(string key, string memberName = "", string sourceFilePath = "", int sourceLineNumber = 0) where TR : class
    {
        return impl.GetValueByKeyAsync<TR>(key, memberName, sourceFilePath, sourceLineNumber);
    }

    public Task<List<string>> GetValueListByKeyAsync(string key, string memberName = "", string sourceFilePath = "", int sourceLineNumber = 0)
    {
        return impl.GetValueListByKeyAsync(key, memberName, sourceFilePath, sourceLineNumber);
    }

    public List<string> GetKeyListByMatch(string match, int count = 10, string memberName = "", string sourceFilePath = "",
        int sourceLineNumber = 0)
    {
        return impl.GetKeyListByMatch(match, count, memberName, sourceFilePath, sourceLineNumber);
    }

    public Task<string> GetAndDeleteAsync(string key, string memberName = "", string sourceFilePath = "", int sourceLineNumber = 0)
    {
        return impl.GetAndDeleteAsync(key, memberName, sourceFilePath, sourceLineNumber);
    }

    public Task<bool> DeleteKeyAsync(string key, string memberName = "", string sourceFilePath = "", int sourceLineNumber = 0)
    {
        return impl.DeleteKeyAsync(key, memberName, sourceFilePath, sourceLineNumber);
    }

    public Task<long> DeleteKeysAsync(IReadOnlyCollection<string> keyCollection, string memberName = "", string sourceFilePath = "",
        int sourceLineNumber = 0)
    {
        return impl.DeleteKeysAsync(keyCollection, memberName, sourceFilePath, sourceLineNumber);
    }

    public Task DeleteKeysMatchedAsync(string match, string memberName = "", string sourceFilePath = "", int sourceLineNumber = 0)
    {
        return impl.DeleteKeysMatchedAsync(match, memberName, sourceFilePath, sourceLineNumber);
    }

    public Task<List<string>> GetValueListByKeyListAsync(IReadOnlyCollection<string> keyCollection, string memberName = "", string sourceFilePath = "",
        int sourceLineNumber = 0)
    {
        return impl.GetValueListByKeyListAsync(keyCollection, memberName, sourceFilePath, sourceLineNumber);
    }

    public Task<List<TR>> GetValueListByKeyListAsync<TR>(IReadOnlyCollection<string> keyCollection, string memberName = "",
        string sourceFilePath = "", int sourceLineNumber = 0) where TR : class
    {
        return impl.GetValueListByKeyListAsync<TR>(keyCollection, memberName, sourceFilePath, sourceLineNumber);
    }

    public Task<long> IncrNumberValueForKeyAsync(string key, string memberName = "", string sourceFilePath = "",
        int sourceLineNumber = 0)
    {
        return impl.IncrNumberValueForKeyAsync(key, memberName, sourceFilePath, sourceLineNumber);
    }

    public Task<long> DecrNumberValueForKeyAsync(string key, string memberName = "", string sourceFilePath = "",
        int sourceLineNumber = 0)
    {
        return impl.DecrNumberValueForKeyAsync(key, memberName, sourceFilePath, sourceLineNumber);
    }

    public Task<bool> ExpireForKeyAsync(string key, TimeSpan expire, string memberName = "", string sourceFilePath = "",
        int sourceLineNumber = 0)
    {
        return impl.ExpireForKeyAsync(key, expire, memberName, sourceFilePath, sourceLineNumber);
    }

    public Task<bool> LpushAndLtrimInListForKeyAsync(string key, string value, int trimSize, string memberName = "",
        string sourceFilePath = "", int sourceLineNumber = 0)
    {
        return impl.LpushAndLtrimInListForKeyAsync(key, value, trimSize, memberName, sourceFilePath, sourceLineNumber);
    }

    public Task<bool> DeleteValueInListForKeyAsync(string key, string value, string memberName = "", string sourceFilePath = "",
        int sourceLineNumber = 0)
    {
        return impl.DeleteValueInListForKeyAsync(key, value, memberName, sourceFilePath, sourceLineNumber);
    }

    public Task<Dictionary<string, string>> GetDictionaryByKeyAsync(string key, string memberName = "", string sourceFilePath = "", int sourceLineNumber = 0)
    {
        return impl.GetDictionaryByKeyAsync(key, memberName, sourceFilePath, sourceLineNumber);
    }

    public Task<Dictionary<string, T>> GetDictionaryByKeyAsync<T>(string key, string memberName = "", string sourceFilePath = "",
        int sourceLineNumber = 0) where T : class
    {
        return impl.GetDictionaryByKeyAsync<T>(key, memberName, sourceFilePath, sourceLineNumber);
    }

    public Task SetDictionaryForKeyAsync(string key, IReadOnlyCollection<KeyValuePair<string, string>> dictionary, TimeSpan? expiry = null,
        string memberName = "", string sourceFilePath = "", int sourceLineNumber = 0)
    {
        return impl.SetDictionaryForKeyAsync(key, dictionary, expiry, memberName, sourceFilePath, sourceLineNumber);
    }

    public Task SetDictionaryForKeyAsync<T>(string key, IReadOnlyCollection<KeyValuePair<string, T>> dictionary, TimeSpan? expiry = null,
        string memberName = "", string sourceFilePath = "", int sourceLineNumber = 0) where T : class
    {
        return impl.SetDictionaryForKeyAsync(key, dictionary, expiry, memberName, sourceFilePath, sourceLineNumber);
    }

    public Task<string> GetFieldValueFromDictionaryByKeyAsync(string key, string field, string memberName = "", string sourceFilePath = "",
        int sourceLineNumber = 0)
    {
        return impl.GetFieldValueFromDictionaryByKeyAsync(key, field, memberName, sourceFilePath, sourceLineNumber);
    }

    public Task<T> GetFieldValueFromDictionaryByKeyAsync<T>(string key, string field, string memberName = "",
        string sourceFilePath = "", int sourceLineNumber = 0) where T : class
    {
        return impl.GetFieldValueFromDictionaryByKeyAsync<T>(key, field, memberName, sourceFilePath, sourceLineNumber);
    }

    public Task SetFieldValueToDictionaryForKeyAsync(string key, KeyValuePair<string, string> fieldValuePair, string memberName = "",
        string sourceFilePath = "", int sourceLineNumber = 0)
    {
        return impl.SetFieldValueToDictionaryForKeyAsync(key, fieldValuePair, memberName, sourceFilePath, sourceLineNumber);
    }

    public Task SetFieldValueToDictionaryForKeyAsync<T>(string key, KeyValuePair<string, T> fieldValuePair, string memberName = "",
        string sourceFilePath = "", int sourceLineNumber = 0) where T : class
    {
        return impl.SetFieldValueToDictionaryForKeyAsync(key, fieldValuePair, memberName, sourceFilePath, sourceLineNumber);
    }

    public Task SetFieldsValuesToDictionaryForKeyAsync(string key, IReadOnlyCollection<KeyValuePair<string, string>> fieldValuePairCollection,
        string memberName = "", string sourceFilePath = "", int sourceLineNumber = 0)
    {
        return impl.SetFieldsValuesToDictionaryForKeyAsync(key, fieldValuePairCollection, memberName, sourceFilePath, sourceLineNumber);
    }

    public Task SetFieldsValuesToDictionaryForKeyAsync<T>(string key, IReadOnlyCollection<KeyValuePair<string, T>> fieldValuePairCollection,
        string memberName = "", string sourceFilePath = "", int sourceLineNumber = 0) where T : class
    {
        return impl.SetFieldsValuesToDictionaryForKeyAsync(key, fieldValuePairCollection, memberName, sourceFilePath, sourceLineNumber);
    }

    public Task DeleteFieldInDictionaryForKey(string key, string field, string memberName = "", string sourceFilePath = "",
        int sourceLineNumber = 0)
    {
        return impl.DeleteFieldInDictionaryForKey(key, field, memberName, sourceFilePath, sourceLineNumber);
    }

    public Task DeleteFieldsInDictionaryForKey(string key, IReadOnlyCollection<string> fieldCollection, string memberName = "",
        string sourceFilePath = "", int sourceLineNumber = 0)
    {
        return impl.DeleteFieldsInDictionaryForKey(key, fieldCollection, memberName, sourceFilePath, sourceLineNumber);
    }

    public Task AddValueToSetAsync<T>(string key, T value, string memberName = "", string sourceFilePath = "",
        int sourceLineNumber = 0)
    {
        return impl.AddValueToSetAsync(key, value, memberName, sourceFilePath, sourceLineNumber);
    }

    public Task AddValueToSetAsync(string key, string value, string memberName = "", string sourceFilePath = "",
        int sourceLineNumber = 0)
    {
        throw new NotImplementedException();
    }

    public Task DeleteValueFromSetAsync<T>(string key, T value, string memberName = "", string sourceFilePath = "",
        int sourceLineNumber = 0)
    {
        return impl.DeleteValueFromSetAsync(key, value, memberName, sourceFilePath, sourceLineNumber);
    }

    public Task DeleteValueFromSetAsync(string key, string value, string memberName = "", string sourceFilePath = "",
        int sourceLineNumber = 0)
    {
        throw new NotImplementedException();
    }

    public Task<HashSet<string>> GetAllValuesOfSetAsync(string key, string memberName = "", string sourceFilePath = "", int sourceLineNumber = 0)
    {
        return impl.GetAllValuesOfSetAsync(key, memberName, sourceFilePath, sourceLineNumber);
    }

    public Task<HashSet<T>> GetAllValuesOfSetAsync<T>(string key, string memberName = "", string sourceFilePath = "",
        int sourceLineNumber = 0)
    {
        return impl.GetAllValuesOfSetAsync<T>(key, memberName, sourceFilePath, sourceLineNumber);
    }

    public Task<bool> DistributedLockRunAsync(string keyLock, Action method, RedisQueryObject queryObject, string memberName = "",
        string sourceFilePath = "", int sourceLineNumber = 0)
    {
        return impl.DistributedLockRunAsync(keyLock, method, queryObject, memberName, sourceFilePath, sourceLineNumber);
    }

    public Task<bool> DistributedLockRunAsync(string keyLock, Func<Task> method, RedisQueryObject queryObject = null, string memberName = "",
        string sourceFilePath = "", int sourceLineNumber = 0)
    {
        return impl.DistributedLockRunAsync(keyLock, method, queryObject, memberName, sourceFilePath, sourceLineNumber);
    }

    public Task<bool> DistributedLockRunAsync(string keyLock, Func<CancellationToken, Task> method, RedisQueryObject queryObject, string memberName = "",
        string sourceFilePath = "", int sourceLineNumber = 0)
    {
        return impl.DistributedLockRunAsync(keyLock, method, queryObject, memberName, sourceFilePath, sourceLineNumber);
    }

    public Task<bool> DistributedReadLockAsync(string keyLock, RedisQueryObject queryObject = null, string memberName = "",
        string sourceFilePath = "", int sourceLineNumber = 0)
    {
        return impl.DistributedReadLockAsync(keyLock, queryObject, memberName, sourceFilePath, sourceLineNumber);
    }

    public Task<T> DistributedLockRunAsync<T>(string keyLock, Func<Task<T>> method, RedisQueryObject queryObject = null,
        string memberName = "", string sourceFilePath = "", int sourceLineNumber = 0)
    {
        return impl.DistributedLockRunAsync(keyLock, method, queryObject, memberName, sourceFilePath, sourceLineNumber);
    }

    public Task<T> DistributedLockRunAsync<T>(string keyLock, Func<CancellationToken, Task<T>> method, RedisQueryObject queryObject,
        CancellationToken cancellationToken, string memberName = "", string sourceFilePath = "", int sourceLineNumber = 0)
    {
        return impl.DistributedLockRunAsync(keyLock, method, queryObject, cancellationToken, memberName, sourceFilePath, sourceLineNumber);
    }

    public Task<bool> ExistsAsync(string key, string memberName = "", string sourceFilePath = "", int sourceLineNumber = 0)
    {
        return impl.ExistsAsync(key, memberName, sourceFilePath, sourceLineNumber);
    }

    public Task<long> SubscribePublishAsync(string channel, string message)
    {
        return impl.SubscribePublishAsync(channel, message);
    }

    public Task SubscribeHandlerAsync(string channel, Action<string, string> handler)
    {
        return impl.SubscribeHandlerAsync(channel, handler);
    }

    public Task<bool> SortedSetAddAsync(string key, double score, string member, string memberName = "", string sourceFilePath = "",
        int sourceLineNumber = 0)
    {
        throw new NotImplementedException();
    }

    public Task<string[]> SortedSetRangeByScoreAsync(string key, double minScore, double maxScore = Double.PositiveInfinity,
        string memberName = "", string sourceFilePath = "", int sourceLineNumber = 0)
    {
        throw new NotImplementedException();
    }

    public Task<long> SortedSetRemoveRangeByScoreAsync(string key, double minScore, double maxScore, string memberName = "",
        string sourceFilePath = "", int sourceLineNumber = 0)
    {
        throw new NotImplementedException();
    }

    public Task<long> SortedSetLengthAsync(string key, string memberName = "", string sourceFilePath = "", int sourceLineNumber = 0)
    {
        throw new NotImplementedException();
    }

    public Task<long> SortedSetRemoveRangeByRankAsync(string key, long start, long stop, string memberName = "",
        string sourceFilePath = "", int sourceLineNumber = 0)
    {
        throw new NotImplementedException();
    }

    public Task<bool> SortedSetRemoveAsync(string key, string member, string memberName = "", string sourceFilePath = "",
        int sourceLineNumber = 0)
    {
        throw new NotImplementedException();
    }

    public Task<double?> SortedSetScoreAsync(string key, string member, string memberName = "", string sourceFilePath = "",
        int sourceLineNumber = 0)
    {
        throw new NotImplementedException();
    }

    public IRedisBatch CreateBatch(string auditSpanName, string memberName = "", string sourceFilePath = "",
        int sourceLineNumber = 0)
    {
        throw new NotImplementedException();
    }
}
