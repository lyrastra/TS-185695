using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using Moedelo.Infrastructure.Redis.Abstractions.Models;
using Moedelo.InfrastructureV2.Domain.AuditModule.Extensions;
using Moedelo.InfrastructureV2.Domain.AuditModule.Helpers;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.AuditModule.Models;
using Moedelo.InfrastructureV2.Domain.Interfaces.Redis;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.Domain.Models.Redis;
using Moedelo.InfrastructureV2.Domain.Models.Setting;
using Moedelo.InfrastructureV2.Json;
using Moedelo.InfrastructureV2.RedisDataAccess.Extensions;
using IRedisDbExecutor = Moedelo.Infrastructure.Redis.Abstractions.Interfaces.IRedisDbExecuter;

// ReSharper disable ExplicitCallerInfoArgument

namespace Moedelo.InfrastructureV2.RedisDataAccess.Common
{
    public abstract class RedisDbExecuterBase : IRedisDbExecuter
    {
        private static readonly string DefaultKeyPrefix = Environment.MachineName;

        private readonly string typeName;

        private readonly IRedisDbExecutor redisExecutor;
        private readonly SettingValue redisConnectionString;
        private readonly SettingValue needToUseKeyPrefixSetting;
        private readonly SettingValue keyPrefixSetting;
        private readonly int redisDbNumber;
        private readonly IAuditTracer auditTracer;
        private readonly ConcurrentDictionary<string, AuditedRedisConnection> connections = new();

        protected RedisDbExecuterBase(
            IRedisDbExecutor redisExecutor,
            ISettingRepository settingRepository,
            SettingValue redisConnectionString,
            int redisDbNumber, 
            IAuditTracer auditTracer)
        {
            typeName = GetType().Name;

            this.redisExecutor = redisExecutor;
            this.redisConnectionString = redisConnectionString;
            this.redisDbNumber = redisDbNumber;
            this.auditTracer = auditTracer;
            this.needToUseKeyPrefixSetting = settingRepository.GetRedisNeedToUseKeyPrefixSetting();
            this.keyPrefixSetting = settingRepository.GetRedisKeyPrefixSetting();
        }

        private AuditedRedisConnection GetConnection()
        {
            return connections.GetOrAdd(redisConnectionString.Value, CreateConnection);
        }

        private AuditedRedisConnection CreateConnection(string connectionString)
        {
            return new AuditedRedisConnection(connectionString, redisDbNumber);
        }

        private async Task<TResult> RunInAuditScope<TResult>(
            string key,
            string callerMemberName,
            string sourceFilePath,
            int sourceLineNumber,
            Func<IRedisConnection, string, Task<TResult>> action,
            AuditSpanType auditSpanType = AuditSpanType.RedisDbQuery, 
            [CallerMemberName] string memberName = "")
        {
            var connection = GetConnection();
            var spanName = $"{sourceFilePath.GetSourceFileName()}@{sourceLineNumber}.{callerMemberName} -> {typeName}.{memberName}";
            var realKey = GetKey(key);

            using var scope = auditTracer
                .BuildSpan(auditSpanType, spanName)
                .WithStartDateUtc(DateTimeOffset.UtcNow)
                .WithConnection(connection)
                .WithKey(realKey)
                .TagCodeSourcePath(callerMemberName, sourceFilePath, sourceLineNumber)
                .Start();
            try
            {
                var result = await action(connection, realKey).ConfigureAwait(false);

                return result;
            }
            catch (Exception exception)
            {
                scope.Span.SetError(exception);
                throw;
            }
        }

        private async Task<TResult> RunInAuditScope<T, TResult>(
            string key,
            T parameters,
            string callerMemberName,
            string sourceFilePath,
            int sourceLineNumber,
            Func<IRedisConnection, string, T, Task<TResult>> action,
            [CallerMemberName] string memberName = "") where T : class
        {
            var connection = GetConnection();
            var spanName = $"{sourceFilePath.GetSourceFileName()}@{sourceLineNumber}.{callerMemberName} -> {typeName}.{memberName}";
            var realKey = GetKey(key);

            using var scope = auditTracer
                .BuildSpan(AuditSpanType.RedisDbQuery, spanName)
                .WithStartDateUtc(DateTimeOffset.UtcNow)
                .WithConnection(connection)
                .WithKey(realKey)
                .WithParams(parameters)
                .TagCodeSourcePath(callerMemberName, sourceFilePath, sourceLineNumber)
                .Start();
            try
            {
                var result = await action(connection, realKey, parameters).ConfigureAwait(false);

                return result;
            }
            catch (Exception exception)
            {
                scope.Span.SetError(exception);
                throw;
            }
        }
        
