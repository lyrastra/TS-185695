using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Moedelo.Common.Audit.Abstractions.Interfaces;
using Moedelo.Common.ExecutionContext.Abstractions.Interfaces;
using Moedelo.Common.Kafka.Abstractions.Extensions;
using Moedelo.Common.Settings.Abstractions.Models;
using Moedelo.Infrastructure.Kafka.Abstractions.Interfaces;
using Moedelo.Infrastructure.Kafka.Abstractions.Interfaces.ProducingRetry;
using Moedelo.Infrastructure.Kafka.Abstractions.Models;

namespace Moedelo.Common.Kafka.Abstractions.Base
{
    public abstract class MoedeloKafkaTopicWriterBase
    {
        private readonly IKafkaProducer producer;
        private readonly IExecutionInfoContextAccessor contextAccessor;
        private readonly IAuditScopeManager auditScopeManager;
        private readonly IKafkaTopicNameResolver topicNameResolver;
        private readonly IKafkaProducingRetryPolicy producingRetryPolicy;
        private readonly IKafkaProducingRetryPolicy producingNoRetryPolicy;

        private readonly SettingValue brokerEndpointsSetting;

        protected MoedeloKafkaTopicWriterBase(IMoedeloKafkaTopicWriterBaseDependencies dependencies)
        {
            this.producer = dependencies.Producer;
            this.contextAccessor = dependencies.ContextAccessor;
            this.auditScopeManager = dependencies.AuditScopeManager;
            this.topicNameResolver = dependencies.TopicNameResolver;
            this.producingRetryPolicy = dependencies.ProducingRetryPolicy;
            this.producingNoRetryPolicy = dependencies.ProducingNoRetryPolicy;
            brokerEndpointsSetting = dependencies.SettingRepository.GetKafkaBrokerEndpoints();
        }

        // [Obsolete("используй метод с CancellationToken")]
        protected Task<string> WriteAsync<TMessage>(
            string topicName,
            string key,
            TMessage message)
            where TMessage : MoedeloKafkaMessageValueBase =>
            WriteAsync(topicName, key, message, CancellationToken.None);

        protected Task<string> WriteAsync<TMessage>(
            string topicName,
            string key,
            TMessage message,
            CancellationToken cancellationToken)
            where TMessage : MoedeloKafkaMessageValueBase =>
            WriteAsync(topicName, key, message, ProducingSettings.Default, cancellationToken);
        
        protected Task<string> WriteAsync<TMessage>(
            string topicName,
            string key,
            TMessage message,
            ProducingSettings settings,
            CancellationToken cancellationToken) where TMessage : MoedeloKafkaMessageValueBase
        {
            topicName.EnsureIsNotNullOrWhiteSpace(nameof(topicName));
            key.EnsureIsNotNullOrWhiteSpace(nameof(key));
            message.EnsureIsNotNull(nameof(message));

            message.Metadata ??= GenerateMetadata();

            if (string.IsNullOrEmpty(message.Token))
            {
                message.Token = contextAccessor.ExecutionInfoContextToken;
            }

            if (message.AuditSpanContext == null)
            {
                var auditSpanContext = auditScopeManager.Current?.Span?.Context;
                
                if (auditSpanContext != null)
                {
                    message.AuditSpanContext = new AuditSpanContext(auditSpanContext);
                }
            }

            var fullTopicName = topicNameResolver.GetTopicFullName(topicName);

            var kafkaMessage = new KafkaMessage<TMessage>(fullTopicName, key, message);
            var brokerEndpoints = brokerEndpointsSetting.Value;

            var retryPolicy = settings.NoRetryIfProducingFailed
                ? producingNoRetryPolicy
                : producingRetryPolicy;

            return producer.ProduceAsync(brokerEndpoints, kafkaMessage, retryPolicy, cancellationToken);
        }

        /// <summary>
        /// Поставить в очередь на отправку коллекцию сообщений
        /// К выходу из метода сообщения могут быть ещё не отправлены
        /// Т.е. нет 100-процентной гарантии, что сообщения будут оправлены 
        /// </summary>
        protected async Task QueueToWriteAsync<TMessage>(
            string topicName,
            IEnumerable<KeyValuePair<string, TMessage>> messagesList,
            CancellationToken cancellationToken) where TMessage : MoedeloKafkaMessageValueBase
        {
            topicName.EnsureIsNotNullOrWhiteSpace(nameof(topicName));
            var fullTopicName = topicNameResolver.GetTopicFullName(topicName);
            var contextToken = contextAccessor.ExecutionInfoContextToken;
            var auditSpanContext = auditScopeManager.Current?.Span?.Context;
            var brokerEndpoints = brokerEndpointsSetting.Value;

            var kafkaMessageList = messagesList.Select(kv =>
            {
                var key = kv.Key;
                var message = kv.Value;

                key.EnsureIsNotNullOrWhiteSpace(nameof(key));
                message.EnsureIsNotNull(nameof(message));

                message.Metadata ??= GenerateMetadata();

                if (string.IsNullOrEmpty(message.Token))
                {
                    message.Token = contextToken;
                }

                if (message.AuditSpanContext == null)
                {
                    if (auditSpanContext != null)
                    {
                        message.AuditSpanContext = new AuditSpanContext(auditSpanContext);
                    }
                }

                return new KafkaMessage<TMessage>(fullTopicName, key, message);
            });
 
            await producer.ProduceAsync(brokerEndpoints, kafkaMessageList, flushProducer: false);
        }

        private static KafkaMessageValueMetadata GenerateMetadata()
        {
            return new KafkaMessageValueMetadata
            {
                MessageId = Guid.NewGuid(),
                MessageDate = DateTime.UtcNow
            };
        }

        protected static void ValidateMessage<T>(T message) where T : class 
        {
            if (message == null)
            {
                throw new ArgumentNullException($"Message of type {typeof(T).Name} can not be null");
            }
        }
    }
}