using System;

namespace Moedelo.Infrastructure.Kafka.Abstractions.Models;

public readonly record struct KafkaConsumerSettings<TMessage>(
    KafkaConsumerConfig Config,
    IKafkaConsumerHandlers<TMessage> Handlers,
    Type? ConsumerFactoryType = null
) where TMessage : KafkaMessageValueBase;
