using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Infrastructure.Json;
using Moedelo.Infrastructure.Kafka.Abstractions.Exceptions;
using Moedelo.Infrastructure.Kafka.Abstractions.Interfaces;
using Moedelo.Infrastructure.Kafka.Abstractions.Models;
using Moedelo.Infrastructure.Kafka.Extensions;

namespace Moedelo.Infrastructure.Kafka;

[InjectAsSingleton(typeof(IKafkaOneTimeConsumeExecutor))]
internal sealed class KafkaOneTimeConsumeExecutor(IKafkaConsumerFactorySettings settings,
    ILogger<KafkaOneTimeConsumeExecutor> logger) : IKafkaOneTimeConsumeExecutor
{
    public async ValueTask<MessageHandlingResultBase> ConsumeAndHandleAsync<TMessage>(KafkaConsumerConfig config,
        IKafkaConsumerHandlers<TMessage> handlers,
        IKafkaConsumer kafkaConsumer,
        CancellationToken cancellation) where TMessage : KafkaMessageValueBase
    {
        logger.LogMessageConsuming(settings.ConsumingLogLevel, config.TopicName.NameInKafka, kafkaConsumer);
        // чтение сообщения из кафки
        var consumeResult = await kafkaConsumer.ConsumeAsync(cancellation).ConfigureAwait(false)
            ?? throw new UnexpectedNullConsumeResultException(config);
        cancellation.ThrowIfCancellationRequested();
        logger.LogMessageConsumed<TMessage>(settings.ConsumedLogLevel, consumeResult, kafkaConsumer);

        // вызов обработчика
        var messageHandler = handlers.HandleMessage;
        var handlingResult = await CallMessageHandlerAsync(consumeResult,
                messageHandler, kafkaConsumer, cancellation)
            .ConfigureAwait(false);

        // фиксирование результата обработки
        if (handlingResult.Success)
        {
            await CommitMessageAsync(kafkaConsumer, handlingResult, handlers).ConfigureAwait(false);
            return handlingResult;
        }

        // произошла ошибка обработки сообщения: ставим обработку секции на паузу, сообщение пропускаем
        await SetPartitionOnPauseAsync(kafkaConsumer, handlingResult, handlers).ConfigureAwait(false);
        
        return handlingResult;
    }
    
    /// <summary>
    /// Вызов обработчика сообщения
    /// Отлавливает все исключения, кроме OperationCanceledException при отозванном cancellationToken
    /// </summary>
    /// <param name="consumeResult">данные, полученные из топика кафки</param>
    /// <param name="handleMessage">обработчик сообщений</param>
    /// <param name="kafkaConsumer"></param>
    /// <param name="cancellationToken">токен отмены операции</param>
    /// <typeparam name="TMessage"></typeparam>
    /// <returns>результат обработки</returns>
    private async ValueTask<MessageHandlingResult<TMessage>> CallMessageHandlerAsync<TMessage>(
        IConsumeResultWrap consumeResult,
        Func<TMessage, CancellationToken, Task> handleMessage,
        IKafkaConsumer kafkaConsumer,
        CancellationToken cancellationToken) where TMessage : KafkaMessageValueBase
    {
        var consumingWatch = Stopwatch.StartNew();
        TMessage? message = default;
        try
        {
            message = consumeResult.MessageValue.FromJsonString<TMessage>();
            message.Metadata ??= new KafkaMessageValueMetadata();
            message.Metadata.Partition = consumeResult.Partition;
            message.Metadata.Offset = consumeResult.Offset;

            await handleMessage(message, cancellationToken).ConfigureAwait(false);

            consumingWatch.Stop();

            return MessageHandlingResult<TMessage>.CreateSuccess(consumeResult, message, consumingWatch);
        }
        catch (Exception exception)
        {
            consumingWatch.Stop();

            if (exception is OperationCanceledException)
            {
                // не очень хорошая ситуация - операцию сняли во время обработки сообщения. Залогируем ошибку
                logger.LogMessageHandlingCanceled(consumeResult, exception, kafkaConsumer);

                if (cancellationToken.IsCancellationRequested)
                {
                    throw;
                }
            }

            return MessageHandlingResult<TMessage>.CreateFailed(consumeResult, message, exception, consumingWatch);
        }
    }

    private async ValueTask CommitMessageAsync<TMessage>(IKafkaConsumer consumer,
        MessageHandlingResult<TMessage> handlingResult,
        IKafkaConsumerHandlers<TMessage> handlers) where TMessage : KafkaMessageValueBase
    {
        var consumeResult = handlingResult.ConsumeResult;
        logger.LogErrorIfMaxPollIntervalExceeded(consumer.MaxPollIntervalMs, handlingResult.Duration, consumeResult, consumer);

        try
        {
            await consumer.CommitAsync(consumeResult).ConfigureAwait(false);
            logger.LogMessageCommitment(settings.CommitmentLogLevel, consumeResult, handlingResult.Duration, consumer);
            handlers.OnMessageCommitted(handlingResult.Message!);
        }
        catch(Exception exception)
        {
            logger.LogMessageCommitmentError(consumeResult, exception, handlingResult.Duration, consumer);
            handlers.OnMessageHandlingFailed(handlingResult.Message!, exception);

            throw;
        }
    }

    /// <summary>
    /// Поставить обработку партиции топика на паузу
    /// </summary>
    private async ValueTask SetPartitionOnPauseAsync<TMessage>(IKafkaConsumer kafkaConsumer,
        MessageHandlingResult<TMessage> handlingResult,
        IKafkaConsumerHandlers<TMessage> handlers) where TMessage : KafkaMessageValueBase
    {
        handlers.OnMessageHandlingFailed(handlingResult.Message!, handlingResult.Exception!);

        var consumeResult = handlingResult.ConsumeResult;
        var offset = consumeResult.TopicPartitionOffset;
        var wasAlreadyPaused = kafkaConsumer.IsPaused(offset);

        await kafkaConsumer
            .OnMessageHandlingFailedAsync(offset, consumeResult.MessageKey)
            .ConfigureAwait(false);

        if (!wasAlreadyPaused && kafkaConsumer.IsPaused(offset))
        {
            logger.LogPartitionSetOnPauseError(consumeResult, handlingResult.Exception!, kafkaConsumer);
        }
    }
}
