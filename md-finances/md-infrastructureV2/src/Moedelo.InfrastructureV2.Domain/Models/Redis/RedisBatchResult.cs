using System.Threading.Tasks;

namespace Moedelo.InfrastructureV2.Domain.Models.Redis;

/// <summary>
/// Результат batch операции Redis без возвращаемого значения.
/// Используется для отложенного выполнения операций в рамках batch.
/// </summary>
public readonly struct RedisBatchResult
{
    private Task Task { get; init; }

    /// <summary>
    /// Проверяет, завершена ли операция
    /// </summary>
    public bool IsCompleted => Task.IsCompleted;

    /// <summary>
    /// Создаёт результат batch операции из Task
    /// </summary>
    public static RedisBatchResult From(Task task) => new() { Task = task };

    /// <summary>
    /// Создаёт результат batch операции из Task&lt;T&gt; (с автоматическим выводом типа)
    /// </summary>
    public static RedisBatchResult<T> From<T>(Task<T> task) => RedisBatchResult<T>.From(task);
}

/// <summary>
/// Результат batch операции Redis с возвращаемым значением типа T.
/// Используется для отложенного выполнения операций в рамках batch.
/// </summary>
/// <typeparam name="T">Тип возвращаемого значения</typeparam>
public readonly struct RedisBatchResult<T>
{
    private Task<T> Task { get; init; }

    /// <summary>
    /// Получает значение результата операции.
    /// ВНИМАНИЕ: может заблокировать поток, если ExecuteAsync() ещё не завершён.
    /// </summary>
    public T Value => Task.Result;

    /// <summary>
    /// Проверяет, завершена ли операция
    /// </summary>
    public bool IsCompleted => Task.IsCompleted;

    /// <summary>
    /// Создаёт результат batch операции из Task&lt;T&gt; (с автоматическим выводом типа)
    /// </summary>
    public static RedisBatchResult<T> From(Task<T> task) => new() { Task = task };
}
