using Moedelo.Common.Kafka.Abstractions.Base;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Infrastructure.Json;
using Moedelo.Money.Domain.PaymentOrders.Incoming.CurrencySale;
using Moedelo.Money.Enums;
using Moedelo.Money.Kafka.Abstractions;
using Moedelo.Money.Kafka.Abstractions.PaymentOrders.Incoming.CurrencySale.Events;
using Moedelo.Money.Kafka.Abstractions.Topics;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.PaymentOrders.Incoming.CurrencySale
{
    [OperationType(OperationType.PaymentOrderIncomingCurrencySale)]
    [InjectAsSingleton(typeof(IncomingCurrencySaleEventWriter))]
    [InjectAsSingleton(typeof(IConcretePaymentOrderDeletedEventWriter))]
    internal sealed class IncomingCurrencySaleEventWriter : MoedeloKafkaTopicWriterBase, IConcretePaymentOrderDeletedEventWriter
    {
        private readonly string topic = PaymentOrderTopics.IncomingCurrencySale.Event.CUD;

        public IncomingCurrencySaleEventWriter(
            IMoedeloKafkaTopicWriterBaseDependencies dependencies)
            : base(dependencies)
        {
        }

        public Task WriteCreatedEventAsync(IncomingCurrencySaleSaveRequest request)
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

        private static IncomingCurrencySaleCreatedMessage MapToCreatedMessage(IncomingCurrencySaleSaveRequest request)
        {
            return new IncomingCurrencySaleCreatedMessage
            {
                Sum = request.Sum,
                Date = request.Date,
                Number = request.Number,
                Description = request.Description,
                DocumentBaseId = request.DocumentBaseId,
                SettlementAccountId = request.SettlementAccountId,
                FromSettlementAccountId = request.FromSettlementAccountId,
                ProvideInAccounting = request.ProvideInAccounting,
                OperationState = request.OperationState,
                OutsourceState = request.OutsourceState,
                ImportRuleIds = request.ImportRuleIds,
                ImportLogId = request.ImportLogId,
            };
        }

        public Task WriteUpdatedEventAsync(IncomingCurrencySaleSaveRequest request)
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

        private static IncomingCurrencySaleUpdatedMessage MapToUpdatedMessage(IncomingCurrencySaleSaveRequest request)
        {
            return new IncomingCurrencySaleUpdatedMessage
            {
                Sum = request.Sum,
                Date = request.Date,
                Number = request.Number,
                Description = request.Description,
                DocumentBaseId = request.DocumentBaseId,
                SettlementAccountId = request.SettlementAccountId,
                FromSettlementAccountId = request.FromSettlementAccountId ?? 0,
                ProvideInAccounting = request.ProvideInAccounting,
                OperationState = request.OperationState,
                OutsourceState = request.OutsourceState,
            };
        }

        public Task WriteDeletedEventAsync(IncomingCurrencySaleResponse response, long? newDocumentBaseId)
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

        private static IncomingCurrencySaleDeletedMessage MapToDeletedMessage(IncomingCurrencySaleResponse response, long? newDocumentBaseId)
        {
            return new IncomingCurrencySaleDeletedMessage
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
            var message = new IncomingCurrencySaleDeletedMessage
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