namespace Moedelo.Infrastructure.Kafka.ErrorTolerance;

/// <summary>
/// Оценка состояния памяти секции
/// (приходится делать public, чтобы можно было мокать в юнит-тестах)
/// </summary>
public readonly record struct PartitionMemoryEstimation(
    bool IsReadOnly,
    string ReadOnlyReason);
