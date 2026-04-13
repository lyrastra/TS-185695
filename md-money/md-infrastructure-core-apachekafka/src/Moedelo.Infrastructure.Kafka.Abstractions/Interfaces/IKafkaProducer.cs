using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Moedelo.Infrastructure.Kafka.Abstractions.Interfaces.ProducingRetry;
using Moedelo.Infrastructure.Kafka.Abstractions.Models;

namespace Moedelo.Infrastructure.Kafka.Abstractions.Interfaces
{
    public interface IKafkaProducer
    {
        /// <returns>TopicPartitionOffset</returns>
        Task<string> ProduceAsync<T>(string brokerEndpoints,
            KafkaMessage<T> message,
            IKafkaProducingRetryPolicy kafkaProducingRetryPolicy,
            CancellationToken cancellationToken) where T : KafkaMessageValueBase;

        /// <summary>
        /// Поставить в очередь на отправку коллекцию сообщений
        /// К выходу из метода сообщения могут быть ещё не отправлены
        /// </summary>
        /// <param name="brokerEndpoints">строка соединения с kafka</param>
        /// <param name="messages">список сообщений</param>
        /// <param name="flushProducer">после постановки сообщений в очередь, вызвать producer.Flush</param>
        /// <typeparam name="T"></typeparam>
        Task ProduceAsync<T>(
            string brokerEndpoints,
            IEnumerable<KafkaMessage<T>> messages,
            bool flushProducer = false)
            where T : KafkaMessageValueBase;

        /// <summary>
        /// Поставить сообщение в очередь
        /// К выходу из метода сообщение может быть (и скорее всего) ещё не отправлено
        /// </summary>
        /// <param name="brokerEndpoints">строка соединения с kafka</param>
        /// <param name="message">сообщение</param>
        /// <typeparam name="TMessage"></typeparam>
        void QueueToProduce<TMessage>(string brokerEndpoints, KafkaMessage<TMessage> message)
            where TMessage : KafkaMessageValueBase;
    }
}