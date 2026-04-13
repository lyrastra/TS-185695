using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Infrastructure.Redis.Abstractions.Interfaces;
using Moedelo.Infrastructure.Redis.Abstractions.Models;
using Moedelo.Infrastructure.Redis.Extensions;
using StackExchange.Redis;

namespace Moedelo.Infrastructure.Redis
{
    [InjectAsSingleton(typeof(IRedisDbExecuter))]
    internal sealed class RedisDbExecutor : IRedisDbExecuter
    {
        private readonly IConnectionMultiplexerPool connectionMultiplexerPool;

        public RedisDbExecutor(IConnectionMultiplexerPool connectionMultiplexerPool)
        {
            this.connectionMultiplexerPool = connectionMultiplexerPool;
        }

        public bool IsAvailable(IRedisConnection connection)
        {
            var multiplexer = connectionMultiplexerPool.GetConnectionMultiplexer(connection);

            return multiplexer is { IsConnected: true };
        }

        public Task<bool> SetValueForKeyAsync(
            IRedisConnection connection, 
            string key, 
            string value, 
            TimeSpan? expiry,
            bool keepTtl)
        {
            if (keepTtl && expiry.HasValue)
            {
                throw new ArgumentException("Keep TTL can't be set if expiry is set.");
            }

            var redisDatabase = GetDataBase(connection);

            return redisDatabase.StringSetAsync(key, value, expiry, keepTtl: keepTtl);
        }

        public Task<long> PushValueToListForKeyAsync(
            IRedisConnection connection, 
            string key, 
            string value)
        {
            var redisDatabase = GetDataBase(connection);

            return redisDatabase.ListRightPushAsync(key, (RedisValue)value);
        }

        public async Task<string> PopValueFromListForKeyAsync(
            IRedisConnection connection, 
            string key)
        {
            var redisDatabase = GetDataBase(connection);
            var result = await redisDatabase.ListLeftPopAsync(key).ConfigureAwait(false);

            return result.ToString();
        }

        public async Task<long> SetValueListForKeyAsync(
            IRedisConnection connection, 
            string key, 
            IReadOnlyCollection<string> valueCollection)
        {
            var redisDatabase = GetDataBase(connection);
            await redisDatabase.KeyDeleteAsync(key).ConfigureAwait(false);
            var values = valueCollection.Select(x => (RedisValue) x).ToArray();
            var result = await redisDatabase.ListRightPushAsync(key, values).ConfigureAwait(false);

            return result;
        }

        public async Task<string> GetValueByKeyAsync(
            IRedisConnection connection, 
            string key)
        {
            var redisDatabase = GetDataBase(connection);
            var result = await redisDatabase.StringGetAsync(key).ConfigureAwait(false);

            return result;
        }

        public async Task<string[]> GetValueListByKeyAsync(
            IRedisConnection connection, 
            string key)
        {
            var redisDatabase = GetDataBase(connection);
            var result = await redisDatabase.ListRangeAsync(key).ConfigureAwait(false);

            // ReSharper disable once ConstantConditionalAccessQualifier
            // ReSharper disable once ConstantNullCoalescingCondition
            return result?.ToStringArray() ?? [];
        }

        public async Task<long?> LPosAsync(
            IRedisConnection connection,
            string key,
            string value)
        {
            var redisDatabase = GetDataBase(connection);

            var redisValue = await redisDatabase.ListPositionAsync(key, value).ConfigureAwait(false);

            return redisValue >= 0 ? redisValue : null;
        }

        public string[] GetKeyListByMatch(
            IRedisConnection connection, 
            string match, 
            int count = 10)
        {
            var server = GetServer(connection);
            var result = server.Keys(connection.DbNumber, match, count).Take(count);

            return result.Select(x => x.ToString()).ToArray();
        }

        public Task<bool> DeleteKeyAsync(
            IRedisConnection connection, 
            string key)
        {
            var redisDatabase = GetDataBase(connection);

            return redisDatabase.KeyDeleteAsync(key);
        }

        public async Task<string> GetDeleteAsync(IRedisConnection connection, string key)
        {
            var db = GetDataBase(connection);

            var value = await db.StringGetDeleteAsync(key);

            return value;
        }

