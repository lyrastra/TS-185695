using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Moedelo.Common.Kafka.Abstractions.Base;
using Moedelo.Common.Kafka.Abstractions.Settings;
using Moedelo.Docs.Kafka.Abstractions;
using Moedelo.Docs.Kafka.Abstractions.Purchases.CurrencyInvoices.Messages;
using Moedelo.Docs.Kafka.Abstractions.Purchases.CurrencyInvoices.Readers;
using Moedelo.Docs.Kafka.Abstractions.Topics.ByApps;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Infrastructure.Json;
using Moedelo.Infrastructure.Kafka.Abstractions.Models;

namespace Moedelo.Docs.Kafka.Purchases.CurrencyInvoices
{
    [InjectAsSingleton(typeof(IPurchaseCurrencyInvoiceEventReader))]
    internal sealed class PurchaseCurrencyInvoiceEventReader : MoedeloKafkaTopicReaderBase, IPurchaseCurrencyInvoiceEventReader
    {
        public PurchaseCurrencyInvoiceEventReader(
            IMoedeloKafkaTopicReaderBaseDependencies dependencies,
            ILogger<PurchaseCurrencyInvoiceEventReader> logger)
            : base(
                dependencies,
                logger)
        {
        }
        
        public Task ReadAsync(
            string groupId,
            Func<PurchaseCurrencyInvoiceCreatedMessage, KafkaMessageValueMetadata, Task> onCreate,
            Func<PurchaseCurrencyInvoiceUpdatedMessage, KafkaMessageValueMetadata, Task> onUpdate,
            Func<PurchaseCurrencyInvoiceDeletedMessage, KafkaMessageValueMetadata, Task> onDelete,
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
                        var createdMessage = messageValue.EventDataJson.FromJsonString<PurchaseCurrencyInvoiceCreatedMessage>();
                        return onCreate(createdMessage, metadata);
                    case DocsCudEventType.Updated:
                        var updatedMessage = messageValue.EventDataJson.FromJsonString<PurchaseCurrencyInvoiceUpdatedMessage>();
                        return onUpdate(updatedMessage, metadata);
                    case DocsCudEventType.Deleted:
                        var deletedMessage = messageValue.EventDataJson.FromJsonString<PurchaseCurrencyInvoiceDeletedMessage>();
                        return onDelete(deletedMessage, metadata);
                    default:
                        throw new ArgumentOutOfRangeException($"Not supported eventType: {messageValue.EventType}");
                }
            }
            var topicName = CurrencyInvoiceTopics.Purchase.Event.CUD;
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