using System;
using System.Threading;
using System.Threading.Tasks;
using Moedelo.Infrastructure.Kafka.Abstractions.Models;

namespace Moedelo.Infrastructure.Kafka.Abstractions.Interfaces;

public interface IKafkaConsumer : IDisposable
{
    /// <summary>
    /// Группа консьюмера как она задана при создании конмьюмера (без префикса окружения)
    /// </summary>
    KafkaConsumerGroupId ConsumerGroupId { get; }
    string ConsumerUid { get; }
    int? MaxPollIntervalMs { get; }
    bool CanConsume { get; }
    void Subscribe(string topicName);
    ValueTask<IConsumeResultWrap> ConsumeAsync(CancellationToken cancellationToken);
    ValueTask CommitAsync(IConsumeResultWrap consumeResult);
    bool IsPaused(KafkaTopicPartitionOffset topicPartition);
    ValueTask OnMessageHandlingFailedAsync(KafkaTopicPartitionOffset topicPartition, string? messageKey);
}