using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Moedelo.Common.Kafka.Abstractions;
using Moedelo.Common.Kafka.Abstractions.Base;
using Moedelo.Common.Kafka.Abstractions.Entities.Commands;
using Moedelo.Common.Kafka.Abstractions.Settings;
using Moedelo.Common.Settings.Abstractions;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Infrastructure.Kafka.Abstractions.Interfaces;

namespace Moedelo.Common.Kafka.Entities.Commands;

[InjectAsSingleton(typeof(IMoedeloEntityCommandKafkaTopicReader))]
internal sealed class MoedeloEntityCommandKafkaTopicReader : MoedeloKafkaTopicReaderInternalBase,
    IMoedeloEntityCommandKafkaTopicReader
{
    public MoedeloEntityCommandKafkaTopicReader(
        IKafkaConsumerStarter consumerStarter,
        ISettingRepository settingRepository,
        IKafkaTopicNameResolver topicNameResolver,
        IKafkaConsumerBalancer consumerBalancer,
        ILogger<MoedeloEntityCommandKafkaTopicReader> logger)
        : base(
            consumerStarter,
            settingRepository,
            topicNameResolver,
            consumerBalancer,
            logger)
    {
    }

    public Task ReadFromTopicAsync(ReadTopicSetting<MoedeloEntityCommandKafkaMessageValue> settings, CancellationToken cancellationToken)
    {
        return ReadTopicImpl(settings, settings.OnMessage, cancellationToken);
    }
}