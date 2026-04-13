using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Infrastructure.Kafka.Abstractions.Interfaces;
using Moedelo.Infrastructure.Kafka.Abstractions.Models;
using Moedelo.Infrastructure.Kafka.Extensions;

namespace Moedelo.Infrastructure.Kafka;

internal interface IKafkaConsumerEventSourceImplementation : IKafkaConsumerEventSource
{
    ValueTask RaiseConsumerStartedEventAsync(
        string kafkaConsumerGuid,
        KafkaConsumerConfig config,
        CancellationToken cancellationToken);

    ValueTask RaisePartitionSetOnPauseEventAsync(
        string consumerId,
        MessageHandlingResultBase handlingResult,
        KafkaConsumerConfig config,
        CancellationToken cancellationToken);

    ValueTask RaiseConsumerStoppedEventAsync(
        string consumerId,
        KafkaConsumerConfig config,
        CancellationToken cancellationToken);
}

[InjectAsTransient(typeof(IKafkaConsumerEventSourceImplementation))]
internal class KafkaConsumerEventSource : IKafkaConsumerEventSourceImplementation
{
    private readonly ILogger logger;

    public KafkaConsumerEventSource(ILogger<KafkaConsumerEventSource> logger)
    {
        this.logger = logger;
    }

    public event IKafkaConsumerEventSource.ConsumerStarted? ConsumerStartedEvent;
    public event IKafkaConsumerEventSource.ConsumerStopped? ConsumerStoppedEvent;
    public event IKafkaConsumerEventSource.PartitionSetOnPause? PartitionSetOnPauseEvent;

    public async ValueTask RaiseConsumerStartedEventAsync(
        string kafkaConsumerGuid,
        KafkaConsumerConfig config,
        CancellationToken cancellationToken)
    {
        if (ConsumerStartedEvent != null)
        {
            try
            {
                await ConsumerStartedEvent
                    .Invoke(
                        new ConsumerStartedEvent(
                            config.GroupId.Raw,
                            kafkaConsumerGuid,
                            config.TopicName.Raw),
                        cancellationToken)
                    .ConfigureAwait(false);
            }
            catch (Exception exception)
            {
                logger.LogError(exception,
                    "Во время обработки события {EventName} произошла ошибка", nameof(ConsumerStartedEvent));
            }
        }
    }

    public async ValueTask RaisePartitionSetOnPauseEventAsync(
        string consumerId,
        MessageHandlingResultBase handlingResult,
        KafkaConsumerConfig config,
        CancellationToken cancellationToken)
    {
        // ReSharper disable once ConditionIsAlwaysTrueOrFalseAccordingToNullableAPIContract
        if (PartitionSetOnPauseEvent != null)
        {
            try
            {
                var @event = handlingResult.ToPartitionSetOnPauseEvent(
                    config.GroupId.Raw,
                    config.TopicName.Raw,
                    consumerId);

                await PartitionSetOnPauseEvent.Invoke(@event, cancellationToken).ConfigureAwait(false);
            }
            catch (Exception exception)
            {
                logger.LogError(exception,
                    "Во время обработки события {EventName} произошла ошибка", nameof(PartitionSetOnPauseEvent));
            }
        }
    }

    public async ValueTask RaiseConsumerStoppedEventAsync(
        string consumerId,
        KafkaConsumerConfig config,
        CancellationToken cancellationToken)
    {
        // ReSharper disable once ConditionIsAlwaysTrueOrFalseAccordingToNullableAPIContract
        if (ConsumerStoppedEvent != null)
        {
            try
            {
                var @event = new ConsumerStoppedEvent(
                    config.GroupId.Raw,
                    consumerId,
                    config.TopicName.Raw);
                // подменяем cancellation token, если он был уже отозван
                var cancellation = cancellationToken.IsCancellationRequested
                    ? CancellationToken.None
                    : cancellationToken;

                await ConsumerStoppedEvent.Invoke(@event, cancellation).ConfigureAwait(false);
            }
            catch (Exception exception)
            {
                logger.LogError(exception,
                    "Во время обработки события {EventName} произошла ошибка", nameof(ConsumerStoppedEvent));
            }
        }
    }
}
