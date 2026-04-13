#nullable enable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using Moedelo.Common.Audit.Abstractions.Interfaces;
using Moedelo.Common.Audit.Abstractions.Models;
using Moedelo.Common.Redis.Abstractions.Extensions;
using Moedelo.Common.Settings.Abstractions;
using Moedelo.Common.Settings.Abstractions.Models;
using Moedelo.Infrastructure.Json;
using Moedelo.Infrastructure.Redis.Abstractions.Interfaces;
using Moedelo.Infrastructure.Redis.Abstractions.Models;
// ReSharper disable ExplicitCallerInfoArgument
// ReSharper disable InvalidXmlDocComment

namespace Moedelo.Common.Redis.Abstractions;

public abstract partial class MoedeloRedisDbExecutorBase : IMoedeloRedisDbExecutorBase
{
    private static readonly string DefaultKeyPrefix = Environment.MachineName;

    private readonly string typeName;
    private readonly IRedisDbExecuter redisExecutor;
    private readonly IAuditTracer auditTracer;

    private readonly SettingValue connectionStringSetting;
    private readonly IntSettingValue dbNumberSetting;
    private readonly SettingValue needToUseKeyPrefixSetting;
    private readonly SettingValue keyPrefixSetting;

    protected MoedeloRedisDbExecutorBase(
        IRedisDbExecuter redisExecutor,
        ISettingRepository settingRepository,
        SettingValue connectionStringSetting,
        SettingValue dbNumberSetting,
        IAuditTracer auditTracer) :
        this(redisExecutor, settingRepository, connectionStringSetting, new IntSettingValue(dbNumberSetting), auditTracer)
    {
    }

    protected MoedeloRedisDbExecutorBase(
        IRedisDbExecuter redisExecutor,
        ISettingRepository settingRepository,
        SettingValue connectionStringSetting,
        IntSettingValue dbNumberSetting,
        IAuditTracer auditTracer)
    {
        typeName = GetType().Name;
        this.auditTracer = auditTracer;

        this.redisExecutor = redisExecutor;
        this.connectionStringSetting = connectionStringSetting;
        this.dbNumberSetting = dbNumberSetting;
        this.needToUseKeyPrefixSetting = settingRepository.GetRedisNeedToUseKeyPrefixSetting();
        this.keyPrefixSetting = settingRepository.GetRedisKeyPrefixSetting();
    }

    public bool IsAvailable(
        [CallerMemberName] string memberName = "",
        [CallerFilePath] string sourceFilePath = "",
        [CallerLineNumber] int sourceLineNumber = 0)
    {
        return true;
    }

    private readonly record struct StringValue(string Value);
    private readonly record struct StructWrap<TValue>(TValue Value);
    private readonly record struct SetValueForKeyParams(string Value, TimeSpan? Expiry);
    private static StructWrap<TValue> WrapAsParameters<TValue>(TValue value) => new StructWrap<TValue>(value);
    private static StringValue WrapString(string value) => new (value);

    public Task<bool> SetValueForKeyAsync(
        string key,
        string value,
        TimeSpan? expiry = null,
        [CallerMemberName] string memberName = "",
        [CallerFilePath] string sourceFilePath = "",
        [CallerLineNumber] int sourceLineNumber = 0)
    {
        return RunInAuditScope(
            key,
            new SetValueForKeyParams(value, expiry),
            memberName,
            sourceFilePath,
            sourceLineNumber,
            static (executor, connection, redisKey, parameters) => executor
                .SetValueForKeyAsync(connection, redisKey, parameters.Value, parameters.Expiry));
    }