        public async Task<long> DeleteKeysAsync(
            IRedisConnection connection, 
            IReadOnlyCollection<string> keyCollection)
        {
            var redisDatabase = GetDataBase(connection);

            const int maxDeleteKeysAtOnce = 1000;
            var keysAmount = keyCollection.Count();

            if (keysAmount < maxDeleteKeysAtOnce)
            {
                return await redisDatabase
                    .KeyDeleteAsync(keyCollection.Select(k => (RedisKey)k).ToArray())
                    .ConfigureAwait(false);
            }

            var list = keyCollection as List<string> ?? keyCollection.ToList();
            var chunk = new string[maxDeleteKeysAtOnce];
            var result = 0L;
            for (var startIndex = 0; startIndex < keysAmount; startIndex += maxDeleteKeysAtOnce)
            {
                var chunkLength = Math.Min(maxDeleteKeysAtOnce, keysAmount - startIndex);
                list.CopyTo(startIndex, chunk, 0, chunkLength);

                result += await redisDatabase
                    .KeyDeleteAsync(chunk
                        .Take(chunkLength)
                        .Select(k => (RedisKey)k)
                        .ToArray())
                    .ConfigureAwait(false);
            }

            return result;
        }

        public async Task<string[]> GetValueListByKeyListAsync(
            IRedisConnection connection, 
            IReadOnlyCollection<string> keyCollection)
        {
            var redisDatabase = GetDataBase(connection);

            var keys = keyCollection
                .Select(key => (RedisKey) key)
                .ToArray();
            var result = await redisDatabase.StringGetAsync(keys).ConfigureAwait(false);

            // ReSharper disable once ConstantConditionalAccessQualifier
            // ReSharper disable once ConstantNullCoalescingCondition
            return result?.Select(r => r.IsNullOrEmpty ? null : r.ToString()).ToArray() ?? [];
        }

        public Task<long> IncrNumberValueForKeyAsync(
            IRedisConnection connection, 
            string key)
        {
            var redisDatabase = GetDataBase(connection);

            return redisDatabase.StringIncrementAsync(key);
        }

        public Task<long> DecrNumberValueForKeyAsync(
            IRedisConnection connection, 
            string key)
        {
            var redisDatabase = GetDataBase(connection);

            return redisDatabase.StringDecrementAsync(key);
        }

        public Task<bool> ExpireForKeyAsync(
            IRedisConnection connection, 
            string key, 
            TimeSpan expire)
        {
            var redisDatabase = GetDataBase(connection);

            return redisDatabase.KeyExpireAsync(key, expire);
        }

        public async Task<bool> LpushAndLtrimInListForKeyAsync(
            IRedisConnection connection, 
            string key, 
            string value, 
            int trimSize)
        {
            var redisDatabase = GetDataBase(connection);
            var result = await redisDatabase.ListLeftPushAsync(key, value).ConfigureAwait(false);
            await redisDatabase.ListTrimAsync(key, 0, trimSize - 1).ConfigureAwait(false);

            return result > 0;
        }

        public async Task<bool> DeleteValueInListForKeyAsync(
            IRedisConnection connection, 
            string key, 
            string value)
        {
            var redisDatabase = GetDataBase(connection);
            var result = await redisDatabase.ListRemoveAsync(key, value).ConfigureAwait(false);

            return result > 0;
        }

        public async Task<Dictionary<string, string>> GetDictionaryByKeyAsync(
            IRedisConnection connection, 
            string key)
        {
            var redisDatabase = GetDataBase(connection);
            var result = await redisDatabase.HashGetAllAsync(key).ConfigureAwait(false);

            return result.ToStringDictionary();
        }

        public async Task SetDictionaryForKeyAsync(
            IRedisConnection connection, 
            string key, 
            IReadOnlyCollection<KeyValuePair<string, string>> dictionary,
            TimeSpan? expiry = null)
        {
            dictionary.ValidateHashSet();

            var redisDatabase = GetDataBase(connection);
            await redisDatabase.KeyDeleteAsync(key).ConfigureAwait(false);
            var hashFields = dictionary.Select(kv => new HashEntry(kv.Key, kv.Value)).ToArray();
            await redisDatabase.HashSetAsync(key, hashFields).ConfigureAwait(false);

            if (expiry.HasValue)
            {
                await redisDatabase.KeyExpireAsync(key, expiry).ConfigureAwait(false);
            }
        }

        public async Task<string> GetFieldValueFromDictionaryByKeyAsync(
            IRedisConnection connection, 
            string key, 
            string field)
        {
            var redisDatabase = GetDataBase(connection);
            var result = await redisDatabase.HashGetAsync(key, field).ConfigureAwait(false);

            return result;
        }

        public Task SetFieldValueToDictionaryForKeyAsync(
            IRedisConnection connection, 
            string key, 
            KeyValuePair<string, string> fieldValuePair)
        {
            var redisDatabase = GetDataBase(connection);
            var hashField = fieldValuePair.Key;
            var hashValue = fieldValuePair.Value;
            
            return redisDatabase.HashSetAsync(key, hashField, hashValue);
        }

