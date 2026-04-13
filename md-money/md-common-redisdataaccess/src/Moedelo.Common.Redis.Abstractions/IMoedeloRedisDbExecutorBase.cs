using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using Moedelo.Infrastructure.Redis.Abstractions.Models;
// ReSharper disable InvalidXmlDocComment

namespace Moedelo.Common.Redis.Abstractions
{
    public interface IMoedeloRedisDbExecutorBase
    {
        bool IsAvailable(
            [CallerMemberName] string memberName = "",
            [CallerFilePath] string sourceFilePath = "",
            [CallerLineNumber] int sourceLineNumber = 0);

        Task<bool> SetValueForKeyAsync(
            string key, 
            string value, 
            TimeSpan? expiry = null,
            [CallerMemberName] string memberName = "",
            [CallerFilePath] string sourceFilePath = "",
            [CallerLineNumber] int sourceLineNumber = 0);

        Task<bool> SetValueForKeyAsync<T>(
            string key, 
            T value, 
            TimeSpan? expiry = null,
            [CallerMemberName] string memberName = "",
            [CallerFilePath] string sourceFilePath = "",
            [CallerLineNumber] int sourceLineNumber = 0) where T : class;

        Task<long> PushValueToListForKeyAsync(
            string key, 
            string value,
            [CallerMemberName] string memberName = "",
            [CallerFilePath] string sourceFilePath = "",
            [CallerLineNumber] int sourceLineNumber = 0);
        
        Task<long> PushValueToListForKeyAsync<T>(
            string key, 
            T value,
            [CallerMemberName] string memberName = "",
            [CallerFilePath] string sourceFilePath = "",
            [CallerLineNumber] int sourceLineNumber = 0) where T : class;

        Task<string> PopValueFromListForKeyAsync(
            string key,
            [CallerMemberName] string memberName = "",
            [CallerFilePath] string sourceFilePath = "",
            [CallerLineNumber] int sourceLineNumber = 0);
        
        Task<TR> PopValueFromListForKeyAsync<TR>(
            string key,
            [CallerMemberName] string memberName = "",
            [CallerFilePath] string sourceFilePath = "",
            [CallerLineNumber] int sourceLineNumber = 0) where TR : class;

        Task<long> SetValueListForKeyAsync(
            string key, 
            IReadOnlyCollection<string> valueCollection,
            [CallerMemberName] string memberName = "",
            [CallerFilePath] string sourceFilePath = "",
            [CallerLineNumber] int sourceLineNumber = 0);
        
        Task<long> SetValueListForKeyAsync<T>(
            string key, 
            IReadOnlyCollection<T> valueCollection,
            [CallerMemberName] string memberName = "",
            [CallerFilePath] string sourceFilePath = "",
            [CallerLineNumber] int sourceLineNumber = 0) where T : class;

        Task<string> GetValueByKeyAsync(
            string key,
            [CallerMemberName] string memberName = "",
            [CallerFilePath] string sourceFilePath = "",
            [CallerLineNumber] int sourceLineNumber = 0);

        Task<TR> GetValueByKeyAsync<TR>(
            string key,
            [CallerMemberName] string memberName = "",
            [CallerFilePath] string sourceFilePath = "",
            [CallerLineNumber] int sourceLineNumber = 0) where TR : class;

        Task<string[]> GetValueListByKeyAsync(
            string key,
            [CallerMemberName] string memberName = "",
            [CallerFilePath] string sourceFilePath = "",
            [CallerLineNumber] int sourceLineNumber = 0);

        /// <summary>
        /// Получить индекс указанного значения <see cref="value"/> в списке, сохранённом по ключу <see cref="key"/>
        /// </summary>
        /// <param name="key">ключ в редис</param>
        /// <param name="value">искомое значение</param>
        /// <param name="memberName"></param>
        /// <param name="sourceFilePath"></param>
        /// <param name="sourceLineNumber"></param>
        Task<long?> GetValueIndexInListAsync(string key,
            string value,
            [CallerMemberName] string memberName = "",
            [CallerFilePath] string sourceFilePath = "",
            [CallerLineNumber] int sourceLineNumber = 0);
        
