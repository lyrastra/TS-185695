using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Moedelo.Infrastructure.Kafka.Abstractions.Extensions;
using Moedelo.Infrastructure.Kafka.Abstractions.Interfaces;
using Moedelo.Infrastructure.Kafka.Abstractions.Models;

namespace Moedelo.Infrastructure.Kafka.Abstractions.Implementations;

public abstract class AbstractKafkaConsumerFactory : IKafkaConsumerFactory
{
    private Lazy<Task>? initializationDelay;

    public async Task<IKafkaConsumer> CreateAsync(KafkaConsumerConfig config,
        IKafkaConsumerFactorySettings kafkaConsumerFactorySettings, ILogger logger)
    {
        // библиотеке нужно какое-то время на старте, чтобы начать работать
        initializationDelay ??= new Lazy<Task>(() =>
            Task.Delay(kafkaConsumerFactorySettings.PauseBeforeFirstConsumerStart));

        await initializationDelay.Value.ConfigureAwait(false);

        return Create(config, logger)
            .SubscribeOrDispose(config.TopicName.NameInKafka);
    }

    protected abstract IKafkaConsumer Create(
        KafkaConsumerConfig config,
        ILogger logger);
}