        private async Task<TResult> RunInAuditScope<T, TResult>(
            string key,
            T parameters,
            string callerMemberName,
            string sourceFilePath,
            int sourceLineNumber,
            Func<IRedisDbExecutor, IRedisConnection, string, T, Task<TResult>> action,
            [CallerMemberName] string memberName = "")
        {
            var connection = GetConnection();
            var spanName = $"{sourceFilePath.GetSourceFileName()}@{sourceLineNumber}.{callerMemberName} -> {typeName}.{memberName}";
            var realKey = GetKey(key);

            using var scope = auditTracer
                .BuildSpan(AuditSpanType.RedisDbQuery, spanName)
                .WithStartDateUtc(DateTimeOffset.UtcNow)
                .WithConnection(connection)
                .WithKey(realKey)
                .WithParams(parameters)
                .TagCodeSourcePath(callerMemberName, sourceFilePath, sourceLineNumber)
                .Start();
            try
            {
                var result = await action(redisExecutor, connection, realKey, parameters).ConfigureAwait(false);

                return result;
            }
            catch (Exception exception)
            {
                scope.Span.SetError(exception);
                throw;
            }
        }

        private async Task RunInAuditScope<T>(
            string key,
            T parameters,
            string callerMemberName,
            string sourceFilePath,
            int sourceLineNumber,
            Func<IRedisDbExecutor, IRedisConnection, string, T, Task> action,
            [CallerMemberName] string memberName = "") where T : class
        {
            var connection = GetConnection();
            var spanName = $"{sourceFilePath.GetSourceFileName()}@{sourceLineNumber}.{callerMemberName} -> {typeName}.{memberName}";
            var realKey = GetKey(key);

            using var scope = auditTracer
                .BuildSpan(AuditSpanType.RedisDbQuery, spanName)
                .WithStartDateUtc(DateTimeOffset.UtcNow)
                .WithConnection(connection)
                .WithKey(realKey)
                .WithParams(parameters)
                .TagCodeSourcePath(callerMemberName, sourceFilePath, sourceLineNumber)
                .Start();
            try
            {
                await action(redisExecutor, connection, realKey, parameters).ConfigureAwait(false);
            }
            catch (Exception exception)
            {
                scope.Span.SetError(exception);
                throw;
            }
        }

        private async Task<TResult> RunInAuditScope<TResult>(
            IReadOnlyCollection<string> keys,
            string callerMemberName,
            string sourceFilePath,
            int sourceLineNumber,
            Func<IRedisConnection, IReadOnlyCollection<string>, Task<TResult>> action,
            [CallerMemberName] string memberName = "")
        {
            var connection = GetConnection();
            var spanName = $"{sourceFilePath.GetSourceFileName()}@{sourceLineNumber}.{callerMemberName} -> {typeName}.{memberName}";
            var realKeys = keys.Select(GetKey).ToArray();

            using var scope = auditTracer
                .BuildSpan(AuditSpanType.RedisDbQuery, spanName)
                .WithStartDateUtc(DateTimeOffset.UtcNow)
                .WithConnection(connection)
                .WithKeys(realKeys)
                .TagCodeSourcePath(callerMemberName, sourceFilePath, sourceLineNumber)
                .Start();
            try
            {
                var result = await action(connection, realKeys).ConfigureAwait(false);

                return result;
            }
            catch (Exception exception)
            {
                scope.Span.SetError(exception);
                throw;
            }
        }

        private async Task RunInAuditScope<T>(
            T parameters,
            string callerMemberName,
            string sourceFilePath,
            int sourceLineNumber,
            Func<IRedisDbExecutor, IRedisConnection, T, Task> action,
            [CallerMemberName] string memberName = "") where T : class
        {
            var connection = GetConnection();
            var spanName = $"{sourceFilePath.GetSourceFileName()}@{sourceLineNumber}.{callerMemberName} -> {typeName}.{memberName}";

            using var scope = auditTracer
                .BuildSpan(AuditSpanType.RedisDbQuery, spanName)
                .WithStartDateUtc(DateTimeOffset.UtcNow)
                .WithConnection(connection)
                .WithParams(parameters)
                .TagCodeSourcePath(callerMemberName, sourceFilePath, sourceLineNumber)
                .Start();
            try
            {
                await action(redisExecutor, connection, parameters).ConfigureAwait(false);
            }
            catch (Exception exception)
            {
                scope.Span.SetError(exception);
                throw;
            }
        }


        public Task<long> DecrNumberValueForKeyAsync(
            string key,
            [CallerMemberName] string memberName = "",
            [CallerFilePath] string sourceFilePath = "",
            [CallerLineNumber] int sourceLineNumber = 0)
        {
            return RunInAuditScope(
                    key,
                    memberName,
                    sourceFilePath,
                    sourceLineNumber,
                    redisExecutor.DecrNumberValueForKeyAsync);
        }

        public Task<string> GetAndDeleteAsync(string key,
            [CallerMemberName] string memberName = "",
            [CallerFilePath] string sourceFilePath = "",
            [CallerLineNumber] int sourceLineNumber = 0)
        {
            return RunInAuditScope(
                key,
                memberName,
                sourceFilePath,
                sourceLineNumber,
                redisExecutor.GetDeleteAsync);
        }

        public Task<bool> DeleteKeyAsync(
            string key,
            [CallerMemberName] string memberName = "",
            [CallerFilePath] string sourceFilePath = "",
            [CallerLineNumber] int sourceLineNumber = 0)
        {
            return RunInAuditScope(
                key,
                memberName,
                sourceFilePath,
                sourceLineNumber,
                redisExecutor.DeleteKeyAsync);
        }

