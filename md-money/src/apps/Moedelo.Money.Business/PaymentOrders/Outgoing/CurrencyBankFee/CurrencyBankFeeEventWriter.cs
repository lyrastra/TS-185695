using Moedelo.Common.Kafka.Abstractions.Base;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Infrastructure.Json;
using Moedelo.Money.Domain.PaymentOrders.Outgoing.CurrencyBankFee;
using Moedelo.Money.Enums;
using Moedelo.Money.Kafka.Abstractions;
using Moedelo.Money.Kafka.Abstractions.PaymentOrders.Outgoing.CurrencyBankFee.Events;
using Moedelo.Money.Kafka.Abstractions.Topics;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.PaymentOrders.Outgoing.CurrencyBankFee
{
    [OperationType(OperationType.CurrencyBankFee)]
    [InjectAsSingleton(typeof(CurrencyBankFeeEventWriter))]
    [InjectAsSingleton(typeof(IConcretePaymentOrderDeletedEventWriter))]
    internal sealed class CurrencyBankFeeEventWriter : MoedeloKafkaTopicWriterBase, IConcretePaymentOrderDeletedEventWriter
    {
        private readonly string topic = PaymentOrderTopics.CurrencyBankFee.Event.CUD;

        public CurrencyBankFeeEventWriter(
            IMoedeloKafkaTopicWriterBaseDependencies dependencies)
            : base(dependencies)
        {
        }

        public Task WriteCreatedEventAsync(CurrencyBankFeeSaveRequest request)
        {
            var message = CurrencyBankFeeMapper.MapToCreatedMessage(request);
            var key = message.DocumentBaseId.ToString();
            var value = new CUDEventMessageValue
            {
                EventType = CUDEventType.Created,
                EventDataJson = message.ToJsonString(),
            };
            return WriteAsync(topic, key, value);
        }

        public Task WriteUpdatedEventAsync(CurrencyBankFeeSaveRequest request)
        {
            var message = CurrencyBankFeeMapper.MapToUpdatedMessage(request);
            var key = message.DocumentBaseId.ToString();
            var value = new CUDEventMessageValue
            {
                EventType = CUDEventType.Updated,
                EventDataJson = message.ToJsonString(),
            };
            return WriteAsync(topic, key, value);
        }

        public Task WriteDeletedEventAsync(CurrencyBankFeeResponse response, long? newDocumentBaseId)
        {
            var message = CurrencyBankFeeMapper.MapToDeletedMessage(response, newDocumentBaseId);
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
            var message = new CurrencyBankFeeDeletedMessage
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