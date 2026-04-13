using System.Threading;
using System.Threading.Tasks;
using Moedelo.Common.Kafka.Abstractions.Base;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Infrastructure.Json;
using Moedelo.Money.Domain.PaymentOrders.Outgoing.CurrencyOther;
using Moedelo.Money.Enums;
using Moedelo.Money.Kafka.Abstractions;
using Moedelo.Money.Kafka.Abstractions.PaymentOrders.Outgoing.CurrencyOther.Events;
using Moedelo.Money.Kafka.Abstractions.Topics;

namespace Moedelo.Money.Business.PaymentOrders.Outgoing.CurrencyOther
{
    [OperationType(OperationType.PaymentOrderOutgoingCurrencyOther)]
    [InjectAsSingleton(typeof(CurrencyOtherOutgoingEventWriter))]
    [InjectAsSingleton(typeof(IConcretePaymentOrderDeletedEventWriter))]
    internal sealed class CurrencyOtherOutgoingEventWriter : MoedeloKafkaTopicWriterBase, IConcretePaymentOrderDeletedEventWriter
    {
        private readonly string topic = PaymentOrderTopics.CurrencyOtherOutgoing.Event.CUD;
        
        public CurrencyOtherOutgoingEventWriter(
            IMoedeloKafkaTopicWriterBaseDependencies dependencies)
            : base(dependencies)
        {
        }
        
        public Task WriteCreatedEventAsync(CurrencyOtherOutgoingSaveRequest request)
        {
            var message = CurrencyOtherOutgoingMapper.MapToCreatedMessage(request);
            var key = message.DocumentBaseId.ToString();
            var value = new CUDEventMessageValue
            {
                EventType = CUDEventType.Created,
                EventDataJson = message.ToJsonString(),
            };
            return WriteAsync(topic, key, value, CancellationToken.None);
        }

        public Task WriteUpdatedEventAsync(CurrencyOtherOutgoingSaveRequest request)
        {
            var message = CurrencyOtherOutgoingMapper.MapToUpdatedMessage(request);
            var key = message.DocumentBaseId.ToString();
            var value = new CUDEventMessageValue
            {
                EventType = CUDEventType.Updated,
                EventDataJson = message.ToJsonString(),
            };
            return WriteAsync(topic, key, value, CancellationToken.None);
        }

        public Task WriteDeletedEventAsync(CurrencyOtherOutgoingResponse response, long? newDocumentBaseId)
        {
            var message = CurrencyOtherOutgoingMapper.MapToDeletedMessage(response, newDocumentBaseId);
            var key = message.DocumentBaseId.ToString();
            var value = new CUDEventMessageValue
            {
                EventType = CUDEventType.Deleted,
                EventDataJson = message.ToJsonString(),
            };
            return WriteAsync(topic, key, value, CancellationToken.None);
        }

        /// <summary>
        /// используется в технических целях
        /// в событии об удалении не заполняется никаких данных кроме documentBaseId 
        /// </summary>
        public Task WriteDeletedEventAsync(long documentBaseId)
        {
            var message = new CurrencyOtherOutgoingDeletedMessage
            {
                DocumentBaseId = documentBaseId
            };
            var key = message.DocumentBaseId.ToString();
            var value = new CUDEventMessageValue
            {
                EventType = CUDEventType.Deleted,
                EventDataJson = message.ToJsonString(),
            };
            return WriteAsync(topic, key, value, CancellationToken.None);
        }
    }
}