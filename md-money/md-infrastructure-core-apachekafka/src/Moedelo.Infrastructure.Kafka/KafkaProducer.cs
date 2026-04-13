using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Confluent.Kafka;
using Microsoft.Extensions.Logging;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Infrastructure.Json;
using Moedelo.Infrastructure.Kafka.Abstractions;
using Moedelo.Infrastructure.Kafka.Abstractions.Interfaces;
using Moedelo.Infrastructure.Kafka.Abstractions.Interfaces.ProducingRetry;
using Moedelo.Infrastructure.Kafka.Abstractions.Models;

namespace Moedelo.Infrastructure.Kafka
{
    [InjectAsSingleton(typeof(IKafkaProducer))]
    internal sealed class KafkaProducer : IKafkaProducer
    {
        private readonly IProducerPool producerPool;
        private readonly ILogger logger;

        public KafkaProducer(IProducerPool producerPool, ILogger<KafkaProducer> logger)
        {
            this.producerPool = producerPool;
            this.logger = logger;
        }

        public async Task<string> ProduceAsync<T>(string brokerEndpoints,
            KafkaMessage<T> message,
            IKafkaProducingRetryPolicy retryPolicy,
            CancellationToken cancellationToken)
            where T : KafkaMessageValueBase
        {
            var producer = producerPool.GetProducer(brokerEndpoints);
            var topicName = message.TopicName;

            var rawMessage = new Message<string, string>
            {
                Key = message.Key,
                Value = message.Value.ToJsonString(),
            };

            var deliveryResult = await ProduceWithRetryAsync(producer, retryPolicy, topicName, rawMessage, cancellationToken)
                .ConfigureAwait(false);

            return deliveryResult.TopicPartitionOffset.ToString();
        }

        private async Task<DeliveryResult<string, string>> ProduceWithRetryAsync(
            IProducer<string, string> producer,
            IKafkaProducingRetryPolicy retryPolicy,
            string topicName,
            Message<string, string> rawMessage,
            CancellationToken cancellationToken)
        {
            var retryState = retryPolicy.CreateProducingRetryState(topicName);

            while(!cancellationToken.IsCancellationRequested)
            {
                try
                {
                    return await producer
                        .ProduceAsync(topicName, rawMessage, cancellationToken)
                        .ConfigureAwait(false);
                }
                catch (KafkaException exception)
                {
                    retryState = retryPolicy.OnException(retryState, exception);

                    if (retryState.MustRetry)
                    {
                        logger.LogError(exception,
                            "Исключение при отправке сообщения в топик {TopicName} (ошибок подряд: {ErrorsCount}), повторная попытка будет сделана через {Pause}",
                            topicName,
                            retryState.ErrorsCount,
                            retryState.PauseBeforeRetry?.ToString("g") ?? "null");

                        if (retryState.PauseBeforeRetry.HasValue)
                        {
                            await Task
                                .Delay(retryState.PauseBeforeRetry.Value, cancellationToken)
                                .ConfigureAwait(false);
                        }

                        continue;
                    }

                    throw new Exception($"При отправке сообщения в топик {topicName} возникло исключение", exception);
                }
                catch (Exception exception)
                {
                    throw new Exception($"При отправке сообщения в топик {topicName} возникло исключение типа {exception.GetType().Name}", exception);
                }

            }
            cancellationToken.ThrowIfCancellationRequested();

            throw new Exception("Парадокс: вызван недостижимый участок кода");
        }

#pragma warning disable 1998
        public async Task ProduceAsync<T>(
#pragma warning restore 1998
            string brokerEndpoints,
            IEnumerable<KafkaMessage<T>> messages,
            bool flushProducer = false)
            where T : KafkaMessageValueBase
        {
            var producer = producerPool.GetProducer(brokerEndpoints);

            try
            {
                foreach (var message in messages)
                {
                    var topic = message.TopicName;
                    var key = message.Key;
                    var value = message.Value.ToJsonString();

                    producer.Produce(topic, new Message<string, string>
                    {
                        Key = key,
                        Value = value,
                    });
                }

                if (flushProducer)
                {
                    var flashTimeout = TimeSpan.FromSeconds(2);
                    producer.Flush(flashTimeout);
                }
            }
            catch (Exception e)
            {
                throw new Exception($"При отправке сообщений возникло исключение", e);
            }
        }

        public void QueueToProduce<TMessage>(string brokerEndpoints, KafkaMessage<TMessage> message) where TMessage : KafkaMessageValueBase
        {
            var producer = producerPool.GetProducer(brokerEndpoints);

            var topic = message.TopicName;
            var key = message.Key;
            var value = message.Value.ToJsonString();

            producer.Produce(topic, new Message<string, string>
            {
                Key = key,
                Value = value,
            });
        }
    }
}