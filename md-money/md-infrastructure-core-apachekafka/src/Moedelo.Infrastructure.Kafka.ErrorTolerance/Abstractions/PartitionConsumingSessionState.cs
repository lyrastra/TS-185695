namespace Moedelo.Infrastructure.Kafka.ErrorTolerance.Abstractions;

public readonly record struct PartitionConsumingSessionState(
    int ConsumedMessagesCount,
    long? CommittedOffset);
