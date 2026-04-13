using System;

namespace Moedelo.Infrastructure.Kafka.Abstractions.Models;

public interface IConsumerLifecycleEvent
{
    string GroupId { get; }
    string ConsumerGuid { get; }
    string Topic { get; }
};

public readonly record struct ConsumerStartedEvent(string GroupId, string ConsumerGuid, string Topic)
    : IConsumerLifecycleEvent;

public readonly record struct ConsumerStoppedEvent(string GroupId, string ConsumerGuid, string Topic)
    : IConsumerLifecycleEvent;


public sealed class ConsumerPartitionSetOnPauseEvent : IConsumerLifecycleEvent
{
    public string GroupId { get; init; } = null!;
    public string ConsumerGuid { get; init; } = null!;
    public string Topic { get; init; } = null!;
    public int Partition { get; init; }
    public long Offset { get; init; }
    public Exception? Exception { get; init; }
}
