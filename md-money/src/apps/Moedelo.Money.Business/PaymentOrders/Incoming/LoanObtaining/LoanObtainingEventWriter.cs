using Moedelo.Common.Kafka.Abstractions.Base;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Infrastructure.Json;
using Moedelo.Money.Domain.PaymentOrders.Incoming.LoanObtaining;
using Moedelo.Money.Enums;
using Moedelo.Money.Kafka.Abstractions;
using Moedelo.Money.Kafka.Abstractions.PaymentOrders.Incoming.LoanObtaining.Events;
using Moedelo.Money.Kafka.Abstractions.Topics;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.PaymentOrders.Incoming.LoanObtaining
{
    [OperationType(OperationType.PaymentOrderIncomingLoanObtaining)]
    [InjectAsSingleton(typeof(LoanObtainingEventWriter))]
    [InjectAsSingleton(typeof(IConcretePaymentOrderDeletedEventWriter))]
    internal sealed class LoanObtainingEventWriter : MoedeloKafkaTopicWriterBase, IConcretePaymentOrderDeletedEventWriter
    {
        private readonly string topic = PaymentOrderTopics.LoanObtaining.Event.CUD;

        public LoanObtainingEventWriter(
            IMoedeloKafkaTopicWriterBaseDependencies dependencies)
            : base(dependencies)
        {
        }

        public Task WriteCreatedEventAsync(LoanObtainingSaveRequest request)
        {
            var message = LoanObtainingMapper.MapToCreatedMessage(request);
            var key = message.DocumentBaseId.ToString();
            var value = new CUDEventMessageValue
            {
                EventType = CUDEventType.Created,
                EventDataJson = message.ToJsonString(),
            };
            return WriteAsync(topic, key, value);
        }

        public Task WriteUpdatedEventAsync(LoanObtainingSaveRequest request)
        {
            var message = LoanObtainingMapper.MapToUpdatedMessage(request);
            var key = message.DocumentBaseId.ToString();
            var value = new CUDEventMessageValue
            {
                EventType = CUDEventType.Updated,
                EventDataJson = message.ToJsonString(),
            };
            return WriteAsync(topic, key, value);
        }

        public Task WriteDeletedEventAsync(LoanObtainingResponse response, long? newDocumentBaseId)
        {
            var message = LoanObtainingMapper.MapToDeletedMessage(response, newDocumentBaseId);
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
            var message = new LoanObtainingDeletedMessage { DocumentBaseId = documentBaseId };
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
