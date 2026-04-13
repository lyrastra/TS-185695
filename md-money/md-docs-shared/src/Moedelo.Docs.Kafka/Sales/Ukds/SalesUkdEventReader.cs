using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Moedelo.Common.Kafka.Abstractions.Base;
using Moedelo.Common.Kafka.Abstractions.Settings;
using Moedelo.Docs.Kafka.Abstractions;
using Moedelo.Docs.Kafka.Abstractions.Sales.Ukds;
using Moedelo.Docs.Kafka.Abstractions.Sales.Ukds.Events;
using Moedelo.Docs.Kafka.Abstractions.Topics;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Infrastructure.Json;
using Moedelo.Infrastructure.Kafka.Abstractions.Models;

namespace Moedelo.Docs.Kafka.Sales.Ukds
{
    [InjectAsSingleton(typeof(ISalesUkdEventReader))]
    public class SalesUkdEventReader : MoedeloKafkaTopicReaderBase, ISalesUkdEventReader
    {
        public SalesUkdEventReader(
            IMoedeloKafkaTopicReaderBaseDependencies dependencies,
            ILogger<SalesUkdEventReader> logger)
            : base(
                dependencies,
                logger)
        {
        }
        
        public Task ReadAsync(
            string groupId,
            Func<SalesUkdCreatedMessage, KafkaMessageValueMetadata, Task> onCreate,
            Func<SalesUkdUpdatedMessage, KafkaMessageValueMetadata, Task> onUpdate,
            Func<SalesUkdDeletedMessage, KafkaMessageValueMetadata, Task> onDelete,
            Func<DocsCudEventMessageValue, Exception, Task> onException = null,
            KafkaConsumerConfig.AutoOffsetResetType resetType = KafkaConsumerConfig.AutoOffsetResetType.Latest,
            int consumerCount = 1,
            CancellationToken? cancellationToken = null)
        {
            ValidateHandlerFunc(onCreate, nameof(onCreate));
            ValidateHandlerFunc(onUpdate, nameof(onUpdate));
            ValidateHandlerFunc(onDelete, nameof(onDelete));

            Task OnMessageAction(DocsCudEventMessageValue messageValue)
            {
                var metadata = messageValue.Metadata;

                switch (messageValue.EventType)
                {
                    case DocsCudEventType.Created:
                        var createdMessage = messageValue.EventDataJson.FromJsonString<SalesUkdCreatedMessage>();
                        return onCreate(createdMessage, metadata);
                    case DocsCudEventType.Updated:
                        var updatedMessage = messageValue.EventDataJson.FromJsonString<SalesUkdUpdatedMessage>();
                        return onUpdate(updatedMessage, metadata);
                    case DocsCudEventType.Deleted:
                        var deletedMessage = messageValue.EventDataJson.FromJsonString<SalesUkdDeletedMessage>();
                        return onDelete(deletedMessage, metadata);
                    default:
                        throw new ArgumentOutOfRangeException($"Not supported eventType: {messageValue.EventType}");
                }
            }
            var topicName = AccountingPrimaryDocumentsTopics.Sales.Ukds.CUD;
            var settings = new ReadTopicSetting<DocsCudEventMessageValue>(
                groupId,
                topicName,
                OnMessageAction,
                onException,
                onFatalException: null,
                resetType: resetType,
                consumersCount: consumerCount);
            return ReadTopicWithRetryAsync(settings, cancellationToken ?? CancellationToken.None);
        }
    }
}