        public Task SetFieldsValuesToDictionaryForKeyAsync(
            IRedisConnection connection, 
            string key,
            IReadOnlyCollection<KeyValuePair<string, string>> fieldValuePairCollection)
        {
            var redisDatabase = GetDataBase(connection);
            var hashFields = fieldValuePairCollection.Select(kv => new HashEntry(kv.Key, kv.Value)).ToArray();

            return redisDatabase.HashSetAsync(key, hashFields);
        }

        public Task DeleteFieldInDictionaryForKey(
            IRedisConnection connection, 
            string key, 
            string field)
        {
            var redisDatabase = GetDataBase(connection);

            return redisDatabase.HashDeleteAsync(key, field);
        }

        public Task SetAddAsync(IRedisConnection connection, string key, string setValue)
        {
            var db = GetDataBase(connection);

            return db.SetAddAsync(key, setValue);
        }

        public Task SetDeleteAsync(IRedisConnection connection, string key, string setValue)
        {
            var db = GetDataBase(connection);

            return db.SetRemoveAsync(key, setValue);
        }

        public async Task<HashSet<string>> GetSetAllAsync(IRedisConnection connection, string setKey)
        {
            var db = GetDataBase(connection);

            var members = await db.SetMembersAsync(setKey).ConfigureAwait(false);

            return new HashSet<string>(members.Select(member => member.ToString()));
        }

        public Task DeleteFieldsInDictionaryForKey(
            IRedisConnection connection, 
            string key, 
            IReadOnlyCollection<string> fieldCollection)
        {
            var redisDatabase = GetDataBase(connection);
            var hashFields = fieldCollection.Select(field => (RedisValue) field).ToArray();
            
            return redisDatabase.HashDeleteAsync(key, hashFields);
        }

        public Task<bool> DistributedLockRunAsync(
            IRedisConnection connection, 
            string keyLock, 
            Action method, 
            DistributedLockSettings settings = null)
        {
            Task ActionToFunc()
            {
                method.Invoke();
                return Task.FromResult(true);
            }

            return DistributedLockRunAsync(connection, keyLock, ActionToFunc, settings);
        }

        public async Task<bool> DistributedLockRunAsync(
            IRedisConnection connection, 
            string keyLock, 
            Func<Task> method, 
            DistributedLockSettings settings = null)
        {
            settings ??= DistributedLockSettings.Default;

            var redisDatabase = GetDataBase(connection);
            var attempt = 0;
            var configureAwaitValue = settings.ConfigureAwaitValue;

            while (attempt < settings.AttemptCount)
            {
                var captured = await redisDatabase
                    .StringSetAsync(keyLock, 1, settings.Expiry, When.NotExists)
                    .ConfigureAwait(configureAwaitValue);
                
                if (captured)
                {
                    try
                    {
                        await method.Invoke().ConfigureAwait(configureAwaitValue);
                    }
                    finally
                    {
                        await redisDatabase.KeyDeleteAsync(keyLock).ConfigureAwait(configureAwaitValue);
                    }

                    return true;
                }

                await Task.Delay(settings.Delay).ConfigureAwait(configureAwaitValue);
                ++attempt;
            }

            return false;
        }

        public async Task<DistributedLockRunResult> DistributedLockRunAsync(
            IRedisConnection connection,
            string keyLock,
            Func<Task> method,
            DistributedLockSettings settings,
            CancellationToken cancellationToken)
        {
            settings ??= DistributedLockSettings.Default;

            var redisDatabase = GetDataBase(connection);
            var retryNumber = 0;
            var configureAwaitValue = settings.ConfigureAwaitValue;
            var awaitingTimeSpan = Stopwatch.StartNew();

            while (retryNumber < settings.AttemptCount)
            {
                var captured = await redisDatabase
                    .StringSetAsync(keyLock, new RedisValue("1"), settings.Expiry, When.NotExists)
                    .ConfigureAwait(configureAwaitValue);
                
                if (captured)
                {
                    try
                    {
                        awaitingTimeSpan.Stop();
                        await method.Invoke().ConfigureAwait(configureAwaitValue);
                    }
                    finally
                    {
                        await redisDatabase.KeyDeleteAsync(keyLock).ConfigureAwait(configureAwaitValue);
                    }

                    return new DistributedLockRunResult
                    {
                        Success = true,
                        AttemptCount = retryNumber + 1,
                        AwaitingBeforeInvoke = awaitingTimeSpan.Elapsed
                    };
                }

                await Task.Delay(settings.Delay, cancellationToken).ConfigureAwait(configureAwaitValue);
                ++retryNumber;
            }

            return new DistributedLockRunResult
            {
                Success = false,
                AttemptCount = retryNumber + 1,
                AwaitingBeforeInvoke = awaitingTimeSpan.Elapsed
            };
        }

