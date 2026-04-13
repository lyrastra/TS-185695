using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Infrastructure.Kafka.Abstractions;
using Moedelo.Infrastructure.Kafka.Abstractions.Interfaces;

namespace Moedelo.Infrastructure.Kafka;

[InjectAsSingleton(typeof(IKafkaClusterMetaDataReader))]
internal sealed class KafkaClusterMetaDataReader : IKafkaClusterMetaDataReader
{
    private readonly IAdminClientPool adminClientPool;

    public KafkaClusterMetaDataReader(IAdminClientPool adminClientPool)
    {
        this.adminClientPool = adminClientPool;
    }

    public Task<IReadOnlyDictionary<string, int>> GetTopicPartitionCountsAsync(string brokerEndpoints)
    {
        var adminClient = adminClientPool.GetAdminClient(brokerEndpoints);

        var meta = adminClient.GetMetadata(TimeSpan.FromSeconds(5));

        return Task.FromResult<IReadOnlyDictionary<string, int>>(meta.Topics
            .ToDictionary(topic => topic.Topic, topic => topic.Partitions.Count));
    }
}
