#nullable enable
using System;
using Moedelo.Common.Kafka.Abstractions.Extensions;
using Moedelo.Infrastructure.Kafka.Abstractions.Models;

namespace Moedelo.Common.Kafka.Abstractions.Settings;

/// <summary>
/// Настройки консьюмера
/// </summary>
public class KafkaConsumerSettings
{
    private readonly int? consumersCount;

    internal KafkaConsumerSettings(
        string groupId,
        string topicName,
        KafkaConsumerConfig.AutoOffsetResetType resetType,
        int? consumersCount)
    {
        if (consumersCount is < 1)
        {
            throw new ArgumentException("Значение должно быть либо положительным, либо равно null",
                nameof(consumersCount));
        }

        GroupId = groupId.EnsureIsNotNullOrWhiteSpace(nameof(groupId));
        TopicName = topicName.EnsureIsNotNullOrWhiteSpace(nameof(topicName));
        ResetType = resetType;
        this.consumersCount = consumersCount;
    }

    public string GroupId { get; }

    public string TopicName { get; }

    public KafkaConsumerConfig.AutoOffsetResetType ResetType { get; }

    public int ConsumerCount => consumersCount.GetValueOrDefault(0);

    public bool AutoConsumersCount => consumersCount == null;
        
    public int? FetchWaitMaxMs { get; set; }

    public int? FetchErrorBackoffMs { get; set; }

    public int? FetchMinBytes { get; set; }
        
    public int? FetchMaxBytes { get; set; }

    public int? QueuedMinMessages { get; set; }
        
    public int? SessionTimeoutMs { get; set; }

    /// <summary>
    /// Максимальное време между соседними вызовами Consume
    /// По сути, это максимальное время на обработку одного сообщения приложением
    /// default: 300000 мс
    /// </summary>
    public int? MaxPollIntervalMs { get; set; }

    /// <summary>
    /// Использовать или нет ExecutionContext
    /// </summary>
    public bool UseContext { get; set; } = true;

    public ExtraOptions ExtraOptions { get; } = new();

    public Type? ConsumerFactoryType { get; set; }

    public bool IgnoreExecutionContextOutdating { get; set; }

    /// <summary>
    /// Количество нефатальных ошибок получения сообщения из кафки, которое будет проигнорировано.
    /// По достижению указанного количества будет выброшено исключение, которое приведёт к логированию ошибок и аварийной остановке консьюмера.
    /// Обычно с такими ошибками библиотека справляется сама, перезапрашивая сообщение.
    /// Поэтому на практике нет причин его выставлять
    /// Значение по умолчанию: 1000
    /// </summary>
    public int? MaxCountOfIgnoringConsumeExceptionsInRow { get; set; }
}
