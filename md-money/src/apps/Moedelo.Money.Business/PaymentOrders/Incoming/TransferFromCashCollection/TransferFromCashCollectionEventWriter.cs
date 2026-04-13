using Moedelo.Common.Kafka.Abstractions.Base;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Infrastructure.Json;
using Moedelo.Money.Domain.PaymentOrders.Incoming.TransferFromCashCollection;
using Moedelo.Money.Enums;
using Moedelo.Money.Kafka.Abstractions;
using Moedelo.Money.Kafka.Abstractions.PaymentOrders.Incoming.TransferFromCashCollection.Events;
using Moedelo.Money.Kafka.Abstractions.Topics;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.PaymentOrders.Incoming.TransferFromCashCollection
{
    [OperationType(OperationType.MemorialWarrantTransferFromCashCollection)]
    [InjectAsSingleton(typeof(TransferFromCashCollectionEventWriter))]
    [InjectAsSingleton(typeof(IConcretePaymentOrderDeletedEventWriter))]
    internal sealed class TransferFromCashCollectionEventWriter : MoedeloKafkaTopicWriterBase, IConcretePaymentOrderDeletedEventWriter
    {
        private readonly string topic = PaymentOrderTopics.TransferFromCashCollection.Event.CUD;

        public TransferFromCashCollectionEventWriter(
            IMoedeloKafkaTopicWriterBaseDependencies dependencies)
            : base(dependencies)
        {
        }

        public Task WriteCreatedEventAsync(TransferFromCashCollectionSaveRequest request)
        {
            var message = TransferFromCashCollectionMapper.MapToCreatedMessage(request);
            ValidateMessage(message);

            var key = message.DocumentBaseId.ToString();
            var value = new CUDEventMessageValue
            {
                EventType = CUDEventType.Created,
                EventDataJson = message.ToJsonString(),
            };

            return WriteAsync(topic, key, value);
        }

        public Task WriteUpdatedEventAsync(TransferFromCashCollectionSaveRequest request)
        {
            var message = TransferFromCashCollectionMapper.MapToUpdatedMessage(request);
            ValidateMessage(message);

            var key = message.DocumentBaseId.ToString();
            var value = new CUDEventMessageValue
            {
                EventType = CUDEventType.Updated,
                EventDataJson = message.ToJsonString(),
            };

            return WriteAsync(topic, key, value);
        }

        public Task WriteDeletedEventAsync(TransferFromCashCollectionResponse response, long? newDocumentBaseId)
        {
            var message = TransferFromCashCollectionMapper.MapToDeletedMessage(response, newDocumentBaseId);

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
            var message = new TransferFromCashCollectionDeletedMessage { DocumentBaseId = documentBaseId };

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