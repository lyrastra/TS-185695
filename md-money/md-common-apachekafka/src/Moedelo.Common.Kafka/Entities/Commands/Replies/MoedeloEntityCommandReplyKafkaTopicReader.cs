using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Moedelo.Common.Kafka.Abstractions;
using Moedelo.Common.Kafka.Abstractions.Base;
using Moedelo.Common.Kafka.Abstractions.Entities.Commands.Replies;
using Moedelo.Common.Kafka.Abstractions.Settings;
using Moedelo.Common.Settings.Abstractions;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Infrastructure.Kafka.Abstractions.Interfaces;

namespace Moedelo.Common.Kafka.Entities.Commands.Replies;

[InjectAsSingleton(typeof(IMoedeloEntityCommandReplyKafkaTopicReader))]
internal sealed class MoedeloEntityCommandReplyKafkaTopicReader : MoedeloKafkaTopicReaderInternalBase,
    IMoedeloEntityCommandReplyKafkaTopicReader
{
    public MoedeloEntityCommandReplyKafkaTopicReader(
        IKafkaConsumerStarter consumerStarter,
        ISettingRepository settingRepository,
        IKafkaTopicNameResolver topicNameResolver,
        IKafkaConsumerBalancer consumerBalancer,
        ILogger<MoedeloEntityCommandReplyKafkaTopicReader> logger)
        : base(
            consumerStarter,
            settingRepository,
            topicNameResolver,
            consumerBalancer,
            logger)
    {
    }

    public Task ReadCommandReplyTopicAsync(
        ReadTopicSetting<MoedeloEntityCommandReplyKafkaMessageValue> settings,
        CancellationToken cancellationToken)
    {
        return ReadTopicImpl(settings, settings.OnMessage, cancellationToken);
    }
}