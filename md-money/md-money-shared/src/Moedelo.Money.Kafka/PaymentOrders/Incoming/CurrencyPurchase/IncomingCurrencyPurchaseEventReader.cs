using Microsoft.Extensions.Logging;
using Moedelo.Common.Kafka.Abstractions.Base;
using Moedelo.Common.Kafka.Abstractions.Settings;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Infrastructure.Json;
using Moedelo.Infrastructure.Kafka.Abstractions.Models;
using Moedelo.Money.Kafka.Abstractions;
using Moedelo.Money.Kafka.Abstractions.PaymentOrders.Incoming.CurrencyPurchase;
using Moedelo.Money.Kafka.Abstractions.PaymentOrders.Incoming.CurrencyPurchase.Events;
using Moedelo.Money.Kafka.Abstractions.Topics;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Moedelo.Money.Kafka.PaymentOrders.Incoming.CurrencyPurchase
{
    [InjectAsSingleton(typeof(IIncomingCurrencyPurchaseEventReader))]
    public class IncomingCurrencyPurchaseEventReader : MoedeloKafkaTopicReaderBase, IIncomingCurrencyPurchaseEventReader
    {
        public IncomingCurrencyPurchaseEventReader(
            IMoedeloKafkaTopicReaderBaseDependencies dependencies,
            ILogger<IncomingCurrencyPurchaseEventReader> logger)
            : base(dependencies, logger)
        {
        }

        public Task ReadAsync(string groupId,
            Func<IncomingCurrencyPurchaseCreatedMessage, KafkaMessageValueMetadata, Task> onCreate,
            Func<IncomingCurrencyPurchaseUpdatedMessage, KafkaMessageValueMetadata, Task> onUpdate,
            Func<IncomingCurrencyPurchaseDeletedMessage, KafkaMessageValueMetadata, Task> onDelete,
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
                        var createdMessage = messageValue.EventDataJson.FromJsonString<IncomingCurrencyPurchaseCreatedMessage>();
                        return onCreate(createdMessage, metadata);
                    case CUDEventType.Updated:
                        var updatedMessage = messageValue.EventDataJson.FromJsonString<IncomingCurrencyPurchaseUpdatedMessage>();
                        return onUpdate(updatedMessage, metadata);
                    case CUDEventType.Deleted:
                        var deletedMessage = messageValue.EventDataJson.FromJsonString<IncomingCurrencyPurchaseDeletedMessage>();
                        return onDelete(deletedMessage, metadata);
                    default:
                        throw new ArgumentOutOfRangeException($"Not supported eventType: {messageValue.EventType}");
                }
            }

            var topicName = PaymentOrderTopics.IncomingCurrencyPurchase.Event.CUD;
            var settings = new ReadTopicSetting<CUDEventMessageValue>(
                groupId,
                topicName,
                OnMessageAction,
                onException,
                onFatalException,
                resetType: resetType,
                consumersCount: consumerCount);
            return ReadTopicWithRetryAsync(settings, retrySettings, cancellationToken ?? CancellationToken.None);
        }
    }
}