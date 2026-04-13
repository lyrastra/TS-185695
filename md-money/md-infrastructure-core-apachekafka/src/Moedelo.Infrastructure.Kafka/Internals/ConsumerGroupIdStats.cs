using System.Collections.Concurrent;
using Moedelo.Infrastructure.Kafka.Abstractions.Models;

namespace Moedelo.Infrastructure.Kafka.Internals;

internal static class ConsumerGroupIdStats
{
    private static readonly ConcurrentDictionary<string, bool> UniqueGroupIds = new();
    
    internal static void RegisterGroupId(KafkaConsumerGroupId groupId)
    {
        UniqueGroupIds[groupId.Raw] = true;
    }

    internal static bool DoesApplicationHaveMultipleGroups => UniqueGroupIds.Count > 1;
}