        public Task<long> DeleteKeysAsync(
            IReadOnlyCollection<string> keyCollection,
            [CallerMemberName] string memberName = "",
            [CallerFilePath] string sourceFilePath = "",
            [CallerLineNumber] int sourceLineNumber = 0)
        {
            if (keyCollection?.Any() != true)
            {
                return Task.FromResult(0L);
            }

            return RunInAuditScope(
                keyCollection,
                memberName,
                sourceFilePath,
                sourceLineNumber,
                redisExecutor.DeleteKeysAsync);
        }

        public Task DeleteKeysMatchedAsync(
            string match,
            [CallerMemberName] string memberName = "",
            [CallerFilePath] string sourceFilePath = "",
            [CallerLineNumber] int sourceLineNumber = 0)
        {
            var realMatch = GetKey(match);

            return RunInAuditScope(
                new { Match = realMatch },
                memberName,
                sourceFilePath,
                sourceLineNumber,
                static async (executor, connection, parameters) => {
                    const int maxKeysPerOnce = 100;
                    string[] keys;

                    while ((keys = executor.GetKeyListByMatch(connection, parameters.Match, maxKeysPerOnce)).Length > 0)
                    {
                        await executor.DeleteKeysAsync(connection, keys).ConfigureAwait(false);
                    }
                });
        }

        public Task<bool> DeleteValueInListForKeyAsync(
            string key, 
            string value,
            [CallerMemberName] string memberName = "",
            [CallerFilePath] string sourceFilePath = "",
            [CallerLineNumber] int sourceLineNumber = 0)
        {
            return RunInAuditScope(
                key,
                value,
                memberName,
                sourceFilePath,
                sourceLineNumber,
                redisExecutor.DeleteValueInListForKeyAsync);
        }

        public Task<Dictionary<string, string>> GetDictionaryByKeyAsync(
            string key, 
            [CallerMemberName] string memberName = "",
            [CallerFilePath] string sourceFilePath = "",
            [CallerLineNumber] int sourceLineNumber = 0)
        {
            return RunInAuditScope(
                key,
                memberName,
                sourceFilePath,
                sourceLineNumber,
                redisExecutor.GetDictionaryByKeyAsync);
        }

        public async Task<Dictionary<string, T>> GetDictionaryByKeyAsync<T>(
            string key,
            [CallerMemberName] string memberName = "",
            [CallerFilePath] string sourceFilePath = "",
            [CallerLineNumber] int sourceLineNumber = 0) where T : class
        {
            var result = await GetDictionaryByKeyAsync(key, memberName, sourceFilePath, sourceLineNumber)
                .ConfigureAwait(false);

            return result?.ToDictionary(r => r.Key, r => r.Value?.FromJsonString<T>());
        }

        public Task SetDictionaryForKeyAsync(
            string key, 
            IReadOnlyCollection<KeyValuePair<string, string>> dictionary,
            TimeSpan? expiry = null, 
            [CallerMemberName] string memberName = "",
            [CallerFilePath] string sourceFilePath = "",
            [CallerLineNumber] int sourceLineNumber = 0)
        {
            return RunInAuditScope(
                key,
                new { Dictionary = dictionary, Expiry = expiry },
                memberName,
                sourceFilePath,
                sourceLineNumber,
                static (executor, connection, realKey, parameters) => executor
                    .SetDictionaryForKeyAsync(connection, realKey, parameters.Dictionary, parameters.Expiry));
        }

        public Task SetDictionaryForKeyAsync<T>(
            string key, 
            IReadOnlyCollection<KeyValuePair<string, T>> dictionary, 
            TimeSpan? expiry = null,
            [CallerMemberName] string memberName = "",
            [CallerFilePath] string sourceFilePath = "",
            [CallerLineNumber] int sourceLineNumber = 0) where T : class
        {
            return SetDictionaryForKeyAsync(
                key,
                dictionary.ToDictionary(kv => kv.Key, kv => kv.Value.ToJsonString()),
                expiry,
                memberName,
                sourceFilePath,
                sourceLineNumber
            );
        }

        public Task<string> GetFieldValueFromDictionaryByKeyAsync(
            string key, 
            string field, 
            [CallerMemberName] string memberName = "",
            [CallerFilePath] string sourceFilePath = "",
            [CallerLineNumber] int sourceLineNumber = 0)
        {
            return RunInAuditScope(
                key,
                field,
                memberName,
                sourceFilePath,
                sourceLineNumber,
                redisExecutor.GetFieldValueFromDictionaryByKeyAsync);
        }

        public async Task<T> GetFieldValueFromDictionaryByKeyAsync<T>(
            string key, 
            string field, 
            [CallerMemberName] string memberName = "",
            [CallerFilePath] string sourceFilePath = "",
            [CallerLineNumber] int sourceLineNumber = 0) where T : class
        {
            var result = await GetFieldValueFromDictionaryByKeyAsync(
                key,
                field,
                memberName,
                sourceFilePath,
                sourceLineNumber).ConfigureAwait(false);

            return result?.FromJsonString<T>();
        }

