using Moedelo.Common.Kafka.Abstractions.Base;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Infrastructure.Json;
using Moedelo.Money.Domain.PaymentOrders.Outgoing.CurrencyTransferToAccount;
using Moedelo.Money.Enums;
using Moedelo.Money.Kafka.Abstractions;
using Moedelo.Money.Kafka.Abstractions.PaymentOrders.Outgoing.CurrencyTransferToAccount.Events;
using Moedelo.Money.Kafka.Abstractions.Topics;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.PaymentOrders.Outgoing.CurrencyTransferToAccount
{
    [OperationType(OperationType.PaymentOrderOutgoingCurrencyTransferToAccount)]
    [InjectAsSingleton(typeof(CurrencyTransferToAccountEventWriter))]
    [InjectAsSingleton(typeof(IConcretePaymentOrderDeletedEventWriter))]
    internal sealed class CurrencyTransferToAccountEventWriter : MoedeloKafkaTopicWriterBase, IConcretePaymentOrderDeletedEventWriter
    {
        private readonly string topic = PaymentOrderTopics.CurrencyTransferToAccount.Event.CUD;

        public CurrencyTransferToAccountEventWriter(
            IMoedeloKafkaTopicWriterBaseDependencies dependencies)
            : base(dependencies)
        {
        }

        public Task WriteCreatedEventAsync(CurrencyTransferToAccountSaveRequest request)
        {
            var message = CurrencyTransferToAccountMapper.MapToCreatedMessage(request);
            var key = message.DocumentBaseId.ToString();
            var value = new CUDEventMessageValue
            {
                EventType = CUDEventType.Created,
                EventDataJson = message.ToJsonString(),
            };
            return WriteAsync(topic, key, value);
        }

        public Task WriteUpdatedEventAsync(CurrencyTransferToAccountSaveRequest request)
        {
            var message = CurrencyTransferToAccountMapper.MapToUpdatedMessage(request);
            var key = message.DocumentBaseId.ToString();
            var value = new CUDEventMessageValue
            {
                EventType = CUDEventType.Updated,
                EventDataJson = message.ToJsonString(),
            };
            return WriteAsync(topic, key, value);
        }

        public Task WriteDeletedEventAsync(CurrencyTransferToAccountResponse response, long? newDocumentBaseId)
        {
            var message = CurrencyTransferToAccountMapper.MapToDeletedMessage(response, newDocumentBaseId);
            var key = message.DocumentBaseId.ToString();
            var value = new CUDEventMessageValue
            {
                EventType = CUDEventType.Deleted,
                EventDataJson = message.ToJsonString(),
            };
            return WriteAsync(topic, key, value);
        }

        /// <summary>
        /// используется в технических целях
        /// в событии об удалении не заполняется никаких данных кроме documentBaseId 
        /// </summary>
        public Task WriteDeletedEventAsync(long documentBaseId)
        {
            var message = new CurrencyTransferToAccountDeletedMessage
            {
                DocumentBaseId = documentBaseId
            };
            var key = message.DocumentBaseId.ToString();
            var value = new CUDEventMessageValue
            {
                EventType = CUDEventType.Deleted,
                EventDataJson = message.ToJsonString(),
            };
            return WriteAsync(topic, key, value);
        }
    }
}
