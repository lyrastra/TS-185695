using Microsoft.Extensions.Logging;
using Moedelo.Common.Kafka.Abstractions.Base;
using Moedelo.Common.Kafka.Abstractions.Settings;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Infrastructure.Json;
using Moedelo.Infrastructure.Kafka.Abstractions.Models;
using Moedelo.Money.Kafka.Abstractions;
using Moedelo.Money.Kafka.Abstractions.PaymentOrders.Incoming.TransferFromAccount;
using Moedelo.Money.Kafka.Abstractions.PaymentOrders.Incoming.TransferFromAccount.Events;
using Moedelo.Money.Kafka.Abstractions.Topics;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Moedelo.Money.Kafka.PaymentOrders.Incoming.TransferFromAccount
{
    [InjectAsSingleton(typeof(ITransferFromAccountEventReader))]
    internal sealed class TransferFromAccountEventReader : MoedeloKafkaTopicReaderBase, ITransferFromAccountEventReader
    {
        public TransferFromAccountEventReader(
            IMoedeloKafkaTopicReaderBaseDependencies dependencies,
            ILogger<TransferFromAccountEventReader> logger)
            : base(
                dependencies,
                logger)
        {
        }

        public Task ReadAsync(string groupId,
            Func<TransferFromAccountCreatedMessage, KafkaMessageValueMetadata, Task> onCreate,
            Func<TransferFromAccountUpdatedMessage, KafkaMessageValueMetadata, Task> onUpdate,
            Func<TransferFromAccountDeletedMessage, KafkaMessageValueMetadata, Task> onDelete,
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
                        var createdMessage = messageValue.EventDataJson.FromJsonString<TransferFromAccountCreatedMessage>();
                        return onCreate(createdMessage, metadata);
                    case CUDEventType.Updated:
                        var updatedMessage = messageValue.EventDataJson.FromJsonString<TransferFromAccountUpdatedMessage>();
                        return onUpdate(updatedMessage, metadata);
                    case CUDEventType.Deleted:
                        var deletedMessage = messageValue.EventDataJson.FromJsonString<TransferFromAccountDeletedMessage>();
                        return onDelete(deletedMessage, metadata);
                    default:
                        throw new ArgumentOutOfRangeException($"Not supported eventType: {messageValue.EventType}");
                }
            }

            var topicName = PaymentOrderTopics.TransferFromAccount.Event.CUD;
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