        public Task SetFieldValueToDictionaryForKeyAsync(
            string key, 
            KeyValuePair<string, string> fieldValuePair,
            [CallerMemberName] string memberName = "",
            [CallerFilePath] string sourceFilePath = "",
            [CallerLineNumber] int sourceLineNumber = 0)
        {
            return RunInAuditScope(
                key,
                new { FieldValuePair = fieldValuePair },
                memberName,
                sourceFilePath,
                sourceLineNumber,
                static (executor, connection, realKey, parameters) => executor
                    .SetFieldValueToDictionaryForKeyAsync(connection, realKey, parameters.FieldValuePair));
        }

        public Task SetFieldValueToDictionaryForKeyAsync<T>(
            string key, 
            KeyValuePair<string, T> fieldValuePair, 
            [CallerMemberName] string memberName = "",
            [CallerFilePath] string sourceFilePath = "",
            [CallerLineNumber] int sourceLineNumber = 0) where T : class
        {
            return SetFieldValueToDictionaryForKeyAsync(
                key,
                new KeyValuePair<string, string>(fieldValuePair.Key, fieldValuePair.Value.ToJsonString()),
                memberName,
                sourceFilePath,
                sourceLineNumber);
        }

        public Task SetFieldsValuesToDictionaryForKeyAsync(
            string key,
            IReadOnlyCollection<KeyValuePair<string, string>> fieldValuePairCollection, 
            [CallerMemberName] string memberName = "",
            [CallerFilePath] string sourceFilePath = "",
            [CallerLineNumber] int sourceLineNumber = 0)
        {
            return RunInAuditScope(
                key,
                new { FieldValuePairCollection = fieldValuePairCollection },
                memberName,
                sourceFilePath,
                sourceLineNumber,
                static (executor, connection, realKey, parameters) => executor
                    .SetFieldsValuesToDictionaryForKeyAsync(connection, realKey, parameters.FieldValuePairCollection));
        }

        public Task SetFieldsValuesToDictionaryForKeyAsync<T>(
            string key, 
            IReadOnlyCollection<KeyValuePair<string, T>> fieldValuePairCollection,
            [CallerMemberName] string memberName = "",
            [CallerFilePath] string sourceFilePath = "",
            [CallerLineNumber] int sourceLineNumber = 0) where T : class
        {
            return SetFieldsValuesToDictionaryForKeyAsync(
                key,
                fieldValuePairCollection.Select(kv => new KeyValuePair<string,string>(kv.Key, kv.Value.ToJsonString())).ToArray(),
                memberName,
                sourceFilePath,
                sourceLineNumber);
        }

        public Task DeleteFieldInDictionaryForKey(
            string key, 
            string field, 
            [CallerMemberName] string memberName = "",
            [CallerFilePath] string sourceFilePath = "",
            [CallerLineNumber] int sourceLineNumber = 0)
        {
            return RunInAuditScope(
                key,
                new { Field = field },
                memberName,
                sourceFilePath,
                sourceLineNumber,
                static (executor, connection, realKey, parameters) => executor
                    .DeleteFieldInDictionaryForKey(connection, realKey, parameters.Field));
        }

        public Task DeleteFieldsInDictionaryForKey(
            string key, 
            IReadOnlyCollection<string> fieldCollection,
            [CallerMemberName] string memberName = "",
            [CallerFilePath] string sourceFilePath = "",
            [CallerLineNumber] int sourceLineNumber = 0)
        {
            return RunInAuditScope(
                key,
                new { FieldCollection = fieldCollection },
                memberName,
                sourceFilePath,
                sourceLineNumber,
                static (executor, connection, realKey, parameters) => executor
                    .DeleteFieldsInDictionaryForKey(connection, realKey, parameters.FieldCollection));
        }

        public Task AddValueToSetAsync<T>(string key, T value, string memberName = "", string sourceFilePath = "",
            int sourceLineNumber = 0)
        {
            return RunInAuditScope(
                key,
                new { Value = value.ToJsonString() },
                memberName,
                sourceFilePath,
                sourceLineNumber,
                (executor, connection, realKey, parameters) => executor
                    .SetAddAsync(connection, realKey, parameters.Value));
        }

        public Task AddValueToSetAsync(string key, string value, string memberName = "", string sourceFilePath = "",
            int sourceLineNumber = 0)
        {
            return RunInAuditScope(
                key,
                value,
                memberName,
                sourceFilePath,
                sourceLineNumber,
                static (executor, connection, realKey, value1) => executor
                    .SetAddAsync(connection, realKey, value1));
        }

        public Task DeleteValueFromSetAsync<T>(string key, T value, string memberName = "", string sourceFilePath = "",
            int sourceLineNumber = 0)
        {
            return RunInAuditScope(
                key,
                new { Value = value.ToJsonString() },
                memberName,
                sourceFilePath,
                sourceLineNumber,
                static async (executor, connection, realKey, parameters) => await executor
                    .SetDeleteAsync(connection, realKey, parameters.Value));
        }

