using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Moedelo.Common.Kafka.Abstractions;
using Moedelo.Common.Kafka.Abstractions.Extensions;
using Moedelo.Common.Settings.Abstractions;
using Moedelo.Common.Settings.Abstractions.Models;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Infrastructure.Kafka.Abstractions.Interfaces;

namespace Moedelo.Common.Kafka;

[InjectAsSingleton(typeof(IMoedeloKafkaClusterMetaDataReader))]
internal sealed class MoedeloKafkaClusterMetaDataReader : IMoedeloKafkaClusterMetaDataReader
{
    private readonly IKafkaClusterMetaDataReader metaDataReader;
    private readonly IKafkaTopicNameResolver topicNameResolver;
    private readonly SettingValue brokerEndpointsSetting;

    public MoedeloKafkaClusterMetaDataReader(
        IKafkaClusterMetaDataReader metaDataReader,
        IKafkaTopicNameResolver topicNameResolver,
        ISettingRepository settingRepository)
    {
        this.metaDataReader = metaDataReader;
        this.topicNameResolver = topicNameResolver;
        this.brokerEndpointsSetting = settingRepository.GetKafkaBrokerEndpoints();
    }

    public async Task<IReadOnlyDictionary<string, int>> GetTopicPartitionCountsAsync(bool removeEnvPrefix)
    {
        var rawMap = await metaDataReader.GetTopicPartitionCountsAsync(brokerEndpointsSetting.Value);
        var topicNamePrefix = topicNameResolver.GetTopicPrefix();

        if (removeEnvPrefix == false)
        {
            return rawMap
                .Where(kv => kv.Key.StartsWith(topicNamePrefix, StringComparison.InvariantCultureIgnoreCase))
                .ToDictionary(
                kv => kv.Key,
                kv => kv.Value);
        }

        var realNameStartIndex = topicNamePrefix.Length + 1;

        return rawMap
            .Where(kv => kv.Key.StartsWith(topicNamePrefix, StringComparison.InvariantCultureIgnoreCase))
            .ToDictionary(
                kv => kv.Key.Substring(realNameStartIndex),
                kv => kv.Value);
    }
}
