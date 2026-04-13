using Moedelo.Common.Kafka.Abstractions.Base;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Infrastructure.Json;
using Moedelo.Money.Domain.PaymentOrders.Incoming.TransferFromPurse;
using Moedelo.Money.Enums;
using Moedelo.Money.Kafka.Abstractions;
using Moedelo.Money.Kafka.Abstractions.PaymentOrders.Incoming.TransferFromPurse.Events;
using Moedelo.Money.Kafka.Abstractions.Topics;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.PaymentOrders.Incoming.TransferFromPurse
{
    [OperationType(OperationType.PaymentOrderIncomingTransferFromPurse)]
    [InjectAsSingleton(typeof(TransferFromPurseEventWriter))]
    [InjectAsSingleton(typeof(IConcretePaymentOrderDeletedEventWriter))]
    internal sealed class TransferFromPurseEventWriter : MoedeloKafkaTopicWriterBase, IConcretePaymentOrderDeletedEventWriter
    {
        private readonly string topic = PaymentOrderTopics.TransferFromPurse.Event.CUD;

        public TransferFromPurseEventWriter(
            IMoedeloKafkaTopicWriterBaseDependencies dependencies)
            : base(dependencies)
        {
        }

        public Task WriteCreatedEventAsync(TransferFromPurseSaveRequest request)
        {
            var message = TransferFromPurseMapper.MapToCreatedMessage(request);
            ValidateMessage(message);

            var key = message.DocumentBaseId.ToString();
            var value = new CUDEventMessageValue
            {
                EventType = CUDEventType.Created,
                EventDataJson = message.ToJsonString(),
            };

            return WriteAsync(topic, key, value);
        }

        public Task WriteUpdatedEventAsync(TransferFromPurseSaveRequest request)
        {
            var message = TransferFromPurseMapper.MapToUpdatedMessage(request);
            ValidateMessage(message);

            var key = message.DocumentBaseId.ToString();
            var value = new CUDEventMessageValue
            {
                EventType = CUDEventType.Updated,
                EventDataJson = message.ToJsonString(),
            };

            return WriteAsync(topic, key, value);
        }

        public Task WriteDeletedEventAsync(TransferFromPurseResponse response, long? newDocumentBaseId)
        {
            var message = TransferFromPurseMapper.MapToDeletedMessage(response, newDocumentBaseId);

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
            var message = new TransferFromPurseDeletedMessage { DocumentBaseId = documentBaseId };

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