using System.Collections.Concurrent;
using Moedelo.Infrastructure.Kafka.Abstractions.Models;
using Moedelo.Infrastructure.Kafka.ErrorTolerance.Abstractions;
using Moedelo.Infrastructure.Kafka.ErrorTolerance.Exceptions;
using Moedelo.Infrastructure.Kafka.ErrorTolerance.Extensions;

namespace Moedelo.Infrastructure.Kafka.ErrorTolerance;

internal sealed class LazyStateLoadingMemoryWrapper
{
    private readonly SemaphoreSlim mutex;
    private readonly IKafkaConsumerMessageMemoryRepository repository;
    private readonly IConsumingStateMemory memory;
    private readonly ConcurrentDictionary<KafkaTopicPartition, KafkaTopicPartition> assignedPartitions = new ();

    public LazyStateLoadingMemoryWrapper(
        IKafkaConsumerMessageMemoryRepository repository,
        IConsumingStateMemory memory)
    {
        mutex = new(1,1);
        this.repository = repository;
        this.memory = memory;
    }

    public async ValueTask<IConsumingStateMemory> GetMemoryAsync(CancellationToken cancellationToken)
    {
        if (assignedPartitions.Any())
        {
            using (await mutex.CaptureAsync(cancellationToken))
            {
                foreach (var (_, partition) in assignedPartitions)
                {
                    var state = await repository
                        .GetOrCreateAsync(memory.ConsumerGroupId, partition.Topic, partition.Partition,
                            cancellationToken);
        
                    UnableGetOrCreateConsumerStateException.ThrowIfNull(state, partition, repository.GetType());
                    InvalidConsumingStateException.ThrowIfNotMatched(state, partition);
        
                    memory.Assigned(state);
                }

                assignedPartitions.Clear();
            }
        }

        return memory;
    }

    public void AssignPartition(KafkaTopicPartition partition)
    {
        using (mutex.Capture())
        {
            assignedPartitions[partition] = partition;
        }
    }

    public void RevokePartition(KafkaTopicPartitionOffset partitionOffset)
    {
        using (mutex.Capture())
        {
            var partition = new KafkaTopicPartition(partitionOffset.Topic, partitionOffset.Partition);
            assignedPartitions.Remove(partition, out _);
            memory.Revoked(partitionOffset);
        }
    }
}
