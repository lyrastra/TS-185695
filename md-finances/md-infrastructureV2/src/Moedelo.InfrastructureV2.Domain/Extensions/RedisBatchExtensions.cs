using System.Collections.Generic;
using Moedelo.InfrastructureV2.Domain.Interfaces.Redis;

namespace Moedelo.InfrastructureV2.Domain.Extensions;

/// <summary>
/// Extension методы для IRedisBatch, предоставляющие упрощенный синтаксис
/// для операций, где результат обычно не требуется.
/// </summary>
public static class RedisBatchExtensions
{
    #region Key operations

    /// <summary>
    /// Удаляет ключ (результат игнорируется).
    /// Для получения результата используйте перегрузку с out параметром.
    /// </summary>
    public static IRedisBatch KeyDelete(this IRedisBatch batch, string key)
    {
        return batch.KeyDelete(key, out _);
    }

    #endregion

    #region List operations

    /// <summary>
    /// Обрезает список до указанного диапазона (результат не возвращается).
    /// </summary>
    public static IRedisBatch ListTrim(this IRedisBatch batch, string key, long start, long stop)
    {
        return batch.ListTrim(key, start, stop, out _);
    }

    #endregion

    #region Hash operations

    /// <summary>
    /// Устанавливает несколько полей хэша (результат не возвращается).
    /// </summary>
    public static IRedisBatch HashSet(
        this IRedisBatch batch,
        string key,
        IReadOnlyCollection<KeyValuePair<string, string>> fieldValues)
    {
        return batch.HashSet(key, fieldValues, out _);
    }

    #endregion

    #region Sorted Set operations

    /// <summary>
    /// Удаляет элементы из sorted set по диапазону score (результат игнорируется).
    /// Для получения количества удаленных элементов используйте перегрузку с out параметром.
    /// </summary>
    public static IRedisBatch SortedSetRemoveRangeByScore(
        this IRedisBatch batch,
        string key,
        double minScore,
        double maxScore)
    {
        return batch.SortedSetRemoveRangeByScore(key, minScore, maxScore, out _);
    }

    /// <summary>
    /// Удаляет элементы из sorted set по диапазону рангов (результат игнорируется).
    /// Для получения количества удаленных элементов используйте перегрузку с out параметром.
    /// </summary>
    public static IRedisBatch SortedSetRemoveRangeByRank(
        this IRedisBatch batch,
        string key,
        long start,
        long stop)
    {
        return batch.SortedSetRemoveRangeByRank(key, start, stop, out _);
    }

    /// <summary>
    /// Удаляет элемент из sorted set (результат игнорируется).
    /// Для проверки был ли элемент удален используйте перегрузку с out параметром.
    /// </summary>
    public static IRedisBatch SortedSetRemove(
        this IRedisBatch batch,
        string key,
        string member)
    {
        return batch.SortedSetRemove(key, member, out _);
    }

    #endregion
}