        public Task DeleteValueFromSetAsync(string key, string value, string memberName = "", string sourceFilePath = "",
            int sourceLineNumber = 0)
        {
            return RunInAuditScope(
                key,
                value,
                memberName,
                sourceFilePath,
                sourceLineNumber,
                static (executor, connection, realKey, value1) => executor
                    .SetDeleteAsync(connection, realKey, value1));
        }

        public Task<HashSet<string>> GetAllValuesOfSetAsync(string key, string memberName = "",
            string sourceFilePath = "",
            int sourceLineNumber = 0)
        {
            return RunInAuditScope(
                key,
                memberName,
                sourceFilePath,
                sourceLineNumber,
                async (connection, realKey) =>
                {
                    var values = await redisExecutor.GetSetAllAsync(connection, realKey);

                    return new HashSet<string>(values);
                });
        }

        public Task<HashSet<T>> GetAllValuesOfSetAsync<T>(string key, string memberName = "",
            string sourceFilePath = "",
            int sourceLineNumber = 0)
        {
            return RunInAuditScope(
                key,
                memberName,
                sourceFilePath,
                sourceLineNumber,
                async (connection, realKey) =>
                {
                    var values = await redisExecutor.GetSetAllAsync(connection, realKey);

                    return new HashSet<T>(values
                        .Where(value => !string.IsNullOrEmpty(value))
                        .Select(value => value.FromJsonString<T>()));
                });
        }

        public Task<bool> DistributedLockRunAsync(
            string keyLock, 
            Action method, 
            RedisQueryObject queryObject,
            [CallerMemberName] string memberName = "",
            [CallerFilePath] string sourceFilePath = "",
            [CallerLineNumber] int sourceLineNumber = 0)
        {
            return RunInAuditScope(
                keyLock,
                memberName,
                sourceFilePath,
                sourceLineNumber,
                (connection, redisKey) => redisExecutor
                    .DistributedLockRunAsync(connection, redisKey, method, queryObject.ToLockSettings()),
                AuditSpanType.RedisDistributedLock);
        }

        public Task<bool> DistributedLockRunAsync(
            string keyLock, 
            Func<Task> method, 
            RedisQueryObject queryObject = null,
            [CallerMemberName] string memberName = "",
            [CallerFilePath] string sourceFilePath = "",
            [CallerLineNumber] int sourceLineNumber = 0)
        {
            return RunInAuditScope(
                keyLock,
                memberName,
                sourceFilePath,
                sourceLineNumber,
                (connection, redisKey) => redisExecutor
                    .DistributedLockRunAsync(connection, redisKey, method, queryObject.ToLockSettings()),
                AuditSpanType.RedisDistributedLock);
        }
        
        public Task<bool> DistributedLockRunAsync(
            string keyLock,
            Func<CancellationToken, Task> method,
            RedisQueryObject queryObject,
            [CallerMemberName] string memberName = "",
            [CallerFilePath] string sourceFilePath = "",
            [CallerLineNumber] int sourceLineNumber = 0)
        {
            return RunInAuditScope(
                keyLock,
                memberName,
                sourceFilePath,
                sourceLineNumber,
                (connection, redisKey) => redisExecutor
                    .DistributedLockRunAsync(connection, redisKey, method, queryObject.ToLockSettings()),
                AuditSpanType.RedisDistributedLock);
        }

        public Task<bool> DistributedReadLockAsync(
            string keyLock, 
            RedisQueryObject queryObject = null,
            [CallerMemberName] string memberName = "",
            [CallerFilePath] string sourceFilePath = "",
            [CallerLineNumber] int sourceLineNumber = 0)
        {
            return RunInAuditScope(
                keyLock,
                memberName,
                sourceFilePath,
                sourceLineNumber,
                (connection, redisKey) => redisExecutor
                    .DistributedReadLockAsync(connection, redisKey, queryObject.ToLockSettings()),
                AuditSpanType.RedisDistributedLock);
        }

        public Task<T> DistributedLockRunAsync<T>(
            string keyLock, 
            Func<Task<T>> method, 
            RedisQueryObject queryObject = null,
            [CallerMemberName] string memberName = "",
            [CallerFilePath] string sourceFilePath = "",
            [CallerLineNumber] int sourceLineNumber = 0)
        {
            return RunInAuditScope(
                keyLock,
                memberName,
                sourceFilePath,
                sourceLineNumber,
                async (connection, redisKey) =>
                {
                    var (success, result) = await redisExecutor
                        .DistributedLockRunAsync(connection, redisKey, method, queryObject.ToLockSettings())
                        .ConfigureAwait(false);

                    return success ? result : default(T);
                },
                AuditSpanType.RedisDistributedLock);
        }

