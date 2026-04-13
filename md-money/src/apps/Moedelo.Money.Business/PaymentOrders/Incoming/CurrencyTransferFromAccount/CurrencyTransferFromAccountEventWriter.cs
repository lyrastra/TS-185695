using Moedelo.Common.Kafka.Abstractions.Base;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Infrastructure.Json;
using Moedelo.Money.Domain.PaymentOrders.Incoming.CurrencyTransferFromAccount;
using Moedelo.Money.Enums;
using Moedelo.Money.Kafka.Abstractions;
using Moedelo.Money.Kafka.Abstractions.PaymentOrders.Incoming.CurrencyTransferFromAccount.Events;
using Moedelo.Money.Kafka.Abstractions.Topics;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.PaymentOrders.Incoming.CurrencyTransferFromAccount
{
    [OperationType(OperationType.PaymentOrderIncomingCurrencyTransferFromAccount)]
    [InjectAsSingleton(typeof(CurrencyTransferFromAccountEventWriter))]
    [InjectAsSingleton(typeof(IConcretePaymentOrderDeletedEventWriter))]
    internal sealed class CurrencyTransferFromAccountEventWriter : MoedeloKafkaTopicWriterBase, IConcretePaymentOrderDeletedEventWriter
    {
        private readonly string topic = PaymentOrderTopics.CurrencyTransferFromAccount.Event.CUD;

        public CurrencyTransferFromAccountEventWriter(
            IMoedeloKafkaTopicWriterBaseDependencies dependencies)
            : base(dependencies)
        {
        }

        public Task WriteCreatedEventAsync(CurrencyTransferFromAccountSaveRequest request)
        {
            var message = CurrencyTransferFromAccountMapper.MapToCreatedMessage(request);
            ValidateMessage(message);

            var key = message.DocumentBaseId.ToString();
            var value = new CUDEventMessageValue
            {
                EventType = CUDEventType.Created,
                EventDataJson = message.ToJsonString(),
            };

            return WriteAsync(topic, key, value);
        }

        public Task WriteUpdatedEventAsync(CurrencyTransferFromAccountSaveRequest request)
        {
            var message = CurrencyTransferFromAccountMapper.MapToUpdatedMessage(request);
            ValidateMessage(message);

            var key = message.DocumentBaseId.ToString();
            var value = new CUDEventMessageValue
            {
                EventType = CUDEventType.Updated,
                EventDataJson = message.ToJsonString(),
            };

            return WriteAsync(topic, key, value);
        }

        public Task WriteDeletedEventAsync(CurrencyTransferFromAccountResponse response, long? newDocumentBaseId)
        {
            var message = CurrencyTransferFromAccountMapper.MapToDeletedMessage(response, newDocumentBaseId);
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
            var message = new CurrencyTransferFromAccountCreatedMessage { DocumentBaseId = documentBaseId };

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