        public async Task<bool> DistributedLockRunAsync(
            IRedisConnection connection,
            string keyLock,
            Func<CancellationToken, Task> method,
            DistributedLockSettings settings = null)
        {
            settings ??= DistributedLockSettings.Default;

            var redisDatabase = GetDataBase(connection);
            var attempt = 0;
            var configureAwaitValue = settings.ConfigureAwaitValue;

            while (attempt < settings.AttemptCount)
            {
                var captured = await redisDatabase
                    .StringSetAsync(keyLock, 1, settings.Expiry, When.NotExists)
                    .ConfigureAwait(configureAwaitValue);

                if (captured)
                {
                    try
                    {
                        using var ctSource = new CancellationTokenSource(settings.Expiry);

                        await method.Invoke(ctSource.Token).ConfigureAwait(configureAwaitValue);

                        return true;
                    }
                    finally
                    {
                        await redisDatabase.KeyDeleteAsync(keyLock).ConfigureAwait(configureAwaitValue);
                    }
                }

                await Task.Delay(settings.Delay).ConfigureAwait(configureAwaitValue);
                ++attempt;
            }

            return false;
        }

        public async Task<bool> DistributedReadLockAsync(
            IRedisConnection connection,
            string keyLock,
            DistributedLockSettings settings = null)
        {
            settings ??= DistributedLockSettings.Default;

            var redisDatabase = GetDataBase(connection);
            var maxAttempts = Math.Max(1, settings.AttemptCount);
            var attempt = 0;
            var configureAwaitValue = settings.ConfigureAwaitValue;
            
            while (attempt < maxAttempts)
            {
                var lockExists = await redisDatabase.KeyExistsAsync(keyLock).ConfigureAwait(configureAwaitValue);
                if (!lockExists)
                {
                    return true;
                }
                
                await Task.Delay(settings.Delay).ConfigureAwait(configureAwaitValue);
                ++attempt;
            }

            return false;
        }

        public async Task<(bool success, T result)> DistributedLockRunAsync<T>(
            IRedisConnection connection, 
            string keyLock, 
            Func<Task<T>> method, 
            DistributedLockSettings settings = null)
        {
            settings ??= DistributedLockSettings.Default;

            var redisDatabase = GetDataBase(connection);
            var maxAttempts = Math.Max(1, settings.AttemptCount);
            var attempt = 0;
            var configureAwaitValue = settings.ConfigureAwaitValue;
            
            while (attempt < maxAttempts)
            {
                var captured = await redisDatabase
                    .StringSetAsync(keyLock, 1, settings.Expiry, When.NotExists)
                    .ConfigureAwait(configureAwaitValue);

                if (captured)
                {
                    try
                    {
                        var returnData = await method.Invoke().ConfigureAwait(configureAwaitValue);
                        return (true, returnData);
                    }
                    finally
                    {
                        await redisDatabase.KeyDeleteAsync(keyLock).ConfigureAwait(configureAwaitValue);
                    }
                }

                await Task.Delay(settings.Delay).ConfigureAwait(configureAwaitValue);
                ++attempt;
            }

            return (false, default);
        }

        /// <summary>
        /// Исполнение метода, возвращающего данные, в глобальной блокировке
        /// </summary>
        /// <param name="connection">Соединение</param>
        /// <param name="keyLock">ключ - наличие в бд ключа блокировка уже взята</param>
        /// <param name="method"></param>
        /// <param name="settings"></param>
        /// <param name="cancellationToken">токен отмены операции</param>
        /// <returns>T - если метод выполнился, Default(T) - если метод не выполнился</returns>
        public async Task<(bool success, TResultType result)> DistributedLockRunAsync<TResultType>(
            IRedisConnection connection,
            string keyLock,
            Func<CancellationToken, Task<TResultType>> method,
            DistributedLockSettings settings,
            CancellationToken cancellationToken)
        {
            settings ??= DistributedLockSettings.Default;

            var redisDatabase = GetDataBase(connection);
            var attempt = 0;
            var configureAwaitValue = settings.ConfigureAwaitValue;

            while (attempt < settings.AttemptCount)
            {
                var captured = await redisDatabase
                    .StringSetAsync(keyLock, 1, settings.Expiry, When.NotExists)
                    .ConfigureAwait(configureAwaitValue);

                if (captured)
                {
                    try
                    {
                        using var ctSource = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken);
                        ctSource.CancelAfter(settings.Expiry);

                        var result = await method.Invoke(ctSource.Token).ConfigureAwait(configureAwaitValue);

                        return (true, result);
                    }
                    finally
                    {
                        await redisDatabase.KeyDeleteAsync(keyLock).ConfigureAwait(configureAwaitValue);
                    }
                }

                await Task.Delay(settings.Delay, cancellationToken).ConfigureAwait(configureAwaitValue);
                ++attempt;
            }

