using Moedelo.Common.Kafka.Abstractions.Base;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Infrastructure.Json;
using Moedelo.Money.Business.Kontragents;
using Moedelo.Money.Business.LinkedDocuments.Links;
using Moedelo.Money.Domain.PaymentOrders.Outgoing.CurrencyPaymentToSupplier;
using Moedelo.Money.Enums;
using Moedelo.Money.Kafka.Abstractions;
using Moedelo.Money.Kafka.Abstractions.PaymentOrders.Outgoing.CurrencyPaymentToSupplier.Events;
using Moedelo.Money.Kafka.Abstractions.Topics;
using System.Linq;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.PaymentOrders.Outgoing.CurrencyPaymentToSupplier
{
    [OperationType(OperationType.PaymentOrderOutgoingCurrencyPaymentToSupplier)]
    [InjectAsSingleton(typeof(CurrencyPaymentToSupplierEventWriter))]
    [InjectAsSingleton(typeof(IConcretePaymentOrderDeletedEventWriter))]
    internal sealed class CurrencyPaymentToSupplierEventWriter : MoedeloKafkaTopicWriterBase, IConcretePaymentOrderDeletedEventWriter
    {
        private readonly string topic = PaymentOrderTopics.CurrencyPaymentToSupplier.Event.CUD;
        private readonly ILinksReader linksReader;

        public CurrencyPaymentToSupplierEventWriter(
            IMoedeloKafkaTopicWriterBaseDependencies dependencies,
            ILinksReader linksReader)
            : base(dependencies)
        {
            this.linksReader = linksReader;
        }

        public Task WriteCreatedEventAsync(CurrencyPaymentToSupplierSaveRequest request)
        {
            var message = CurrencyPaymentToSupplierMapper.MapToCreatedMessage(request);
            var key = message.DocumentBaseId.ToString();
            var value = new CUDEventMessageValue
            {
                EventType = CUDEventType.Created,
                EventDataJson = message.ToJsonString(),
            };
            return WriteAsync(topic, key, value);
        }

        public Task WriteUpdatedEventAsync(CurrencyPaymentToSupplierSaveRequest request)
        {
            var message = CurrencyPaymentToSupplierMapper.MapToUpdatedMessage(request);
            var key = message.DocumentBaseId.ToString();
            var value = new CUDEventMessageValue
            {
                EventType = CUDEventType.Updated,
                EventDataJson = message.ToJsonString(),
            };
            return WriteAsync(topic, key, value);
        }

        public async Task WriteDeletedEventAsync(CurrencyPaymentToSupplierResponse response, long? newDocumentBaseId)
        {
            var linkedCurrencyInvoicesTask = await linksReader.GetLinksWithDocumentsAsync(response.DocumentBaseId);
            var linkedPurchasesCurrencyInvoicesIds = linkedCurrencyInvoicesTask.Select(ci => ci.Document.Id).ToArray();

            var message = new CurrencyPaymentToSupplierDeletedMessage
            {
                DocumentBaseId = response.DocumentBaseId,
                Date = response.Date,
                Number = response.Number,
                KontragentId = response.Kontragent?.Id ?? default,
                PurchasesCurrencyInvoicesIds = linkedPurchasesCurrencyInvoicesIds,
                NewDocumentBaseId = newDocumentBaseId,
            };
            var key = message.DocumentBaseId.ToString();
            var value = new CUDEventMessageValue
            {
                EventType = CUDEventType.Deleted,
                EventDataJson = message.ToJsonString(),
            };
            await WriteAsync(topic, key, value);
        }

        /// <summary>
        /// используется в технических целях
        /// в событии об удалении не заполняется никаких данных кроме documentBaseId 
        /// </summary>
        public async Task WriteDeletedEventAsync(long documentBaseId)
        {
            var linkedCurrencyInvoicesTask = await linksReader.GetLinksWithDocumentsAsync(documentBaseId);
            var linkedPurchasesCurrencyInvoicesIds = linkedCurrencyInvoicesTask.Select(ci => ci.Document.Id).ToArray();

            var message = new CurrencyPaymentToSupplierDeletedMessage
            {
                DocumentBaseId = documentBaseId,
                PurchasesCurrencyInvoicesIds = linkedPurchasesCurrencyInvoicesIds
            };
            var key = message.DocumentBaseId.ToString();
            var value = new CUDEventMessageValue
            {
                EventType = CUDEventType.Deleted,
                EventDataJson = message.ToJsonString(),
            };
            await WriteAsync(topic, key, value);
        }
    }
}
