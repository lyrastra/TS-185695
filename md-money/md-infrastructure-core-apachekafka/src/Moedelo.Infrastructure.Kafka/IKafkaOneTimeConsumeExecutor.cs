using System.Threading;
using System.Threading.Tasks;
using Moedelo.Infrastructure.Kafka.Abstractions.Interfaces;
using Moedelo.Infrastructure.Kafka.Abstractions.Models;

namespace Moedelo.Infrastructure.Kafka;

internal interface IKafkaOneTimeConsumeExecutor
{
    ValueTask<MessageHandlingResultBase> ConsumeAndHandleAsync<TMessage>(KafkaConsumerConfig config,
        IKafkaConsumerHandlers<TMessage> handlers,
        IKafkaConsumer kafkaConsumer,
        CancellationToken cancellation) where TMessage : KafkaMessageValueBase;
}