        public Task<T> DistributedLockRunAsync<T>(
            string keyLock,
            Func<CancellationToken, Task<T>> method,
            RedisQueryObject queryObject,
            CancellationToken cancellationToken,
            [CallerMemberName] string memberName = "",
            [CallerFilePath] string sourceFilePath = "",
            [CallerLineNumber] int sourceLineNumber = 0)
        {
            return RunInAuditScope(
                keyLock,
                memberName,
                sourceFilePath,
                sourceLineNumber,
                async (connection, redisKey) =>
                {
                    var (success, result) = await redisExecutor
                        .DistributedLockRunAsync(connection, redisKey, method, queryObject.ToLockSettings(), cancellationToken)
                        .ConfigureAwait(false);

                    return success ? result : default(T);
                },
                AuditSpanType.RedisDistributedLock);
        }

        public Task<bool> ExistsAsync(
            string key,
            [CallerMemberName] string memberName = "",
            [CallerFilePath] string sourceFilePath = "",
            [CallerLineNumber] int sourceLineNumber = 0)
        {
            return RunInAuditScope(
                key,
                memberName,
                sourceFilePath,
                sourceLineNumber,
                redisExecutor.ExistsAsync);
        }

        public Task<bool> ExpireForKeyAsync(
            string key, 
            TimeSpan expire,
            [CallerMemberName] string memberName = "",
            [CallerFilePath] string sourceFilePath = "",
            [CallerLineNumber] int sourceLineNumber = 0)
        {
            return RunInAuditScope(
                key,
                new { Expire = expire },
                memberName,
                sourceFilePath,
                sourceLineNumber,
                (connection, redisKey, parameters) => redisExecutor
                    .ExpireForKeyAsync(connection, redisKey, parameters.Expire));
        }

        public List<string> GetKeyListByMatch(
            string match, 
            int count = 10,
            [CallerMemberName] string memberName = "",
            [CallerFilePath] string sourceFilePath = "",
            [CallerLineNumber] int sourceLineNumber = 0)
        {
            var keyPrefix = KeyPrefix;

            return RunInAuditScope(
                match,
                new { Match = $"{keyPrefix}{match}", Count = count },
                memberName,
                sourceFilePath,
                sourceLineNumber,
                (connection, redisKey, parameters) => {
                    var keys = redisExecutor.GetKeyListByMatch(connection, redisKey, parameters.Count);

                    if (string.IsNullOrEmpty(keyPrefix))
                    {
                        return Task.FromResult(keys.ToList());
                    }

                    return Task.FromResult(keys.Select(key => key.Substring(keyPrefix.Length)).ToList());
                }).Result;
        }

        public Task<string> GetValueByKeyAsync(
            string key,
            [CallerMemberName] string memberName = "",
            [CallerFilePath] string sourceFilePath = "",
            [CallerLineNumber] int sourceLineNumber = 0)
        {
            return RunInAuditScope(
                key,
                memberName,
                sourceFilePath,
                sourceLineNumber,
                redisExecutor.GetValueByKeyAsync);
        }

        public Task<long?> GetValueIndexInListAsync(
            string key,
            string value,
            [CallerMemberName] string memberName = "",
            [CallerFilePath] string sourceFilePath = "",
            [CallerLineNumber] int sourceLineNumber = 0)
        {
            return RunInAuditScope(
                key,
                memberName,
                sourceFilePath,
                sourceLineNumber,
                (connection, realKey) => redisExecutor.LPosAsync(connection, realKey, value));
        }

        public async Task<TR> GetValueByKeyAsync<TR>(
            string key,
            [CallerMemberName] string memberName = "",
            [CallerFilePath] string sourceFilePath = "",
            [CallerLineNumber] int sourceLineNumber = 0) where TR : class
        {
            var result = await GetValueByKeyAsync(key, memberName, sourceFilePath, sourceLineNumber)
                .ConfigureAwait(false);

            return string.IsNullOrEmpty(result) ? null : result.FromJsonString<TR>();
        }

        public Task<List<string>> GetValueListByKeyAsync(
            string key,
            [CallerMemberName] string memberName = "",
            [CallerFilePath] string sourceFilePath = "",
            [CallerLineNumber] int sourceLineNumber = 0)
        {
            return RunInAuditScope(
                key,
                memberName,
                sourceFilePath,
                sourceLineNumber,
                async (connection, redisKey) =>
                {
                    var array = await redisExecutor
                        .GetValueListByKeyAsync(connection, redisKey)
                        .ConfigureAwait(false);

                    return array?.ToList();
                }
            );
        }

        public Task<List<string>> GetValueListByKeyListAsync(
            IReadOnlyCollection<string> keyCollection,
            [CallerMemberName] string memberName = "",
            [CallerFilePath] string sourceFilePath = "",
            [CallerLineNumber] int sourceLineNumber = 0)
        {
            return RunInAuditScope(
                keyCollection,
                memberName,
                sourceFilePath,
                sourceLineNumber,
                async (connection, keys) =>
                {
                    var array = await redisExecutor
                        .GetValueListByKeyListAsync(connection, keys)
                        .ConfigureAwait(false);

                    return array?.ToList();
                }
            );
        }

