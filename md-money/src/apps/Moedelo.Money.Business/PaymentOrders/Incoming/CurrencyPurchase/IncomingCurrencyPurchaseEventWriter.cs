using Moedelo.Common.Kafka.Abstractions.Base;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Infrastructure.Json;
using Moedelo.Money.Domain.PaymentOrders.Incoming.CurrencyPurchase;
using Moedelo.Money.Enums;
using Moedelo.Money.Kafka.Abstractions;
using Moedelo.Money.Kafka.Abstractions.PaymentOrders.Incoming.CurrencyPurchase.Events;
using Moedelo.Money.Kafka.Abstractions.Topics;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.PaymentOrders.Incoming.CurrencyPurchase
{
    [OperationType(OperationType.PaymentOrderIncomingCurrencyPurchase)]
    [InjectAsSingleton(typeof(IncomingCurrencyPurchaseEventWriter))]
    [InjectAsSingleton(typeof(IConcretePaymentOrderDeletedEventWriter))]
    internal sealed class IncomingCurrencyPurchaseEventWriter : MoedeloKafkaTopicWriterBase, IConcretePaymentOrderDeletedEventWriter
    {
        private readonly string topic = PaymentOrderTopics.IncomingCurrencyPurchase.Event.CUD;

        public IncomingCurrencyPurchaseEventWriter(
            IMoedeloKafkaTopicWriterBaseDependencies dependencies)
            : base(dependencies)
        {
        }

        public Task WriteCreatedEventAsync(IncomingCurrencyPurchaseSaveRequest request)
        {
            var message = MapToCreatedMessage(request);
            var key = message.DocumentBaseId.ToString();
            var value = new CUDEventMessageValue
            {
                EventType = CUDEventType.Created,
                EventDataJson = message.ToJsonString()
            };
            return WriteAsync(topic, key, value);
        }

        private static IncomingCurrencyPurchaseCreatedMessage MapToCreatedMessage(IncomingCurrencyPurchaseSaveRequest request)
        {
            return new IncomingCurrencyPurchaseCreatedMessage
            {
                Sum = request.Sum,
                Date = request.Date,
                Number = request.Number,
                Description = request.Description,
                DocumentBaseId = request.DocumentBaseId,
                SettlementAccountId = request.SettlementAccountId,
                FromSettlementAccountId = request.FromSettlementAccountId.GetValueOrDefault(),
                OperationState = request.OperationState,
                ProvideInAccounting = request.ProvideInAccounting,
                OutsourceState = request.OutsourceState,
                ImportRuleIds = request.ImportRuleIds,
                ImportLogId = request.ImportLogId,
            };
        }

        public Task WriteUpdatedEventAsync(IncomingCurrencyPurchaseSaveRequest request)
        {
            var message = MapToUpdatedMessage(request);
            var key = message.DocumentBaseId.ToString();
            var value = new CUDEventMessageValue
            {
                EventType = CUDEventType.Updated,
                EventDataJson = message.ToJsonString()
            };
            return WriteAsync(topic, key, value);
        }

        private static IncomingCurrencyPurchaseUpdatedMessage MapToUpdatedMessage(IncomingCurrencyPurchaseSaveRequest request)
        {
            return new IncomingCurrencyPurchaseUpdatedMessage
            {
                Sum = request.Sum,
                Date = request.Date,
                Number = request.Number,
                Description = request.Description,
                DocumentBaseId = request.DocumentBaseId,
                SettlementAccountId = request.SettlementAccountId,
                FromSettlementAccountId = request.FromSettlementAccountId.GetValueOrDefault(),
                ProvideInAccounting = request.ProvideInAccounting,
                OutsourceState = request.OutsourceState,
                OperationState = request.OperationState,
            };
        }

        public Task WriteDeletedEventAsync(IncomingCurrencyPurchaseResponse response, long? newDocumentBaseId)
        {
            var message = MapToDeletedMessage(response, newDocumentBaseId);
            var key = message.DocumentBaseId.ToString();
            var value = new CUDEventMessageValue
            {
                EventType = CUDEventType.Deleted,
                EventDataJson = message.ToJsonString(),
            };
            return WriteAsync(topic, key, value);
        }

        private static IncomingCurrencyPurchaseDeletedMessage MapToDeletedMessage(IncomingCurrencyPurchaseResponse response, long? newDocumentBaseId)
        {
            return new IncomingCurrencyPurchaseDeletedMessage
            {
                DocumentBaseId = response.DocumentBaseId,
                Date = response.Date,
                Number = response.Number,
                NewDocumentBaseId = newDocumentBaseId,
            };
        }

        /// <summary>
        /// используется в технических целях
        /// в событии об удалении не заполняется никаких данных кроме documentBaseId 
        /// </summary>
        public Task WriteDeletedEventAsync(long documentBaseId)
        {
            var message = new IncomingCurrencyPurchaseDeletedMessage
            {
                DocumentBaseId = documentBaseId
            };
            var key = message.DocumentBaseId.ToString();
            var value = new CUDEventMessageValue
            {
                EventType = CUDEventType.Deleted,
                EventDataJson = message.ToJsonString()
            };
            return WriteAsync(topic, key, value);
        }
    }
}