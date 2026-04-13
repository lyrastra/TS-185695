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
    [InjectAsSingleton(typeof(ICurrencyPaymentToSupplierAndCurrencyInvoiceChangeLinkEventsReader))]
    internal sealed class CurrencyPaymentToSupplierAndCurrencyInvoiceChangeLinkEventsReader : MoedeloKafkaTopicReaderBase, ICurrencyPaymentToSupplierAndCurrencyInvoiceChangeLinkEventsReader
    {
        public CurrencyPaymentToSupplierAndCurrencyInvoiceChangeLinkEventsReader(
            IMoedeloKafkaTopicReaderBaseDependencies dependencies,
            ILogger<PurchasesCurrencyInvoiceAndBudgetaryPaymentChangeLinkEventsReader> logger)
            : base(
                dependencies,
                logger)
        {
        }

        public Task ReadAsync(
            string groupId,
            Func<CurrencyPaymentToSupplierAndCurrencyInvoiceChangeLinkMessage, Task> onChange,
            Func<CurrencyPaymentToSupplierAndCurrencyInvoiceChangeLinkMessage, Exception, Task> onException = null,
            KafkaConsumerConfig.AutoOffsetResetType resetType = KafkaConsumerConfig.AutoOffsetResetType.Latest,
            int consumerCount = 1,
            CancellationToken? cancellationToken = null)
        {
            ValidateHandlerFunc(onChange, nameof(onChange));
            var settings = new ReadTopicSetting<CurrencyPaymentToSupplierAndCurrencyInvoiceChangeLinkMessage>(
                groupId,
                LinksTopics.Event.CurrencyPaymentToSupplierAndPurchasesCurrencyInvoiceChangeLinks,
                onChange,
                onException,
                onFatalException: null,
                resetType: resetType,
                consumersCount: consumerCount);

            return ReadTopicWithRetryAsync(settings, cancellationToken ?? CancellationToken.None);
        }
    }
}