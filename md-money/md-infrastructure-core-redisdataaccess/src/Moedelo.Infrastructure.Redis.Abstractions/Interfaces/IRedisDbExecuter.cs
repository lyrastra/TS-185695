using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Moedelo.Infrastructure.Redis.Abstractions.Models;

namespace Moedelo.Infrastructure.Redis.Abstractions.Interfaces;

public interface IRedisDbExecuter
{
    bool IsAvailable(IRedisConnection connection);

    /// <summary>
    /// Получить время жизни ключа
    /// </summary>
    /// <param name="connection">подключение к redis</param>
    /// <param name="key">ключ</param>
    /// <returns>оставшееся время жизни (null - не найден)</returns>
    Task<TimeSpan?> KeyTimeToLiveAsync(IRedisConnection connection, string key);

    /// <summary>
    /// Установить значение по ключу. Значение будет закодировано в JSON.
    /// </summary>
    /// <param name="connection">подключение к redis</param>
    /// <param name="key">ключ</param>
    /// <param name="value">строковое значение</param>
    /// <param name="expiry">время жизни (null - либо бесконечность, либо сохранить текущее если <paramref name="keepTtl"/>=true</param>
    /// <param name="keepTtl">сохранить текущий TTL (Если указать true, то <paramref name="expiry"/> должен быть выставлен в null)</param>
    /// <returns>true - операция прошла успешно (false - ошибка работы с редис)</returns>
    Task<bool> SetValueForKeyAsync(
        IRedisConnection connection, 
        string key, 
        string value, 
        TimeSpan? expiry = null,
        bool keepTtl = false);

    Task<long> PushValueToListForKeyAsync(
        IRedisConnection connection, 
        string key, 
        string value);

    Task<string> PopValueFromListForKeyAsync(
        IRedisConnection connection, 
        string key);

    Task<long> SetValueListForKeyAsync(
        IRedisConnection connection, 
        string key,
        IReadOnlyCollection<string> valueCollection);

    Task<string> GetValueByKeyAsync(
        IRedisConnection connection, 
        string key);

    Task<string[]> GetValueListByKeyAsync(
        IRedisConnection connection, 
        string key);

    /// <summary>
    /// Получить индекс указанного значения <see cref="value"/> в списке, сохранённом по ключу <see cref="key"/>
    /// </summary>
    /// <param name="connection">соединение с Redis</param>
    /// <param name="key">ключ в редис</param>
    /// <param name="value">искомое значение</param>
    Task<long?> LPosAsync(
        IRedisConnection connection,
        string key,
        string value);

    string[] GetKeyListByMatch(
        IRedisConnection connection, 
        string match, 
        int count = 10);

    Task<bool> DeleteKeyAsync(
        IRedisConnection connection, 
        string key);

    /// <summary>
    /// <a href="https://redis.io/commands/getdel/">GETDEL</a>
    /// Получить значение, хранящееся по указанному ключу, и удалить его
    /// Значение должно быть строковым 
    /// </summary>
    /// <param name="connection">Соединение</param>
    /// <param name="key">ключ</param>
    /// <returns>строковое значение или null (если значение по ключу отсутствовало)</returns>
    Task<string> GetDeleteAsync(IRedisConnection connection, string key);

    Task<long> DeleteKeysAsync(
        IRedisConnection connection, 
        IReadOnlyCollection<string> keyCollection);

    Task<string[]> GetValueListByKeyListAsync(
        IRedisConnection connection, 
        IReadOnlyCollection<string> keyCollection);

    Task<long> IncrNumberValueForKeyAsync(
        IRedisConnection connection, 
        string key);

    Task<long> DecrNumberValueForKeyAsync(
        IRedisConnection connection, 
        string key);

    Task<bool> ExpireForKeyAsync(
        IRedisConnection connection, 
        string key, 
        TimeSpan expire);

    Task<bool> LpushAndLtrimInListForKeyAsync(
        IRedisConnection connection, 
        string key, 
        string value, 
        int trimSize);

    Task<bool> DeleteValueInListForKeyAsync(
        IRedisConnection connection, 
        string key, 
        string value);

    Task<Dictionary<string, string>> GetDictionaryByKeyAsync(
        IRedisConnection connection, 
        string key);

    Task SetDictionaryForKeyAsync(
        IRedisConnection connection, 
        string key,
        IReadOnlyCollection<KeyValuePair<string, string>> dictionary, 
        TimeSpan? expiry = null);

