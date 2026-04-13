using Microsoft.Extensions.Logging;
using Moedelo.Common.Kafka.Abstractions.Base;
using Moedelo.Common.Kafka.Abstractions.Settings;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Infrastructure.Json;
using Moedelo.Infrastructure.Kafka.Abstractions.Models;
using Moedelo.Money.Kafka.Abstractions;
using Moedelo.Money.Kafka.Abstractions.PaymentOrders.Incoming.TransferFromCash;
using Moedelo.Money.Kafka.Abstractions.PaymentOrders.Incoming.TransferFromCash.Events;
using Moedelo.Money.Kafka.Abstractions.Topics;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Moedelo.Money.Kafka.PaymentOrders.Incoming.TransferFromCash
{
    [InjectAsSingleton(typeof(ITransferFromCashEventReader))]
    internal sealed class TransferFromCashEventReader : MoedeloKafkaTopicReaderBase, ITransferFromCashEventReader
    {
        public TransferFromCashEventReader(
            IMoedeloKafkaTopicReaderBaseDependencies dependencies,
            ILogger<TransferFromCashEventReader> logger)
            : base(
                dependencies,
                logger)
        {
        }

        public Task ReadAsync(string groupId,
            Func<TransferFromCashCreatedMessage, KafkaMessageValueMetadata, Task> onCreate,
            Func<TransferFromCashUpdatedMessage, KafkaMessageValueMetadata, Task> onUpdate,
            Func<TransferFromCashDeletedMessage, KafkaMessageValueMetadata, Task> onDelete,
            Func<CUDEventMessageValue, Exception, Task> onException = null,
            Func<Exception, Task> onFatalException = null,
            KafkaConsumerConfig.AutoOffsetResetType resetType = KafkaConsumerConfig.AutoOffsetResetType.Latest,
            int consumerCount = 1,
            ConsumerActionRetrySettings retrySettings = null,
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
                        var createdMessage = messageValue.EventDataJson.FromJsonString<TransferFromCashCreatedMessage>();
                        return onCreate(createdMessage, metadata);
                    case CUDEventType.Updated:
                        var updatedMessage = messageValue.EventDataJson.FromJsonString<TransferFromCashUpdatedMessage>();
                        return onUpdate(updatedMessage, metadata);
                    case CUDEventType.Deleted:
                        var deletedMessage = messageValue.EventDataJson.FromJsonString<TransferFromCashDeletedMessage>();
                        return onDelete(deletedMessage, metadata);
                    default:
                        throw new ArgumentOutOfRangeException($"Not supported eventType: {messageValue.EventType}");
                }
            }

            var topicName = PaymentOrderTopics.TransferFromCash.Event.CUD;
            var settings = new ReadTopicSetting<CUDEventMessageValue>(
                groupId,
                topicName,
                OnMessageAction,
                onException,
                onFatalException,
                resetType,
                consumerCount);
            return ReadTopicWithRetryAsync(settings, retrySettings, cancellationToken ?? CancellationToken.None);
        }
    }
}