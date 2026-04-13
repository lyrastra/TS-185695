using Microsoft.Extensions.Logging;
using Moedelo.Common.Kafka.Abstractions.Base;
using Moedelo.Common.Kafka.Abstractions.Settings;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Infrastructure.Kafka.Abstractions.Models;
using Moedelo.LinkedDocuments.Kafka.Abstractions.Links;
using Moedelo.LinkedDocuments.Kafka.Abstractions.Links.Events.ConcreteLinks;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Moedelo.LinkedDocuments.Kafka.Links.Events.ConcreteLinks
{
    [InjectAsSingleton(typeof(IAdvanceInvoiceAndPaymentDeleteLinkEventsReader))]
    internal sealed class AdvanceInvoiceAndPaymentDeleteLinkEventsReader : MoedeloKafkaTopicReaderBase, IAdvanceInvoiceAndPaymentDeleteLinkEventsReader
    {
        public AdvanceInvoiceAndPaymentDeleteLinkEventsReader(
            IMoedeloKafkaTopicReaderBaseDependencies dependencies,
            ILogger<AdvanceInvoiceAndPaymentDeleteLinkEventsReader> logger)
            : base(
                dependencies,
                logger)
        {
        }

        public Task ReadAsync(
            string groupId,
            Func<AdvanceInvoiceAndPaymentDeleteLinkMessage, Task> onDelete,
            Func<AdvanceInvoiceAndPaymentDeleteLinkMessage, Exception, Task> onException = null,
            KafkaConsumerConfig.AutoOffsetResetType resetType = KafkaConsumerConfig.AutoOffsetResetType.Latest,
            int consumerCount = 1,
            CancellationToken? cancellationToken = null)
        {
            ValidateHandlerFunc(onDelete, nameof(onDelete));
            var settings = new ReadTopicSetting<AdvanceInvoiceAndPaymentDeleteLinkMessage>(
                groupId, 
                LinksTopics.Event.AdvanceInvoiceAndPaymentDeleteLinks, 
                onDelete, 
                onException,
                onFatalException: null,
                resetType: resetType, 
                consumersCount: consumerCount);
            return ReadTopicWithRetryAsync(settings, cancellationToken ?? CancellationToken.None);
        }
    }
}