    Task<string> GetFieldValueFromDictionaryByKeyAsync(
        IRedisConnection connection, 
        string key, 
        string field);

    Task SetFieldValueToDictionaryForKeyAsync(
        IRedisConnection connection, 
        string key,
        KeyValuePair<string, string> fieldValuePair);

    Task SetFieldsValuesToDictionaryForKeyAsync(
        IRedisConnection connection, 
        string key,
        IReadOnlyCollection<KeyValuePair<string, string>> fieldValuePairCollection);

    Task DeleteFieldInDictionaryForKey(
        IRedisConnection connection,
        string key,
        string field);
        
    Task DeleteFieldsInDictionaryForKey(
        IRedisConnection connection,
        string key,
        IReadOnlyCollection<string> fieldCollection);

    /// <summary>
    /// добавить значение в набор уникальных значений
    /// </summary>
    /// <param name="connection"></param>
    /// <param name="key">ключ-идентификатор набора уникальных значений</param>
    /// <param name="setValue">значение, добавляемое в набор</param>
    Task SetAddAsync(
        IRedisConnection connection,
        string key,
        string setValue);

    /// <summary>
    /// удалить значение из набор уникальных значений
    /// </summary>
    /// <param name="connection"></param>
    /// <param name="key">ключ-идентификатор набора уникальных значений</param>
    /// <param name="setValue">значение, удаляемое из набора</param>
    Task SetDeleteAsync(
        IRedisConnection connection,
        string key,
        string setValue);

    /// <summary>
    /// получить все значения из набора уникальных значений
    /// </summary>
    /// <param name="connection"></param>
    /// <param name="setKey"></param>
    /// <typeparam name="T"></typeparam>
    Task<HashSet<string>> GetSetAllAsync(
        IRedisConnection connection,
        string setKey);

    Task<bool> DistributedLockRunAsync(
        IRedisConnection connection, 
        string keyLock, 
        Action method,
        DistributedLockSettings settings);

    Task<bool> DistributedLockRunAsync(
        IRedisConnection connection, 
        string keyLock, 
        Func<Task> method,
        DistributedLockSettings settings = null);

    Task<DistributedLockRunResult> DistributedLockRunAsync(
        IRedisConnection connection, 
        string keyLock, 
        Func<Task> method,
        DistributedLockSettings settings,
        CancellationToken cancellationToken);

    Task<bool> DistributedLockRunAsync(
        IRedisConnection connection, 
        string keyLock, 
        Func<CancellationToken, Task> method,
        DistributedLockSettings settings = null);

    /// <summary>
    /// Блокировка на чтение
    /// Реализует сценарий, когда несколько процессов читают данные из ресурса, а один записывает.
    /// Метод блокирует выполнение пока происходит запись в разделяемый ресурс (в Redis присутствует ключ keyLock)
    /// Блокировка на запись накладывается/снимается установкой/удалением ключа keyLock в Redis
    /// посредством метода SetValueForKeyAsync/DeleteKeyAsync
    /// </summary>
    /// <param name="connection">Соединение</param>
    /// <param name="keyLock">Ключ блокировки</param>
    /// <param name="settings">настройки блокировки (таймаут, количество попыток)</param>
    /// <returns>true, если блокировка отсутствует, false - завершение по таймауту</returns>
    Task<bool> DistributedReadLockAsync(
        IRedisConnection connection, 
        string keyLock, 
        DistributedLockSettings settings = null);

    /// <summary>
    /// Исполнение метода, возвращающего данные, в глобальной блокировке
    /// </summary>
    /// <param name="connection">Соединение</param>
    /// <param name="keyLock">ключ - наличие в бд ключа блокировка уже взята</param>
    /// <param name="method"></param>
    /// <param name="settings"></param>
    /// <returns>T - если метод выполнился, Default(T) - если метод не выполнился</returns>
    Task<(bool success, T result)> DistributedLockRunAsync<T>(
        IRedisConnection connection, 
        string keyLock,
        Func<Task<T>> method, 
        DistributedLockSettings settings = null);

