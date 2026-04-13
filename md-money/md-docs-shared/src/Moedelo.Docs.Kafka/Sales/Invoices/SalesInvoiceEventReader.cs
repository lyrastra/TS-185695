using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Moedelo.Common.Kafka.Abstractions.Base;
using Moedelo.Common.Kafka.Abstractions.Settings;
using Moedelo.Docs.Kafka.Abstractions;
using Moedelo.Docs.Kafka.Abstractions.Sales.Invoices;
using Moedelo.Docs.Kafka.Abstractions.Sales.Invoices.Events;
using Moedelo.Docs.Kafka.Abstractions.Sales.SignStatusChanged.Events;
using Moedelo.Docs.Kafka.Abstractions.Topics.ByApps;
using Moedelo.Docs.Kafka.Sales.CurrencyInvoices;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Infrastructure.Json;
using Moedelo.Infrastructure.Kafka.Abstractions.Models;

namespace Moedelo.Docs.Kafka.Sales.Invoices
{
    [InjectAsSingleton(typeof(ISalesInvoiceEventReader))]
    internal sealed class SalesInvoiceEventReader : MoedeloKafkaTopicReaderBase, ISalesInvoiceEventReader 
    {
        public SalesInvoiceEventReader(
            IMoedeloKafkaTopicReaderBaseDependencies dependencies,
            ILogger<SalesCurrencyInvoiceEventReader> logger)
            : base(
                dependencies,
                logger)
        {
        }

        public Task ReadAsync(
            string groupId,
            Func<SalesInvoicesUpdatedMessage, KafkaMessageValueMetadata, Task> onUpdate = null,
            Func<SignStatusChangedMessage, KafkaMessageValueMetadata, Task> onSignStatusUpdate = null,
            Func<DocsCudEventMessageValue, Exception, Task> onException = null,
            KafkaConsumerConfig.AutoOffsetResetType resetType = KafkaConsumerConfig.AutoOffsetResetType.Latest,
            int consumerCount = 1,
            CancellationToken? cancellationToken = null)
        {
            Task OnMessageAction(DocsCudEventMessageValue messageValue)
            {
                var metadata = messageValue.Metadata;

                switch (messageValue.EventType)
                {
                    case DocsCudEventType.Updated:
                    {
                        if (onUpdate == null)
                        {
                            return Task.CompletedTask;
                        }

                        var updatedMessage = messageValue.EventDataJson.FromJsonString<SalesInvoicesUpdatedMessage>();
                        return onUpdate(updatedMessage, metadata);
                    }
                    case DocsCudEventType.SignStatusUpdated:
                    {
                        if (onSignStatusUpdate == null)
                        {
                            return Task.CompletedTask;
                        }

                        var signStatusUpdatedMessage = messageValue.EventDataJson.FromJsonString<SignStatusChangedMessage>();
                        return onSignStatusUpdate(signStatusUpdatedMessage, metadata);
                    }
                    default:
                        throw new ArgumentOutOfRangeException($"Not supported eventType: {messageValue.EventType}");
                }
            }
            var topicName = InvoiceTopics.Sales.Event.CUD;
            var settings = new ReadTopicSetting<DocsCudEventMessageValue>(
                groupId,
                topicName,
                OnMessageAction,
                onException,
                onFatalException:null,
                resetType: resetType,
                consumersCount: consumerCount);
            return ReadTopicWithRetryAsync(settings, cancellationToken ?? CancellationToken.None);
        }
    }
}