        public async Task<List<TR>> GetValueListByKeyListAsync<TR>(
            IReadOnlyCollection<string> keyCollection,
            [CallerMemberName] string memberName = "",
            [CallerFilePath] string sourceFilePath = "",
            [CallerLineNumber] int sourceLineNumber = 0) where TR : class
        {
            var jsonValueList = await GetValueListByKeyListAsync(keyCollection, memberName, sourceFilePath, sourceLineNumber)
                .ConfigureAwait(false);

            return jsonValueList?
                .Select(x => string.IsNullOrEmpty(x) ? default(TR) : x.FromJsonString<TR>())
                .ToList();
        }

        public Task<long> IncrNumberValueForKeyAsync(
            string key,
            [CallerMemberName] string memberName = "",
            [CallerFilePath] string sourceFilePath = "",
            [CallerLineNumber] int sourceLineNumber = 0)
        {
            return RunInAuditScope(
                key,
                memberName,
                sourceFilePath,
                sourceLineNumber,
                redisExecutor.IncrNumberValueForKeyAsync);
        }

        public bool IsAvailable(
            [CallerMemberName] string memberName = "",
            [CallerFilePath] string sourceFilePath = "",
            [CallerLineNumber] int sourceLineNumber = 0)
        {
            return true;
        }

        public Task<TimeSpan?> KeyTimeToLiveAsync(string key,
            [CallerMemberName] string memberName = "",
            [CallerFilePath] string sourceFilePath = "",
            [CallerLineNumber] int sourceLineNumber = 0)
        {
            return RunInAuditScope(key, memberName, sourceFilePath, sourceLineNumber, redisExecutor.KeyTimeToLiveAsync);
        }

        public Task<bool> LpushAndLtrimInListForKeyAsync(
            string key, 
            string value, 
            int trimSize,
            [CallerMemberName] string memberName = "",
            [CallerFilePath] string sourceFilePath = "",
            [CallerLineNumber] int sourceLineNumber = 0)
        {
            return RunInAuditScope(
                key,
                new { Value = value, TrimSize = trimSize },
                memberName,
                sourceFilePath,
                sourceLineNumber,
                static (executor, connection, redisKey, parameters) => executor
                    .LpushAndLtrimInListForKeyAsync(connection, redisKey, parameters.Value, parameters.TrimSize));
        }

        public Task<string> PopValueFromListForKeyAsync(
            string key,
            [CallerMemberName] string memberName = "",
            [CallerFilePath] string sourceFilePath = "",
            [CallerLineNumber] int sourceLineNumber = 0)
        {
            return RunInAuditScope(
                key,
                memberName,
                sourceFilePath,
                sourceLineNumber,
                redisExecutor.PopValueFromListForKeyAsync);
        }

        public Task<long> PushValueToListForKeyAsync(
            string key, 
            string value,
            [CallerMemberName] string memberName = "",
            [CallerFilePath] string sourceFilePath = "",
            [CallerLineNumber] int sourceLineNumber = 0)
        {
            return RunInAuditScope(
                key,
                value,
                memberName,
                sourceFilePath,
                sourceLineNumber,
                redisExecutor.PushValueToListForKeyAsync);
        }

        private readonly struct SetValueParameters
        {
            public SetValueParameters(string value, bool keepTtl, TimeSpan? expiry)
            {
                Value = value;
                KeepTtl = keepTtl;
                Expiry = expiry;
            }

            public string Value { get; }
            public bool KeepTtl { get; }
            public TimeSpan? Expiry { get; }
        }

        public Task<bool> SetValueForKeyAsync(
            string key, 
            string value, 
            TimeSpan? expiry = null,
            bool keepTtl = false,
            [CallerMemberName] string memberName = "",
            [CallerFilePath] string sourceFilePath = "",
            [CallerLineNumber] int sourceLineNumber = 0)
        {
            return RunInAuditScope(
                key,
                new SetValueParameters(value, keepTtl, expiry),
                memberName,
                sourceFilePath,
                sourceLineNumber,
                static (executor, connection, redisKey, parameters) => executor
                    .SetValueForKeyAsync(connection, redisKey, parameters.Value, parameters.Expiry, parameters.KeepTtl));
        }

        public Task<bool> SetValueForKeyAsync<T>(
            string key, 
            T value, 
            TimeSpan? expiry = null,
            bool keepTtl = false,
            [CallerMemberName] string memberName = "",
            [CallerFilePath] string sourceFilePath = "",
            [CallerLineNumber] int sourceLineNumber = 0) where T : class
        {
            var jsonValue = value.ToJsonString();

            return SetValueForKeyAsync(key, jsonValue, expiry, keepTtl, memberName, sourceFilePath, sourceLineNumber);
        }

        public Task<long> SetValueListForKeyAsync(
            string key, 
            IReadOnlyCollection<string> valueCollection,
            [CallerMemberName] string memberName = "",
            [CallerFilePath] string sourceFilePath = "",
            [CallerLineNumber] int sourceLineNumber = 0)
        {
            return RunInAuditScope(
                key,
                valueCollection,
                memberName,
                sourceFilePath,
                sourceLineNumber,
                redisExecutor.SetValueListForKeyAsync);
        }

