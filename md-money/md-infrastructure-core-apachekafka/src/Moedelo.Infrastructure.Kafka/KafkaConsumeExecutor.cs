using System.Threading;
using System.Threading.Tasks;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Infrastructure.Kafka.Abstractions.Interfaces;
using Moedelo.Infrastructure.Kafka.Abstractions.Models;

namespace Moedelo.Infrastructure.Kafka;

[InjectAsSingleton(typeof(IKafkaConsumeExecutor))]
// ReSharper disable once ClassNeverInstantiated.Global
internal sealed class KafkaConsumeExecutor(IKafkaOneTimeConsumeExecutor oneTimeConsumeExecutor,
    IKafkaConsumerEventSourceImplementation consumerEventSource) : IKafkaConsumeExecutor
{
    public async Task RunConsumeLoopAsync<TMessage>(KafkaConsumerConfig config,
        IKafkaConsumerHandlers<TMessage> handlers,
        IKafkaConsumer kafkaConsumer,
        CancellationToken cancellation) where TMessage : KafkaMessageValueBase
    {
        await consumerEventSource
            .RaiseConsumerStartedEventAsync(kafkaConsumer.ConsumerUid, config, cancellation)
            .ConfigureAwait(false);

        try
        {
            while (kafkaConsumer.CanConsume && !cancellation.IsCancellationRequested)
            {
                var handleResult = await oneTimeConsumeExecutor
                    .ConsumeAndHandleAsync(config, handlers, kafkaConsumer, cancellation)
                    .ConfigureAwait(false);

                if (!handleResult.Success)
                {
                    await consumerEventSource
                        .RaisePartitionSetOnPauseEventAsync(
                            kafkaConsumer.ConsumerUid,
                            handleResult,
                            config, cancellation)
                        .ConfigureAwait(false);
                }
            }
        }
        finally
        {
            await consumerEventSource
                .RaiseConsumerStoppedEventAsync(kafkaConsumer.ConsumerUid, config, cancellation)
                .ConfigureAwait(false);
        }
    }

    public IKafkaConsumerEventSource EventSource => consumerEventSource;
}
