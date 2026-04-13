using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Moedelo.Common.Kafka.Abstractions;
using Moedelo.Common.Kafka.Abstractions.Base;
using Moedelo.Common.Kafka.Abstractions.Entities.Events;
using Moedelo.Common.Kafka.Abstractions.Settings;
using Moedelo.Common.Settings.Abstractions;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Infrastructure.Kafka.Abstractions.Interfaces;

namespace Moedelo.Common.Kafka.Entities.Events;

[InjectAsSingleton(typeof(IMoedeloEntityEventKafkaTopicReader))]
internal sealed class MoedeloEntityEventKafkaTopicReader : MoedeloKafkaTopicReaderInternalBase, IMoedeloEntityEventKafkaTopicReader
{
    public MoedeloEntityEventKafkaTopicReader(
        IKafkaConsumerStarter consumerStarter,
        ISettingRepository settingRepository,
        IKafkaTopicNameResolver topicNameResolver,
        IKafkaConsumerBalancer consumerBalancer,
        ILogger<MoedeloEntityEventKafkaTopicReader> logger)
        : base(
            consumerStarter,
            settingRepository,
            topicNameResolver,
            consumerBalancer,
            logger)
    {
    }

    public Task ReadFromTopicAsync(ReadTopicSetting<MoedeloEntityEventKafkaMessageValue> settings, CancellationToken cancellationToken)
    {
        return ReadTopicImpl(settings, settings.OnMessage, cancellationToken);
    }
}