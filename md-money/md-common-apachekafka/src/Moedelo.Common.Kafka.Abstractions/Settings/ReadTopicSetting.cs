#nullable enable
using System;
using System.Threading.Tasks;
using Moedelo.Common.Kafka.Abstractions.Base;
using Moedelo.Common.Kafka.Abstractions.Extensions;
using Moedelo.Infrastructure.Kafka.Abstractions.Models;

namespace Moedelo.Common.Kafka.Abstractions.Settings;

public class ReadTopicSetting<TMessage> : KafkaConsumerSettings where TMessage : MoedeloKafkaMessageValueBase
{
    public ReadTopicSetting(
        string groupId,
        string topicName,
        Func<TMessage, Task> onMessage,
        Func<TMessage, Exception, Task> onMessageException,
        Func<Exception, Task> onFatalException,
        KafkaConsumerConfig.AutoOffsetResetType resetType,
        int? consumersCount) : base(groupId, topicName, resetType, consumersCount)
    {
        OnMessage = onMessage.EnsureIsNotNull(nameof(onMessage));
        OnMessageException = onMessageException;
        OnFatalException = onFatalException;
    }

    public ReadTopicSetting(
        string groupId,
        string topicName,
        Func<TMessage, Task> onMessage,
        KafkaConsumerConfig.AutoOffsetResetType resetType,
        int? consumersCount) : base(groupId, topicName, resetType, consumersCount)
    {
        OnMessage = onMessage.EnsureIsNotNull(nameof(onMessage));
    }

    public Func<TMessage, Task> OnMessage { get; }
    public Func<TMessage, Exception, Task>? OnMessageException { get; init; }
    public Func<Exception, Task>? OnFatalException { get; init; }
    public Func<TMessage, Exception, ValueTask<string>>? OnInvalidExecutionContextToken { get; init; }
}