            return (false, default);
        }

        public Task<long> SubscribePublishAsync(IRedisConnection connection, string channel, string message)
        {
            var subscriber = GetSubscriber(connection);
            var redisChannel = new RedisChannel(channel, RedisChannel.PatternMode.Auto);

            return subscriber.PublishAsync(redisChannel, message);
        }

        public Task SubscribeHandlerAsync(IRedisConnection connection, string channel, Action<string, string> handler)
        {
            var subscriber = GetSubscriber(connection);
            var redisChannel = new RedisChannel(channel, RedisChannel.PatternMode.Auto);

            return subscriber.SubscribeAsync(redisChannel, (rChannel, rValue) => { handler.Invoke(rChannel, rValue);});
        }

        public Task<bool> SortedSetAddAsync(IRedisConnection connection, string key, double score, string member)
        {
            var db = GetDataBase(connection);

            return db.SortedSetAddAsync(key, member, score);
        }

        public async Task<string[]> SortedSetRangeByScoreAsync(
            IRedisConnection connection,
            string key,
            double minScore,
            double maxScore = double.PositiveInfinity)
        {
            var db = GetDataBase(connection);
            var result = await db.SortedSetRangeByScoreAsync(key, minScore, maxScore).ConfigureAwait(false);

            return result?.ToStringArray() ?? [];
        }

        public Task<long> SortedSetRemoveRangeByScoreAsync(
            IRedisConnection connection,
            string key,
            double minScore,
            double maxScore)
        {
            var db = GetDataBase(connection);

            return db.SortedSetRemoveRangeByScoreAsync(key, minScore, maxScore);
        }

        public Task<long> SortedSetLengthAsync(IRedisConnection connection, string key)
        {
            var db = GetDataBase(connection);

            return db.SortedSetLengthAsync(key);
        }

        public Task<long> SortedSetRemoveRangeByRankAsync(
            IRedisConnection connection,
            string key,
            long start,
            long stop)
        {
            var db = GetDataBase(connection);

            return db.SortedSetRemoveRangeByRankAsync(key, start, stop);
        }

        public Task<bool> SortedSetRemoveAsync(IRedisConnection connection, string key, string member)
        {
            var db = GetDataBase(connection);

            return db.SortedSetRemoveAsync(key, member);
        }

        public Task<double?> SortedSetScoreAsync(IRedisConnection connection, string key, string member)
        {
            var db = GetDataBase(connection);

            return db.SortedSetScoreAsync(key, member);
        }

        public Task<bool> ExistsAsync(IRedisConnection connection, string key)
        {
            var redisDatabase = GetDataBase(connection);

            return redisDatabase.KeyExistsAsync(key);
        }

        public Task<TimeSpan?> KeyTimeToLiveAsync(IRedisConnection connection, string key)
        {
            var redisDatabase = GetDataBase(connection);

            return redisDatabase.KeyTimeToLiveAsync(key);
        }

        public IRedisBatch CreateBatch(IRedisConnection connection)
        {
            var db = GetDataBase(connection);

            return new RedisBatch(db.CreateBatch());
        }

        private ISubscriber GetSubscriber(IRedisConnection connection)
        {
            var multiplexer = connectionMultiplexerPool.GetConnectionMultiplexer(connection);
            var subscriber = multiplexer.GetSubscriber();

            if (subscriber == null)
            {
                throw new Exception("RedisDbExecuter: can't create redis Subscriber.");
            }

            return subscriber;
        }

        private IDatabase GetDataBase(IRedisConnection connection)
        {
            var multiplexer = connectionMultiplexerPool.GetConnectionMultiplexer(connection);
            var redisDatabase = multiplexer.GetDatabase(connection.DbNumber);

            if (redisDatabase == null)
            {
                throw new Exception("RedisDbExecutor: can't create redis database connection.");
            }

            return redisDatabase;
        }

        private IServer GetServer(IRedisConnection connection)
        {
            var multiplexer = connectionMultiplexerPool.GetConnectionMultiplexer(connection);
            var host = multiplexer.GetEndPoints().FirstOrDefault();

            if (host != null)
            {
                return multiplexer.GetServer(host);
            }

            throw new Exception("RedisDbExecutor: can't create redis database connection.");
        }
    }
}