        Task<TR[]> GetValueListByKeyAsync<TR>(
            string key,
            [CallerMemberName] string memberName = "",
            [CallerFilePath] string sourceFilePath = "",
            [CallerLineNumber] int sourceLineNumber = 0) where TR : class;

        string[] GetKeyListByMatch(
            string match, 
            int count = 10,
            [CallerMemberName] string memberName = "",
            [CallerFilePath] string sourceFilePath = "",
            [CallerLineNumber] int sourceLineNumber = 0);

        Task<bool> DeleteKeyAsync(
            string key,
            [CallerMemberName] string memberName = "",
            [CallerFilePath] string sourceFilePath = "",
            [CallerLineNumber] int sourceLineNumber = 0);

        /// <summary>
        /// <a href="https://redis.io/commands/getdel/">GETDEL</a>
        /// Получить значение, хранящееся по указанному ключу, и удалить его
        /// Значение должно быть строковым 
        /// </summary>
        /// <param name="key">ключ</param>
        /// <returns>строковое значение или null (если значение по ключу отсутствовало)</returns>
        Task<string> GetDeleteAsync(
            string key,
            [CallerMemberName] string memberName = "",
            [CallerFilePath] string sourceFilePath = "",
            [CallerLineNumber] int sourceLineNumber = 0);

        /// <summary>
        /// <a href="https://redis.io/commands/getdel/">GETDEL</a>
        /// Получить значение, хранящееся по указанному ключу, и удалить его
        /// </summary>
        /// <param name="key">ключ</param>
        /// <returns>строковое значение или null (если значение по ключу отсутствовало)</returns>
        Task<TR> GetDeleteAsync<TR>(
            string key,
            [CallerMemberName] string memberName = "",
            [CallerFilePath] string sourceFilePath = "",
            [CallerLineNumber] int sourceLineNumber = 0) where TR : class;

        Task<long> DeleteKeysAsync(
            IReadOnlyCollection<string> keyCollection,
            [CallerMemberName] string memberName = "",
            [CallerFilePath] string sourceFilePath = "",
            [CallerLineNumber] int sourceLineNumber = 0);

        Task DeleteKeysMatchedAsync(
            string match,
            [CallerMemberName] string memberName = "",
            [CallerFilePath] string sourceFilePath = "",
            [CallerLineNumber] int sourceLineNumber = 0);

        Task<string[]> GetValueListByKeyListAsync(
            IReadOnlyCollection<string> keyCollection,
            [CallerMemberName] string memberName = "",
            [CallerFilePath] string sourceFilePath = "",
            [CallerLineNumber] int sourceLineNumber = 0);

        Task<TR[]> GetValueListByKeyListAsync<TR>(
            IReadOnlyCollection<string> keyCollection,
            [CallerMemberName] string memberName = "",
            [CallerFilePath] string sourceFilePath = "",
            [CallerLineNumber] int sourceLineNumber = 0) where TR : class;

        Task<long> IncrNumberValueForKeyAsync(
            string key,
            [CallerMemberName] string memberName = "",
            [CallerFilePath] string sourceFilePath = "",
            [CallerLineNumber] int sourceLineNumber = 0);

        Task<long> DecrNumberValueForKeyAsync(
            string key,
            [CallerMemberName] string memberName = "",
            [CallerFilePath] string sourceFilePath = "",
            [CallerLineNumber] int sourceLineNumber = 0);

        Task<bool> ExpireForKeyAsync(
            string key, 
            TimeSpan expire,
            [CallerMemberName] string memberName = "",
            [CallerFilePath] string sourceFilePath = "",
            [CallerLineNumber] int sourceLineNumber = 0);

        Task<bool> LpushAndLtrimInListForKeyAsync(
            string key, 
            string value, 
            int trimSize,
            [CallerMemberName] string memberName = "",
            [CallerFilePath] string sourceFilePath = "",
            [CallerLineNumber] int sourceLineNumber = 0);

