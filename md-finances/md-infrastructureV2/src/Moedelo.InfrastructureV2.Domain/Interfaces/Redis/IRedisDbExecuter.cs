using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using Moedelo.InfrastructureV2.Domain.Models.Redis;
// ReSharper disable InvalidXmlDocComment

namespace Moedelo.InfrastructureV2.Domain.Interfaces.Redis;

public interface IRedisDbExecuter
{
    bool IsAvailable(
        [CallerMemberName] string memberName = "",
        [CallerFilePath] string sourceFilePath = "",
        [CallerLineNumber] int sourceLineNumber = 0);
    
    /// <summary>
    /// Получить время жизни ключа
    /// </summary>
    /// <param name="key">ключ</param>
    /// <returns>оставшееся время жизни (null - не найден)</returns>
    Task<TimeSpan?> KeyTimeToLiveAsync(string key,
        [CallerMemberName] string memberName = "",
        [CallerFilePath] string sourceFilePath = "",
        [CallerLineNumber] int sourceLineNumber = 0);

    /// <summary>
    /// Установить значение по ключу
    /// </summary>
    /// <param name="key">ключ</param>
    /// <param name="value">строковое значение</param>
    /// <param name="expiry">время жизни (null - либо бесконечность, либо сохранить текущее если <paramref name="keepTtl"/>=true</param>
    /// <param name="keepTtl">сохранить текущий TTL (Если указать true, то <paramref name="expiry"/> должен быть выставлен в null)</param>
    /// <returns>true - операция прошла успешно (false - ошибка работы с редис)</returns>
    Task<bool> SetValueForKeyAsync(
        string key, 
        string value, 
        TimeSpan? expiry = null,
        bool keepTtl = false,
        [CallerMemberName] string memberName = "",
        [CallerFilePath] string sourceFilePath = "",
        [CallerLineNumber] int sourceLineNumber = 0);

    /// <summary>
    /// Установить значение по ключу. Значение будет закодировано в JSON.
    /// </summary>
    /// <param name="key">ключ</param>
    /// <param name="value">строковое значение</param>
    /// <param name="expiry">время жизни (null - либо бесконечность, либо сохранить текущее если <paramref name="keepTtl"/>=true</param>
    /// <param name="keepTtl">сохранить текущий TTL (Если указать true, то <paramref name="expiry"/> должен быть выставлен в null)</param>
    /// <returns>true - операция прошла успешно (false - ошибка работы с редис)</returns>
    Task<bool> SetValueForKeyAsync<T>(
        string key, 
        T value, 
        TimeSpan? expiry = null,
        bool keepTtl = false,
        [CallerMemberName] string memberName = "",
        [CallerFilePath] string sourceFilePath = "",
        [CallerLineNumber] int sourceLineNumber = 0) where T : class;

    Task<long> PushValueToListForKeyAsync(
        string key, 
        string value,
        [CallerMemberName] string memberName = "",
        [CallerFilePath] string sourceFilePath = "",
        [CallerLineNumber] int sourceLineNumber = 0);

    Task<string> PopValueFromListForKeyAsync(
        string key,
        [CallerMemberName] string memberName = "",
        [CallerFilePath] string sourceFilePath = "",
        [CallerLineNumber] int sourceLineNumber = 0);

    Task<long> SetValueListForKeyAsync(
        string key, 
        IReadOnlyCollection<string> valueCollection,
        [CallerMemberName] string memberName = "",
        [CallerFilePath] string sourceFilePath = "",
        [CallerLineNumber] int sourceLineNumber = 0);

