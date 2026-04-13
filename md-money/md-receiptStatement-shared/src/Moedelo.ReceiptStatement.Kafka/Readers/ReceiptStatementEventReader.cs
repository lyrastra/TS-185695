using Microsoft.Extensions.Logging;
using Moedelo.Common.Audit.Abstractions.Interfaces;
using Moedelo.Common.ExecutionContext.Abstractions.Interfaces;
using Moedelo.Common.Kafka.Abstractions.Base;
using Moedelo.Common.Kafka.Abstractions.Settings;
using Moedelo.Common.Settings.Abstractions;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Infrastructure.Json;
using Moedelo.Infrastructure.Kafka.Abstractions.Interfaces;
using Moedelo.Infrastructure.Kafka.Abstractions.Models;
using Moedelo.ReceiptStatement.Kafka.Abstractions;
using Moedelo.ReceiptStatement.Kafka.Abstractions.Messages;
using Moedelo.ReceiptStatement.Kafka.Abstractions.Readers;
using Moedelo.ReceiptStatement.Kafka.Abstractions.Topics;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Moedelo.ReceiptStatement.Kafka.Readers
{
    [InjectAsSingleton(typeof(IReceiptStatementEventReader))]
    internal sealed class ReceiptStatementEventReader : MoedeloKafkaTopicReaderBase, IReceiptStatementEventReader
    {
        public ReceiptStatementEventReader(
            IMoedeloKafkaTopicReaderBaseDependencies dependencies,
            ILogger<ReceiptStatementEventReader> logger)
            : base(
                dependencies,
                logger)
        {
        }

        public Task ReadAsync(
            string groupId,
            Func<ReceiptStatementCreatedMessage, KafkaMessageValueMetadata, Task> onCreate,
            Func<ReceiptStatementUpdatedMessage, KafkaMessageValueMetadata, Task> onUpdate,
            Func<ReceiptStatementDeletedMessage, KafkaMessageValueMetadata, Task> onDelete,
            Func<CUDEventMessageValue, Exception, Task> onException = null,
            KafkaConsumerConfig.AutoOffsetResetType resetType = KafkaConsumerConfig.AutoOffsetResetType.Latest,
            int consumerCount = 1,
            CancellationToken? cancellationToken = null)
        {
            ValidateHandlerFunc(onCreate, nameof(onCreate));
            ValidateHandlerFunc(onUpdate, nameof(onUpdate));
            ValidateHandlerFunc(onDelete, nameof(onDelete));

            Task OnMessageAction(CUDEventMessageValue messageValue)
            {
                var metadata = messageValue.Metadata;

                switch (messageValue.EventType)
                {
                    case CUDEventType.Created:
                        var createdMessage = messageValue.EventDataJson.FromJsonString<ReceiptStatementCreatedMessage>();
                        return onCreate(createdMessage, metadata);
                    case CUDEventType.Updated:
                        var updatedMessage = messageValue.EventDataJson.FromJsonString<ReceiptStatementUpdatedMessage>();
                        return onUpdate(updatedMessage, metadata);
                    case CUDEventType.Deleted:
                        var deletedMessage = messageValue.EventDataJson.FromJsonString<ReceiptStatementDeletedMessage>();
                        return onDelete(deletedMessage, metadata);
                    default:
                        throw new ArgumentOutOfRangeException($"Not supported eventType: {messageValue.EventType}");
                }
            }
            var topicName = ReceiptStatementTopics.ReceiptStatement.Event.CUD;
            var settings = new ReadTopicSetting<CUDEventMessageValue>(groupId, topicName, OnMessageAction, onException, null, resetType, consumerCount);
            return ReadTopicAsync(settings, cancellationToken ?? CancellationToken.None);
        }
    }
}