        Task<bool> LpushAndLtrimInListForKeyAsync<T>(
            string key, 
            T value, 
            int trimSize,
            [CallerMemberName] string memberName = "",
            [CallerFilePath] string sourceFilePath = "",
            [CallerLineNumber] int sourceLineNumber = 0) where T : class;

        Task<bool> DeleteValueInListForKeyAsync(
            string key, 
            string value,
            [CallerMemberName] string memberName = "",
            [CallerFilePath] string sourceFilePath = "",
            [CallerLineNumber] int sourceLineNumber = 0);
        
        Task<bool> DeleteValueInListForKeyAsync<T>(
            string key, 
            T value,
            [CallerMemberName] string memberName = "",
            [CallerFilePath] string sourceFilePath = "",
            [CallerLineNumber] int sourceLineNumber = 0) where T : class;
        
        Task<Dictionary<string, string>> GetDictionaryByKeyAsync(
            string key,
            [CallerMemberName] string memberName = "",
            [CallerFilePath] string sourceFilePath = "",
            [CallerLineNumber] int sourceLineNumber = 0);
        
        Task<Dictionary<string, T>> GetDictionaryByKeyAsync<T>(
            string key,
            [CallerMemberName] string memberName = "",
            [CallerFilePath] string sourceFilePath = "",
            [CallerLineNumber] int sourceLineNumber = 0) where T : class;

        Task SetDictionaryForKeyAsync(
            string key,
            IReadOnlyCollection<KeyValuePair<string, string>> dictionary, 
            TimeSpan? expiry = null,
            [CallerMemberName] string memberName = "",
            [CallerFilePath] string sourceFilePath = "",
            [CallerLineNumber] int sourceLineNumber = 0);
        
        Task SetDictionaryForKeyAsync<T>(
            string key,
            IReadOnlyCollection<KeyValuePair<string, T>> dictionary, 
            TimeSpan? expiry = null,
            [CallerMemberName] string memberName = "",
            [CallerFilePath] string sourceFilePath = "",
            [CallerLineNumber] int sourceLineNumber = 0) where T : class;

        Task<string> GetFieldValueFromDictionaryByKeyAsync(
            string key, 
            string field,
            [CallerMemberName] string memberName = "",
            [CallerFilePath] string sourceFilePath = "",
            [CallerLineNumber] int sourceLineNumber = 0);
        
        Task<T> GetFieldValueFromDictionaryByKeyAsync<T>(
            string key, 
            string field,
            [CallerMemberName] string memberName = "",
            [CallerFilePath] string sourceFilePath = "",
            [CallerLineNumber] int sourceLineNumber = 0) where T : class;

        Task SetFieldValueToDictionaryForKeyAsync(
            string key,
            KeyValuePair<string, string> fieldValuePair,
            [CallerMemberName] string memberName = "",
            [CallerFilePath] string sourceFilePath = "",
            [CallerLineNumber] int sourceLineNumber = 0);
        
        Task SetFieldValueToDictionaryForKeyAsync<T>(
            string key,
            KeyValuePair<string, T> fieldValuePair,
            [CallerMemberName] string memberName = "",
            [CallerFilePath] string sourceFilePath = "",
            [CallerLineNumber] int sourceLineNumber = 0) where T : class;

        Task SetFieldsValuesToDictionaryForKeyAsync(
            string key,
            IReadOnlyCollection<KeyValuePair<string, string>> fieldValuePairCollection,
            [CallerMemberName] string memberName = "",
            [CallerFilePath] string sourceFilePath = "",
            [CallerLineNumber] int sourceLineNumber = 0);
        
        Task SetFieldsValuesToDictionaryForKeyAsync<T>(
            string key,
            IReadOnlyCollection<KeyValuePair<string, T>> fieldValuePairCollection,
            [CallerMemberName] string memberName = "",
            [CallerFilePath] string sourceFilePath = "",
            [CallerLineNumber] int sourceLineNumber = 0) where T : class;