        public Task SubscribeHandlerAsync(
            string channel, 
            Action<string, string> handler)
        {
            var cnn = GetConnection();
            
            return redisExecutor.SubscribeHandlerAsync(cnn, channel, handler);
        }

        public Task<long> SubscribePublishAsync(
            string channel, 
            string message)
        {
            var cnn = GetConnection();
            
            return redisExecutor.SubscribePublishAsync(cnn, channel, message);
        }

        public Task<bool> SortedSetAddAsync(
            string key,
            double score,
            string member,
            [CallerMemberName] string memberName = "",
            [CallerFilePath] string sourceFilePath = "",
            [CallerLineNumber] int sourceLineNumber = 0)
        {
            return RunInAuditScope(
                key,
                new { Score = score, Member = member },
                memberName,
                sourceFilePath,
                sourceLineNumber,
                static (executor, connection, realKey, parameters) => executor
                    .SortedSetAddAsync(connection, realKey, parameters.Score, parameters.Member));
        }

        public Task<string[]> SortedSetRangeByScoreAsync(
            string key,
            double minScore,
            double maxScore = double.PositiveInfinity,
            [CallerMemberName] string memberName = "",
            [CallerFilePath] string sourceFilePath = "",
            [CallerLineNumber] int sourceLineNumber = 0)
        {
            return RunInAuditScope(
                key,
                new { MinScore = minScore, MaxScore = maxScore },
                memberName,
                sourceFilePath,
                sourceLineNumber,
                static (executor, connection, realKey, parameters) => executor
                    .SortedSetRangeByScoreAsync(connection, realKey, parameters.MinScore, parameters.MaxScore));
        }

        public Task<long> SortedSetRemoveRangeByScoreAsync(
            string key,
            double minScore,
            double maxScore,
            [CallerMemberName] string memberName = "",
            [CallerFilePath] string sourceFilePath = "",
            [CallerLineNumber] int sourceLineNumber = 0)
        {
            return RunInAuditScope(
                key,
                new { MinScore = minScore, MaxScore = maxScore },
                memberName,
                sourceFilePath,
                sourceLineNumber,
                static (executor, connection, realKey, parameters) => executor
                    .SortedSetRemoveRangeByScoreAsync(connection, realKey, parameters.MinScore, parameters.MaxScore));
        }

        public Task<long> SortedSetLengthAsync(
            string key,
            [CallerMemberName] string memberName = "",
            [CallerFilePath] string sourceFilePath = "",
            [CallerLineNumber] int sourceLineNumber = 0)
        {
            return RunInAuditScope(
                key,
                memberName,
                sourceFilePath,
                sourceLineNumber,
                redisExecutor.SortedSetLengthAsync);
        }

        public Task<long> SortedSetRemoveRangeByRankAsync(
            string key,
            long start,
            long stop,
            [CallerMemberName] string memberName = "",
            [CallerFilePath] string sourceFilePath = "",
            [CallerLineNumber] int sourceLineNumber = 0)
        {
            return RunInAuditScope(
                key,
                new { Start = start, Stop = stop },
                memberName,
                sourceFilePath,
                sourceLineNumber,
                static (executor, connection, realKey, parameters) => executor
                    .SortedSetRemoveRangeByRankAsync(connection, realKey, parameters.Start, parameters.Stop));
        }

        public Task<bool> SortedSetRemoveAsync(
            string key,
            string member,
            [CallerMemberName] string memberName = "",
            [CallerFilePath] string sourceFilePath = "",
            [CallerLineNumber] int sourceLineNumber = 0)
        {
            return RunInAuditScope(
                key,
                member,
                memberName,
                sourceFilePath,
                sourceLineNumber,
                redisExecutor.SortedSetRemoveAsync);
        }

        public Task<double?> SortedSetScoreAsync(
            string key,
            string member,
            [CallerMemberName] string memberName = "",
            [CallerFilePath] string sourceFilePath = "",
            [CallerLineNumber] int sourceLineNumber = 0)
        {
            return RunInAuditScope(
                key,
                member,
                memberName,
                sourceFilePath,
                sourceLineNumber,
                redisExecutor.SortedSetScoreAsync);
        }

        public IRedisBatch CreateBatch(string auditSpanName,
            [CallerMemberName] string memberName = "",
            [CallerFilePath] string sourceFilePath = "",
            [CallerLineNumber] int sourceLineNumber = 0)
        {
            var connection = GetConnection();
            var spanBuilder = auditTracer
                .BuildSpan(AuditSpanType.RedisDbQuery, auditSpanName)
                .WithConnection(connection)
                .TagCodeSourcePath(memberName, sourceFilePath, sourceLineNumber);

            var batch = redisExecutor.CreateBatch(connection);

            return new MoedeloRedisBatch(batch, spanBuilder, GetKey);
        }

        private string KeyPrefix => needToUseKeyPrefixSetting.GetBoolValueOrDefault(false)
            ? $"{keyPrefixSetting.GetStringValueOrDefault(DefaultKeyPrefix)}:"
            : string.Empty;

        private string GetKey(string key)
        {
            return $"{KeyPrefix}{key}";
        }

    }
}
