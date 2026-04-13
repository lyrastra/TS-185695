using System;
using System.Collections.Generic;
using Confluent.Kafka;

namespace Moedelo.Infrastructure.Kafka;

public readonly record struct ConfluentConsumerEventHandlers(
    Action<IConsumer<string, string>, Error> OnError,
    Action<IConsumer<string, string>, List<TopicPartition>>? OnPartitionAssigned,
    Action<IConsumer<string, string>, List<TopicPartitionOffset>>? OnPartitionRevoked);