        Task DeleteFieldInDictionaryForKey(
            string key,
            string field,
            [CallerMemberName] string memberName = "",
            [CallerFilePath] string sourceFilePath = "",
            [CallerLineNumber] int sourceLineNumber = 0);
        
        Task DeleteFieldsInDictionaryForKey(
            string key,
            IReadOnlyCollection<string> fieldCollection,
            [CallerMemberName] string memberName = "",
            [CallerFilePath] string sourceFilePath = "",
            [CallerLineNumber] int sourceLineNumber = 0);

                /// <summary>
        /// добавить значение в набор уникальных значений
        /// </summary>
        /// <param name="key">ключ-идентификатор набора уникальных значений</param>
        /// <param name="value">добавляемое значение</param>
        /// <param name="memberName"></param>
        /// <param name="sourceFilePath"></param>
        /// <param name="sourceLineNumber"></param>
        /// <typeparam name="T"></typeparam>
        Task AddValueToSetAsync<T>(
            string key,
            T value,
            [CallerMemberName] string memberName = "",
            [CallerFilePath] string sourceFilePath = "",
            [CallerLineNumber] int sourceLineNumber = 0);

        /// <summary>
        /// удалить значение из списка уникальных значений
        /// </summary>
        /// <param name="key">ключ-идентификатор набора уникальных значений</param>
        /// <param name="value">удаляемое значение</param>
        /// <param name="memberName"></param>
        /// <param name="sourceFilePath"></param>
        /// <param name="sourceLineNumber"></param>
        /// <typeparam name="T"></typeparam>
        Task DeleteValueFromSetAsync<T>(
            string key,
            T value,
            [CallerMemberName] string memberName = "",
            [CallerFilePath] string sourceFilePath = "",
            [CallerLineNumber] int sourceLineNumber = 0);

        /// <summary>
        /// получить все значения из набора уникальных значений
        /// </summary>
        /// <param name="key"></param>
        /// <param name="memberName"></param>
        /// <param name="sourceFilePath"></param>
        /// <param name="sourceLineNumber"></param>
        /// <typeparam name="T"></typeparam>
        Task<HashSet<T>> GetAllValuesOfSetAsync<T>(
            string key,
            [CallerMemberName] string memberName = "",
            [CallerFilePath] string sourceFilePath = "",
            [CallerLineNumber] int sourceLineNumber = 0);

        Task<bool> DistributedLockRunAsync(
            string keyLock, 
            Action method, 
            DistributedLockSettings settings = null,
            [CallerMemberName] string memberName = "",
            [CallerFilePath] string sourceFilePath = "",
            [CallerLineNumber] int sourceLineNumber = 0);

        Task<bool> DistributedLockRunAsync(
            string keyLock, 
            Func<Task> method, 
            DistributedLockSettings settings = null,
            [CallerMemberName] string memberName = "",
            [CallerFilePath] string sourceFilePath = "",
            [CallerLineNumber] int sourceLineNumber = 0);

        Task<bool> DistributedLockRunAsync(
            string keyLock, 
            Func<Task> method, 
            DistributedLockSettings settings,
            string auditSpanName,
            CancellationToken cancellationToken,
            [CallerMemberName] string memberName = "",
            [CallerFilePath] string sourceFilePath = "",
            [CallerLineNumber] int sourceLineNumber = 0);

        Task<(bool success, T result)> DistributedLockRunAsync<T>(
            string keyLock, 
            Func<Task<T>> method, 
            DistributedLockSettings settings = null,
            [CallerMemberName] string memberName = "",
            [CallerFilePath] string sourceFilePath = "",
            [CallerLineNumber] int sourceLineNumber = 0);

        Task<bool> ExistsAsync(
            string key,
            [CallerMemberName] string memberName = "",
            [CallerFilePath] string sourceFilePath = "",
            [CallerLineNumber] int sourceLineNumber = 0);
    }
}