    Task<string> GetValueByKeyAsync(
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
    /// <returns></returns>
    Task<long?> GetValueIndexInListAsync(string key,
        string value,
        [CallerMemberName] string memberName = "",
        [CallerFilePath] string sourceFilePath = "",
        [CallerLineNumber] int sourceLineNumber = 0);

    Task<TR> GetValueByKeyAsync<TR>(
        string key,
        [CallerMemberName] string memberName = "",
        [CallerFilePath] string sourceFilePath = "",
        [CallerLineNumber] int sourceLineNumber = 0) where TR : class;

    Task<List<string>> GetValueListByKeyAsync(
        string key,
        [CallerMemberName] string memberName = "",
        [CallerFilePath] string sourceFilePath = "",
        [CallerLineNumber] int sourceLineNumber = 0);

    List<string> GetKeyListByMatch(
        string match, 
        int count = 10,
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
    Task<string> GetAndDeleteAsync(string key,
        [CallerMemberName] string memberName = "",
        [CallerFilePath] string sourceFilePath = "",
        [CallerLineNumber] int sourceLineNumber = 0);

    Task<bool> DeleteKeyAsync(
        string key,
        [CallerMemberName] string memberName = "",
        [CallerFilePath] string sourceFilePath = "",
        [CallerLineNumber] int sourceLineNumber = 0);
        
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

    Task<List<string>> GetValueListByKeyListAsync(
        IReadOnlyCollection<string> keyCollection,
        [CallerMemberName] string memberName = "",
        [CallerFilePath] string sourceFilePath = "",
        [CallerLineNumber] int sourceLineNumber = 0);

    Task<List<TR>> GetValueListByKeyListAsync<TR>(
        IReadOnlyCollection<string> keyCollection,
        [CallerMemberName] string memberName = "",
        [CallerFilePath] string sourceFilePath = "",
        [CallerLineNumber] int sourceLineNumber = 0) where TR: class;

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

    Task<bool> DeleteValueInListForKeyAsync(
        string key, 
        string value,
        [CallerMemberName] string memberName = "",
        [CallerFilePath] string sourceFilePath = "",
        [CallerLineNumber] int sourceLineNumber = 0);

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

    /// <summary>
    /// Установить словарь значений по ключу.
    /// ВНИМАНИЕ: словарь не может содержать пустых значений
    /// </summary>
    /// <param name="key">ключ</param>
    /// <param name="dictionary">словарь</param>
    /// <param name="expiry">время жизни</param>
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
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    Task AddValueToSetAsync<T>(
        string key,
        T value,
        [CallerMemberName] string memberName = "",
        [CallerFilePath] string sourceFilePath = "",
        [CallerLineNumber] int sourceLineNumber = 0);

    /// <summary>
    /// добавить значение в набор уникальных значений
    /// </summary>
    /// <param name="key">ключ-идентификатор набора уникальных значений</param>
    /// <param name="value">добавляемое значение</param>
    /// <typeparam name="T"></typeparam>
    Task AddValueToSetAsync(
        string key,
        string value,
        [CallerMemberName] string memberName = "",
        [CallerFilePath] string sourceFilePath = "",
        [CallerLineNumber] int sourceLineNumber = 0);

    /// <summary>
    /// удалить значение из списка уникальных значений
    /// </summary>
    /// <param name="key">ключ-идентификатор набора уникальных значений</param>
    /// <param name="value">удаляемое значение</param>
    /// <typeparam name="T"></typeparam>
    Task DeleteValueFromSetAsync<T>(
        string key,
        T value,
        [CallerMemberName] string memberName = "",
        [CallerFilePath] string sourceFilePath = "",
        [CallerLineNumber] int sourceLineNumber = 0);

    /// <summary>
    /// удалить строковое значение из списка уникальных строк
    /// </summary>
    /// <param name="key">ключ-идентификатор набора уникальных значений</param>
    /// <param name="value">удаляемое значение</param>
    Task DeleteValueFromSetAsync(
        string key,
        string value,
        [CallerMemberName] string memberName = "",
        [CallerFilePath] string sourceFilePath = "",
        [CallerLineNumber] int sourceLineNumber = 0);

    /// <summary>
    /// получить все строковые значения из набора уникальных значений
    /// </summary>
    /// <param name="key"></param>
    /// <param name="memberName"></param>
    /// <param name="sourceFilePath"></param>
    /// <param name="sourceLineNumber"></param>
    /// <returns>список значений</returns>
    Task<HashSet<string>> GetAllValuesOfSetAsync(
        string key,
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
    /// <returns>список значений</returns>
    Task<HashSet<T>> GetAllValuesOfSetAsync<T>(
        string key,
        [CallerMemberName] string memberName = "",
        [CallerFilePath] string sourceFilePath = "",
        [CallerLineNumber] int sourceLineNumber = 0);

    /// <summary>
    /// Исполнение метода в глобальной блокировке.
    /// </summary>
    /// <param name="keyLock">ключ - наличие в бд ключа блокировка уже взята</param>
    /// <param name="method"></param>
    /// <param name="queryObject"></param>
    /// <returns>true - если метод выполнился</returns>
    Task<bool> DistributedLockRunAsync(
        string keyLock, 
        Action method, 
        RedisQueryObject queryObject,
        [CallerMemberName] string memberName = "",
        [CallerFilePath] string sourceFilePath = "",
        [CallerLineNumber] int sourceLineNumber = 0);

    /// <summary>
    /// Исполнение метода в глобальной блокировке
    /// </summary>
    /// <param name="keyLock">ключ - наличие в бд ключа блокировка уже взята</param>
    /// <param name="method"></param>
    /// <param name="queryObject"></param>
    /// <returns>true - если метод выполнился</returns>
    Task<bool> DistributedLockRunAsync(
        string keyLock, 
        Func<Task> method, 
        RedisQueryObject queryObject = null,
        [CallerMemberName] string memberName = "",
        [CallerFilePath] string sourceFilePath = "",
        [CallerLineNumber] int sourceLineNumber = 0);

    /// <summary>
    /// Исполнение метода в глобальной блокировке
    /// (с возможностью отслеживания таймаута блокировки)
    /// Метод захватывает блокировку по имени ключа keyLock согласно настройкам, указанным в queryObject,
    /// и передаёт управление в method
    /// В случае, если время выполнения method превышает queryObject.Expiry, token будет "отозван"
    /// это позволяет method обработать данную ситуацию в случае необходимости
    /// </summary>
    /// <param name="keyLock">имя ключа блокировки</param>
    /// <param name="method">метод, принимающий CancellationToken, "протухающий" по таймауту блокировки</param>
    /// <param name="queryObject"></param>
    /// <returns>true, если блокировка была успешно захвачена и method вызван,
    ///         false - не удалось захватить блокировку (с учётом ограничений, заданных в queryObject)</returns>
    Task<bool> DistributedLockRunAsync(
        string keyLock,
        Func<CancellationToken, Task> method,
        RedisQueryObject queryObject,
        [CallerMemberName] string memberName = "",
        [CallerFilePath] string sourceFilePath = "",
        [CallerLineNumber] int sourceLineNumber = 0);

    /// <summary>
    /// Блокировка на чтение
    /// Реализует сценарий, когда несколько процессов читают данные из ресурса, а один записывает.
    /// Метод блокирует выполнение пока происходит запись в разделяемый ресурс (в Redis присутствует ключ keyLock)
    /// Блокировка на запись накладывается/снимается установкой/удалением ключа keyLock в Redis
    /// посредством метода SetValueForKeyAsync/DeleteKeyAsync
    /// </summary>
    /// <param name="keyLock">Ключ блокировки</param>
    /// <param name="queryObject">настройки блокировки (таймаут, количество попыток)</param>
    /// <returns>true, если блокировка отсутствует, false - завершение по таймауту</returns>
    Task<bool> DistributedReadLockAsync(
        string keyLock, 
        RedisQueryObject queryObject = null,
        [CallerMemberName] string memberName = "",
        [CallerFilePath] string sourceFilePath = "",
        [CallerLineNumber] int sourceLineNumber = 0);

    /// <summary>
    /// Исполнение метода, возвращающего данные, в глобальной блокировке
    /// </summary>
    /// <param name="keyLock">ключ - наличие в бд ключа блокировка уже взята</param>
    /// <param name="method"></param>
    /// <param name="queryObject"></param>
    /// <returns>T - если метод выполнился, Default(T) - если метод не выполнился</returns>
    Task<T> DistributedLockRunAsync<T>(
        string keyLock, 
        Func<Task<T>> method, 
        RedisQueryObject queryObject = null,
        [CallerMemberName] string memberName = "",
        [CallerFilePath] string sourceFilePath = "",
        [CallerLineNumber] int sourceLineNumber = 0);

    /// <summary>
    /// Исполнение метода, возвращающего данные, в глобальной блокировке
    /// </summary>
    /// <param name="keyLock">ключ - наличие в бд ключа блокировка уже взята</param>
    /// <param name="method"></param>
    /// <param name="queryObject"></param>
    /// <param name="cancellationToken">токен отмены операции</param>
    /// <param name="memberName"></param>
    /// <param name="sourceFilePath"></param>
    /// <param name="sourceLineNumber"></param>
    /// <returns>T - если метод выполнился, Default(T) - если метод не выполнился</returns>
    Task<T> DistributedLockRunAsync<T>(
        string keyLock, 
        Func<CancellationToken, Task<T>> method, 
        RedisQueryObject queryObject,
        CancellationToken cancellationToken,
        [CallerMemberName] string memberName = "",
        [CallerFilePath] string sourceFilePath = "",
        [CallerLineNumber] int sourceLineNumber = 0);


    Task<bool> ExistsAsync(
        string key,
        [CallerMemberName] string memberName = "",
        [CallerFilePath] string sourceFilePath = "",
        [CallerLineNumber] int sourceLineNumber = 0);

    Task<long> SubscribePublishAsync(string channel, string message);

    Task SubscribeHandlerAsync(string channel, Action<string, string> handler);

    /// <summary>
    /// Добавить элемент в sorted set с указанным score
    /// </summary>
    /// <param name="key">ключ sorted set</param>
    /// <param name="score">числовая оценка элемента</param>
    /// <param name="member">добавляемое значение</param>
    /// <param name="memberName"></param>
    /// <param name="sourceFilePath"></param>
    /// <param name="sourceLineNumber"></param>
    /// <returns>true если элемент был добавлен, false если обновлен</returns>
    Task<bool> SortedSetAddAsync(
        string key,
        double score,
        string member,
        [CallerMemberName] string memberName = "",
        [CallerFilePath] string sourceFilePath = "",
        [CallerLineNumber] int sourceLineNumber = 0);

    /// <summary>
    /// Получить элементы из sorted set с score в указанном диапазоне
    /// </summary>
    /// <param name="key">ключ sorted set</param>
    /// <param name="minScore">минимальный score (включительно)</param>
    /// <param name="maxScore">максимальный score (включительно)</param>
    /// <param name="memberName"></param>
    /// <param name="sourceFilePath"></param>
    /// <param name="sourceLineNumber"></param>
    /// <returns>массив значений, отсортированных по score</returns>
    Task<string[]> SortedSetRangeByScoreAsync(
        string key,
        double minScore,
        double maxScore = double.PositiveInfinity,
        [CallerMemberName] string memberName = "",
        [CallerFilePath] string sourceFilePath = "",
        [CallerLineNumber] int sourceLineNumber = 0);

    /// <summary>
    /// Удалить элементы из sorted set с score в указанном диапазоне
    /// </summary>
    /// <param name="key">ключ sorted set</param>
    /// <param name="minScore">минимальный score (включительно)</param>
    /// <param name="maxScore">максимальный score (включительно)</param>
    /// <param name="memberName"></param>
    /// <param name="sourceFilePath"></param>
    /// <param name="sourceLineNumber"></param>
    /// <returns>количество удаленных элементов</returns>
    Task<long> SortedSetRemoveRangeByScoreAsync(
        string key,
        double minScore,
        double maxScore,
        [CallerMemberName] string memberName = "",
        [CallerFilePath] string sourceFilePath = "",
        [CallerLineNumber] int sourceLineNumber = 0);

    /// <summary>
    /// Получить количество элементов в sorted set
    /// </summary>
    /// <param name="key">ключ sorted set</param>
    /// <param name="memberName"></param>
    /// <param name="sourceFilePath"></param>
    /// <param name="sourceLineNumber"></param>
    /// <returns>количество элементов</returns>
    Task<long> SortedSetLengthAsync(
        string key,
        [CallerMemberName] string memberName = "",
        [CallerFilePath] string sourceFilePath = "",
        [CallerLineNumber] int sourceLineNumber = 0);

    /// <summary>
    /// Удалить элементы из sorted set по рангу (индексу)
    /// </summary>
    /// <param name="key">ключ sorted set</param>
    /// <param name="start">начальный индекс (0-based, поддерживает отрицательные значения)</param>
    /// <param name="stop">конечный индекс (включительно, поддерживает отрицательные значения)</param>
    /// <param name="memberName"></param>
    /// <param name="sourceFilePath"></param>
    /// <param name="sourceLineNumber"></param>
    /// <returns>количество удаленных элементов</returns>
    Task<long> SortedSetRemoveRangeByRankAsync(
        string key,
        long start,
        long stop,
        [CallerMemberName] string memberName = "",
        [CallerFilePath] string sourceFilePath = "",
        [CallerLineNumber] int sourceLineNumber = 0);

    /// <summary>
    /// Удалить конкретный элемент из sorted set
    /// </summary>
    /// <param name="key">ключ sorted set</param>
    /// <param name="member">удаляемое значение</param>
    /// <param name="memberName"></param>
    /// <param name="sourceFilePath"></param>
    /// <param name="sourceLineNumber"></param>
    /// <returns>true если элемент был удален</returns>
    Task<bool> SortedSetRemoveAsync(
        string key,
        string member,
        [CallerMemberName] string memberName = "",
        [CallerFilePath] string sourceFilePath = "",
        [CallerLineNumber] int sourceLineNumber = 0);

    /// <summary>
    /// Получить score элемента в sorted set
    /// </summary>
    /// <param name="key">ключ sorted set</param>
    /// <param name="member">значение</param>
    /// <param name="memberName"></param>
    /// <param name="sourceFilePath"></param>
    /// <param name="sourceLineNumber"></param>
    /// <returns>score элемента или null если элемент не найден</returns>
    Task<double?> SortedSetScoreAsync(
        string key,
        string member,
        [CallerMemberName] string memberName = "",
        [CallerFilePath] string sourceFilePath = "",
        [CallerLineNumber] int sourceLineNumber = 0);

    /// <summary>
    /// Создать batch для группировки нескольких Redis команд в один round-trip к серверу.
    /// Все команды добавляются в batch, затем отправляются на сервер одновременно при вызове ExecuteAsync().
    /// Это повышает производительность за счет сокращения числа сетевых запросов.
    /// </summary>
    /// <param name="auditSpanName">Уникальное название спана в auditTrail</param>
    /// <returns></returns>
    IRedisBatch CreateBatch(string auditSpanName,
        [CallerMemberName] string memberName = "",
        [CallerFilePath] string sourceFilePath = "",
        [CallerLineNumber] int sourceLineNumber = 0);
}