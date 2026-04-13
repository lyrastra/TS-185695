namespace Moedelo.Infrastructure.Kafka.ErrorTolerance.Abstractions;

/// <summary>
/// Оценщик состояния памяти секции
/// (приходится делать public, чтобы можно было мокать в юнит-тестах)
/// </summary>
public interface IPartitionStateEstimator
{
    PartitionMemoryEstimation EstimateMemoryStatus(
        IErrorToleratedKafkaConsumerOptions options,
        PartitionMemoryState state,
        long currentOffset);
}