    /// <summary>
    /// Исполнение метода, возвращающего данные, в глобальной блокировке
    /// </summary>
    /// <param name="connection">Соединение</param>
    /// <param name="keyLock">ключ - наличие в бд ключа блокировка уже взята</param>
    /// <param name="method"></param>
    /// <param name="settings"></param>
    /// <param name="cancellationToken">токен отмены операции</param>
    /// <returns>T - если метод выполнился, Default(T) - если метод не выполнился</returns>
    Task<(bool success, T result)> DistributedLockRunAsync<T>(
        IRedisConnection connection, 
        string keyLock,
        Func<CancellationToken, Task<T>> method, 
        DistributedLockSettings settings,
        CancellationToken cancellationToken);

    Task<bool> ExistsAsync(
        IRedisConnection connection, 
        string key);
        
    Task<long> SubscribePublishAsync(
        IRedisConnection connection, 
        string channel, 
        string message);

    Task SubscribeHandlerAsync(
        IRedisConnection connection, 
        string channel, 
        Action<string, string> handler);

    /// <summary>
    /// Добавить элемент в sorted set с указанным score
    /// </summary>
    /// <param name="connection">Соединение</param>
    /// <param name="key">ключ sorted set</param>
    /// <param name="score">числовая оценка элемента</param>
    /// <param name="member">добавляемое значение</param>
    /// <returns>true если элемент был добавлен, false если обновлен</returns>
    Task<bool> SortedSetAddAsync(
        IRedisConnection connection,
        string key,
        double score,
        string member);

    /// <summary>
    /// Получить элементы из sorted set с score в указанном диапазоне
    /// </summary>
    /// <param name="connection">Соединение</param>
    /// <param name="key">ключ sorted set</param>
    /// <param name="minScore">минимальный score (включительно)</param>
    /// <param name="maxScore">максимальный score (включительно)</param>
    /// <returns>массив значений, отсортированных по score</returns>
    Task<string[]> SortedSetRangeByScoreAsync(
        IRedisConnection connection,
        string key,
        double minScore,
        double maxScore = double.PositiveInfinity);

    /// <summary>
    /// Удалить элементы из sorted set с score в указанном диапазоне
    /// </summary>
    /// <param name="connection">Соединение</param>
    /// <param name="key">ключ sorted set</param>
    /// <param name="minScore">минимальный score (включительно)</param>
    /// <param name="maxScore">максимальный score (включительно)</param>
    /// <returns>количество удаленных элементов</returns>
    Task<long> SortedSetRemoveRangeByScoreAsync(
        IRedisConnection connection,
        string key,
        double minScore,
        double maxScore);

    /// <summary>
    /// Получить количество элементов в sorted set
    /// </summary>
    /// <param name="connection">Соединение</param>
    /// <param name="key">ключ sorted set</param>
    /// <returns>количество элементов</returns>
    Task<long> SortedSetLengthAsync(
        IRedisConnection connection,
        string key);

    /// <summary>
    /// Удалить элементы из sorted set по рангу (индексу)
    /// </summary>
    /// <param name="connection">Соединение</param>
    /// <param name="key">ключ sorted set</param>
    /// <param name="start">начальный индекс (0-based, поддерживает отрицательные значения)</param>
    /// <param name="stop">конечный индекс (включительно, поддерживает отрицательные значения)</param>
    /// <returns>количество удаленных элементов</returns>
    Task<long> SortedSetRemoveRangeByRankAsync(
        IRedisConnection connection,
        string key,
        long start,
        long stop);

    /// <summary>
    /// Удалить конкретный элемент из sorted set
    /// </summary>
    /// <param name="connection">Соединение</param>
    /// <param name="key">ключ sorted set</param>
    /// <param name="member">удаляемое значение</param>
    /// <returns>true если элемент был удален</returns>
    Task<bool> SortedSetRemoveAsync(
        IRedisConnection connection,
        string key,
        string member);

    /// <summary>
    /// Получить score элемента в sorted set
    /// </summary>
    /// <param name="connection">Соединение</param>
    /// <param name="key">ключ sorted set</param>
    /// <param name="member">значение</param>
    /// <returns>score элемента или null если элемент не найден</returns>
    Task<double?> SortedSetScoreAsync(
        IRedisConnection connection,
        string key,
        string member);

    /// <summary>
    /// Создать batch для группировки нескольких Redis команд в один round-trip к серверу.
    /// Все команды добавляются в batch, затем отправляются на сервер одновременно при вызове ExecuteAsync().
    /// Это повышает производительность за счет сокращения числа сетевых запросов.
    /// </summary>
    /// <param name="connection">Соединение</param>
    /// <returns>Batch для добавления команд</returns>
    IRedisBatch CreateBatch(IRedisConnection connection);
}