    public Task<bool> SetValueForKeyAsync<T>(
        string key,
        T value,
        TimeSpan? expiry = null,
        [CallerMemberName] string memberName = "",
        [CallerFilePath] string sourceFilePath = "",
        [CallerLineNumber] int sourceLineNumber = 0) where T : class
    {
        var jsonValue = value.ToJsonString();

        return SetValueForKeyAsync(key, jsonValue, expiry, memberName, sourceFilePath, sourceLineNumber);
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
            WrapString(value),
            memberName,
            sourceFilePath,
            sourceLineNumber,
            static (executor, connection, redisKey, parameters) => executor
                .PushValueToListForKeyAsync(connection, redisKey, parameters.Value));
    }

    public Task<long> PushValueToListForKeyAsync<T>(
        string key,
        T value,
        [CallerMemberName] string memberName = "",
        [CallerFilePath] string sourceFilePath = "",
        [CallerLineNumber] int sourceLineNumber = 0) where T : class
    {
        var jsonValue = value.ToJsonString();

        return PushValueToListForKeyAsync(key, jsonValue, memberName, sourceFilePath, sourceLineNumber);
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
            static (executor, connection, redisKey) => executor.PopValueFromListForKeyAsync(connection, redisKey));
    }

    public async Task<TR?> PopValueFromListForKeyAsync<TR>(
        string key,
        [CallerMemberName] string memberName = "",
        [CallerFilePath] string sourceFilePath = "",
        [CallerLineNumber] int sourceLineNumber = 0) where TR : class
    {
        var result = await PopValueFromListForKeyAsync(key, memberName, sourceFilePath, sourceLineNumber)
            .ConfigureAwait(false);

        return string.IsNullOrEmpty(result) ? null : result.FromJsonString<TR>();
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
            WrapAsParameters(valueCollection),
            memberName,
            sourceFilePath,
            sourceLineNumber,
            static (executor, connection, redisKey, parameters) => executor
                .SetValueListForKeyAsync(connection, redisKey, parameters.Value));
    }

    public Task<long> SetValueListForKeyAsync<T>(
        string key,
        IReadOnlyCollection<T> valueCollection,
        [CallerMemberName] string memberName = "",
        [CallerFilePath] string sourceFilePath = "",
        [CallerLineNumber] int sourceLineNumber = 0) where T : class
    {
        // ReSharper disable once ConditionalAccessQualifierIsNonNullableAccordingToAPIContract
        var jsonValueList = valueCollection?.Select(value => value.ToJsonString()).ToArray()
                            ?? throw new ArgumentNullException(nameof(valueCollection));

        return SetValueListForKeyAsync(key, jsonValueList, memberName, sourceFilePath, sourceLineNumber);
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
            static (executor, connection, fullKey) => executor.GetValueByKeyAsync(connection, fullKey));
    }

    public async Task<TR?> GetValueByKeyAsync<TR>(
        string key,
        [CallerMemberName] string memberName = "",
        [CallerFilePath] string sourceFilePath = "",
        [CallerLineNumber] int sourceLineNumber = 0) where TR : class
    {
        var result = await GetValueByKeyAsync(key, memberName, sourceFilePath, sourceLineNumber)
            .ConfigureAwait(false);

        return string.IsNullOrEmpty(result) ? null : result.FromJsonString<TR>();
    }

    public Task<string[]> GetValueListByKeyAsync(
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
            static (executor, connection, redisKey) => executor.GetValueListByKeyAsync(connection, redisKey)
        );
    }

    public Task<long?> GetValueIndexInListAsync(string key,
        string value,
        [CallerMemberName] string memberName = "",
        [CallerFilePath] string sourceFilePath = "",
        [CallerLineNumber] int sourceLineNumber = 0)
    {
        return RunInAuditScope(
            key,
            WrapString(value),
            memberName,
            sourceFilePath,
            sourceLineNumber,
            static (executor, connection, redisKey, parameters) => executor
                .LPosAsync(connection, redisKey, parameters.Value));
    }

    public async Task<TR?[]> GetValueListByKeyAsync<TR>(
        string key,
        [CallerMemberName] string memberName = "",
        [CallerFilePath] string sourceFilePath = "",
        [CallerLineNumber] int sourceLineNumber = 0) where TR : class
    {
        var jsonValueList = await GetValueListByKeyAsync(key, memberName, sourceFilePath, sourceLineNumber)
            .ConfigureAwait(false);

        return jsonValueList?
            .Select(json => string.IsNullOrEmpty(json) ? default(TR) : json.FromJsonString<TR>())
            .ToArray() ?? Array.Empty<TR>();
    }

    public string[] GetKeyListByMatch(
        string match,
        int count = 10,
        [CallerMemberName] string memberName = "",
        [CallerFilePath] string sourceFilePath = "",
        [CallerLineNumber] int sourceLineNumber = 0)
    {
        var cnn = GetConnection();
        var keyPrefix = KeyPrefix;
        var realMatch = GetKey(match);

        using var scope = GetAuditSpanBuilder(memberName, sourceFilePath, sourceLineNumber)
            .WithConnection(cnn)
            .WithParams(new
            {
                Match = realMatch,
            })
            .Start();
        try
        {
            var keys = redisExecutor.GetKeyListByMatch(cnn, realMatch, count);
                
            return string.IsNullOrEmpty(keyPrefix)
                ? keys
                : keys.Select(key => key.Substring(keyPrefix.Length)).ToArray();
        }
        catch (Exception ex)
        {
            scope.Span.SetError(ex);
            throw;
        }
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
            static (executor, connection, redisKey) => executor.DeleteKeyAsync(connection, redisKey));
    }

    /// <summary>
    /// <a href="https://redis.io/commands/getdel/">GETDEL</a>
    /// Получить значение, хранящееся по указанному ключу, и удалить его
    /// Значение должно быть строковым 
    /// </summary>
    /// <param name="key">ключ</param>
    /// <returns>строковое значение или null (если значение по ключу отсутствовало)</returns>
    public Task<string> GetDeleteAsync(
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
            static (executor, connection, redisKey) => executor.GetDeleteAsync(connection, redisKey));
    }

    /// <summary>
    /// <a href="https://redis.io/commands/getdel/">GETDEL</a>
    /// Получить значение, хранящееся по указанному ключу, и удалить его
    /// </summary>
    /// <param name="key">ключ</param>
    /// <returns>строковое значение или null (если значение по ключу отсутствовало)</returns>
    public Task<TR> GetDeleteAsync<TR>(
        string key,
        [CallerMemberName] string memberName = "",
        [CallerFilePath] string sourceFilePath = "",
        [CallerLineNumber] int sourceLineNumber = 0) where TR : class
    {
        return RunInAuditScope(
            key,
            memberName,
            sourceFilePath,
            sourceLineNumber,
            static (executor, connection, redisKey) =>
                executor.GetDeleteAsync<TR>(connection, redisKey));
    }

    public Task<long> DeleteKeysAsync(
        IReadOnlyCollection<string>? keyCollection,
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
            static (executor, connection, redisKeys) => executor.DeleteKeysAsync(connection, redisKeys));
    }

    public async Task DeleteKeysMatchedAsync(
        string match,
        [CallerMemberName] string memberName = "",
        [CallerFilePath] string sourceFilePath = "",
        [CallerLineNumber] int sourceLineNumber = 0)
    {
        var cnn = GetConnection();
        var realMatch = GetKey(match);

        using var scope = GetAuditSpanBuilder(memberName, sourceFilePath, sourceLineNumber)
            .WithConnection(cnn)
            .WithParams(new
            {
                Match = realMatch,
            })
            .Start();
        try
        {
            const int maxKeysPerOnce = 100;
            string[] keys;
                    
            while ((keys = redisExecutor.GetKeyListByMatch(cnn, realMatch, maxKeysPerOnce)).Length > 0)
            {
                await redisExecutor.DeleteKeysAsync(cnn, keys).ConfigureAwait(false);
            }
        }
        catch (Exception ex)
        {
            scope.Span.SetError(ex);
            throw;
        }
    }

    public Task<string[]> GetValueListByKeyListAsync(
        IReadOnlyCollection<string>? keyCollection,
        [CallerMemberName] string memberName = "",
        [CallerFilePath] string sourceFilePath = "",
        [CallerLineNumber] int sourceLineNumber = 0)
    {
        if (keyCollection is not { Count: > 0 })
        {
            return Task.FromResult(Array.Empty<string>());
        }

        return RunInAuditScope(
            keyCollection,
            memberName,
            sourceFilePath,
            sourceLineNumber,
            static (executor, connection, redisKeys) => executor
                .GetValueListByKeyListAsync(connection, redisKeys));
    }

    public async Task<TR?[]> GetValueListByKeyListAsync<TR>(
        IReadOnlyCollection<string>? keyCollection,
        [CallerMemberName] string memberName = "",
        [CallerFilePath] string sourceFilePath = "",
        [CallerLineNumber] int sourceLineNumber = 0) where TR : class
    {
        var jsonValueList = await GetValueListByKeyListAsync(keyCollection).ConfigureAwait(false);

        return jsonValueList?.Select(json => string.IsNullOrEmpty(json) ? null : json.FromJsonString<TR>())
            .ToArray() ?? Array.Empty<TR>();
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
            static (executor, connection, redisKey) => executor
                .IncrNumberValueForKeyAsync(connection, redisKey));
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
            static (executor, connection, redisKey) => executor
                .DecrNumberValueForKeyAsync(connection, redisKey));
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
            WrapAsParameters(expire),
            memberName,
            sourceFilePath,
            sourceLineNumber,
            static (executor, connection, redisKey, parameters) => executor
                .ExpireForKeyAsync(connection, redisKey, parameters.Value));
    }

    private readonly record struct LpushAndLtrimInListForKey(string Value, int TrimSize);
        
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
            new LpushAndLtrimInListForKey(value, trimSize),
            memberName,
            sourceFilePath,
            sourceLineNumber,
            static (executor, connection, redisKey, parameters) => executor
                .LpushAndLtrimInListForKeyAsync(connection, redisKey, parameters.Value, parameters.TrimSize));
    }

    public Task<bool> LpushAndLtrimInListForKeyAsync<T>(
        string key,
        T value,
        int trimSize,
        [CallerMemberName] string memberName = "",
        [CallerFilePath] string sourceFilePath = "",
        [CallerLineNumber] int sourceLineNumber = 0) where T : class
    {
        var jsonValue = value.ToJsonString();

        return LpushAndLtrimInListForKeyAsync(key, jsonValue, trimSize, memberName, sourceFilePath, sourceLineNumber);
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
            new StringValue(value),
            memberName,
            sourceFilePath,
            sourceLineNumber,
            static (executor, connection, redisKey, parameters) => executor
                .DeleteValueInListForKeyAsync(connection, redisKey, parameters.Value));
    }

    public Task<bool> DeleteValueInListForKeyAsync<T>(
        string key,
        T value,
        [CallerMemberName] string memberName = "",
        [CallerFilePath] string sourceFilePath = "",
        [CallerLineNumber] int sourceLineNumber = 0) where T : class
    {
        var jsonValue = value.ToJsonString();

        return DeleteValueInListForKeyAsync(key, jsonValue, memberName, sourceFilePath, sourceLineNumber);
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
            static (executor, connection, redisKey) => executor
                .GetDictionaryByKeyAsync(connection, redisKey));
    }

    public async Task<Dictionary<string, T?>?> GetDictionaryByKeyAsync<T>(
        string key,
        [CallerMemberName] string memberName = "",
        [CallerFilePath] string sourceFilePath = "",
        [CallerLineNumber] int sourceLineNumber = 0) where T : class
    {
        var result = await GetDictionaryByKeyAsync(key, memberName, sourceFilePath, sourceLineNumber)
            .ConfigureAwait(false);

        return result?.ToDictionary(
            kv => kv.Key,
            kv => kv.Value?.FromJsonString<T>());
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
            WrapAsParameters(dictionary),
            memberName,
            sourceFilePath,
            sourceLineNumber,
            static (executor, connection, redisKey, parameters) => executor
                .SetDictionaryForKeyAsync(connection, redisKey, parameters.Value));
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
            WrapString(field),
            memberName,
            sourceFilePath,
            sourceLineNumber,
            static (executor, connection, redisKey, parameters) => executor
                .GetFieldValueFromDictionaryByKeyAsync(connection, redisKey, parameters.Value));
    }

    public async Task<T?> GetFieldValueFromDictionaryByKeyAsync<T>(
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
            fieldValuePair,
            memberName,
            sourceFilePath,
            sourceLineNumber,
            static (executor, connection, redisKey, parameters) => executor
                .SetFieldValueToDictionaryForKeyAsync(connection, redisKey, parameters));

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
            WrapAsParameters(fieldValuePairCollection),
            memberName,
            sourceFilePath,
            sourceLineNumber,
            static (executor, connection, redisKey, parameters) => executor
                .SetFieldsValuesToDictionaryForKeyAsync(connection, redisKey, parameters.Value));
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
            fieldValuePairCollection
                .Select(kv => new KeyValuePair<string,string>(kv.Key, kv.Value.ToJsonString()))
                .ToArray(),
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
            WrapString(field),
            memberName,
            sourceFilePath,
            sourceLineNumber,
            static (executor, connection, redisKey, parameters) => executor
                .DeleteFieldInDictionaryForKey(connection, redisKey, parameters.Value));
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
            WrapAsParameters(fieldCollection),
            memberName,
            sourceFilePath,
            sourceLineNumber,
            (executor,connection, redisKey, parameters) => executor
                .DeleteFieldsInDictionaryForKey(connection, redisKey, parameters.Value));
    }

    public Task AddValueToSetAsync<T>(
        string key,
        T value,
        [CallerMemberName] string memberName = "",
        [CallerFilePath] string sourceFilePath = "",
        [CallerLineNumber] int sourceLineNumber = 0)
    {
        return RunInAuditScope(
            key,
            WrapString(value.ToJsonString()),
            memberName,
            sourceFilePath,
            sourceLineNumber,
            static (executor, connection, redisKey, parameters) => executor
                .SetAddAsync(connection, redisKey, parameters.Value));
    }

    public Task DeleteValueFromSetAsync<T>(
        string key,
        T value,
        [CallerMemberName] string memberName = "",
        [CallerFilePath] string sourceFilePath = "",
        [CallerLineNumber] int sourceLineNumber = 0)
    {
        return RunInAuditScope(
            key,
            WrapString(value.ToJsonString()),
            memberName,
            sourceFilePath,
            sourceLineNumber,
            static (executor, connection, redisKey, parameters) => executor
                .SetDeleteAsync(connection, redisKey, parameters.Value));
    }

    public Task<HashSet<T>> GetAllValuesOfSetAsync<T>(string key,
        [CallerMemberName] string memberName = "",
        [CallerFilePath] string sourceFilePath = "",
        [CallerLineNumber] int sourceLineNumber = 0)
    {
        return RunInAuditScope(
            key,
            memberName,
            sourceFilePath,
            sourceLineNumber,
            static (executor, connection, redisKey) => executor
                .GetSetAllAsync<T>(connection, redisKey));
    }

    public async Task<bool> DistributedLockRunAsync(
        string keyLock,
        Action method,
        DistributedLockSettings? settings = null,
        [CallerMemberName] string memberName = "",
        [CallerFilePath] string sourceFilePath = "",
        [CallerLineNumber] int sourceLineNumber = 0)
    {
        var cnn = GetConnection();
        var realKeyLock = GetKey(keyLock);

        using var scope = GetAuditSpanBuilder(memberName, sourceFilePath, sourceLineNumber, AuditSpanType.RedisDistributedLock)
            .WithConnection(cnn)
            .WithDistributedLock(realKeyLock, settings)
            .Start();
        try
        {
            var result = await redisExecutor.DistributedLockRunAsync(cnn, realKeyLock, method, settings)
                .ConfigureAwait(false);

            return result;
        }
        catch (Exception ex)
        {
            scope.Span.SetError(ex);
            throw;
        }
    }

    public async Task<bool> DistributedLockRunAsync(
        string keyLock,
        Func<Task> method,
        DistributedLockSettings? settings = null,
        [CallerMemberName] string memberName = "",
        [CallerFilePath] string sourceFilePath = "",
        [CallerLineNumber] int sourceLineNumber = 0)
    {
        var cnn = GetConnection();
        var realKeyLock = GetKey(keyLock);

        using var scope = GetAuditSpanBuilder(memberName, sourceFilePath, sourceLineNumber, AuditSpanType.RedisDistributedLock)
            .WithConnection(cnn)
            .WithDistributedLock(realKeyLock, settings)
            .Start();
        try
        {
            var result = await redisExecutor.DistributedLockRunAsync(cnn, realKeyLock, method, settings)
                .ConfigureAwait(false);

            return result;
        }
        catch (Exception ex)
        {
            scope.Span.SetError(ex);
            throw;
        }
    }

    public async Task<bool> DistributedLockRunAsync(
        string keyLock,
        Func<Task> method,
        DistributedLockSettings? settings,
        string auditSpanName,
        CancellationToken cancellationToken,
        [CallerMemberName] string memberName = "",
        [CallerFilePath] string sourceFilePath = "",
        [CallerLineNumber] int sourceLineNumber = 0)
    {
        var cnn = GetConnection();
        var realKeyLock = GetKey(keyLock);

        var spanName = GetSpanName(auditSpanName, memberName, sourceFilePath, sourceLineNumber);
        using var scope = auditTracer.BuildSpan(AuditSpanType.RedisDistributedLock, spanName)
            .WithConnection(cnn)
            .WithDistributedLock(realKeyLock, settings)
            .Start();

        try
        {
            var result = await redisExecutor
                .DistributedLockRunAsync(cnn, realKeyLock, method, settings, cancellationToken)
                .ConfigureAwait(false);
            scope.Span.AddTag("Result", result);

            return result.Success;
        }
        catch (Exception ex)
        {
            scope.Span.SetError(ex);
            throw;
        }
    }

    public async Task<(bool success, T result)> DistributedLockRunAsync<T>(
        string keyLock,
        Func<Task<T>> method,
        DistributedLockSettings? settings = null,
        [CallerMemberName] string memberName = "",
        [CallerFilePath] string sourceFilePath = "",
        [CallerLineNumber] int sourceLineNumber = 0)
    {
        var cnn = GetConnection();
        var realKeyLock = GetKey(keyLock);

        using var scope = GetAuditSpanBuilder(memberName, sourceFilePath, sourceLineNumber, AuditSpanType.RedisDistributedLock)
            .WithConnection(cnn)
            .WithDistributedLock(realKeyLock, settings)
            .Start();
        try
        {
            var result = await redisExecutor.DistributedLockRunAsync(cnn, realKeyLock, method, settings)
                .ConfigureAwait(false);

            return result;
        }
        catch (Exception ex)
        {
            scope.Span.SetError(ex);
            throw;
        }
    }

    public async Task<bool> ExistsAsync(
        string key,
        [CallerMemberName] string memberName = "",
        [CallerFilePath] string sourceFilePath = "",
        [CallerLineNumber] int sourceLineNumber = 0)
    {
        var cnn = GetConnection();
        var realKey = GetKey(key);

        using var scope = GetAuditSpanBuilder(memberName, sourceFilePath, sourceLineNumber)
            .WithConnection(cnn)
            .WithKey(realKey)
            .Start();
        try
        {
            var result = await redisExecutor.ExistsAsync(cnn, realKey).ConfigureAwait(false);

            return result;
        }
        catch (Exception ex)
        {
            scope.Span.SetError(ex);
            throw;
        }
    }

    private RedisConnection GetConnection()
    {
        var connectionString = connectionStringSetting.Value;
        var dbNumber = dbNumberSetting.Value;

        return new RedisConnection(connectionString, dbNumber);
    }

    private string KeyPrefix => needToUseKeyPrefixSetting.GetBoolValueOrDefault(false)
        ? $"{keyPrefixSetting.GetStringValueOrDefault(DefaultKeyPrefix)}:"
        : string.Empty;

    private string GetKey(string key)
    {
        return $"{KeyPrefix}{key}";
    }

    private IAuditSpanBuilder GetAuditSpanBuilder(
        string memberName,
        string sourceFilePath,
        int sourceLineNumber,
        AuditSpanType spanType = AuditSpanType.RedisDbQuery)
    {
        var spanName = $"func {memberName} from {sourceFilePath} file at {sourceLineNumber} line";
        var utcNow = DateTime.UtcNow;

        return auditTracer.BuildSpan(spanType, spanName).WithStartDateUtc(utcNow);
    }

    private static string GetSpanName(string name,
        string memberName,
        string sourceFilePath,
        int sourceLineNumber)
    {
        if (string.IsNullOrEmpty(name) == false)
        {
            return name;
        }

        return $"func {memberName} from {sourceFilePath}:{sourceLineNumber}";
    }
}