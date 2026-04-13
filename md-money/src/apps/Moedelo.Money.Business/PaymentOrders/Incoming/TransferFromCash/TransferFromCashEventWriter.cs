using Moedelo.Common.Kafka.Abstractions.Base;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Infrastructure.Json;
using Moedelo.Money.Domain.PaymentOrders.Incoming.TransferFromCash;
using Moedelo.Money.Enums;
using Moedelo.Money.Kafka.Abstractions;
using Moedelo.Money.Kafka.Abstractions.PaymentOrders.Incoming.TransferFromCash.Events;
using Moedelo.Money.Kafka.Abstractions.Topics;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.PaymentOrders.Incoming.TransferFromCash
{
    [OperationType(OperationType.MemorialWarrantTransferFromCash)]
    [InjectAsSingleton(typeof(TransferFromCashEventWriter))]
    [InjectAsSingleton(typeof(IConcretePaymentOrderDeletedEventWriter))]
    internal sealed class TransferFromCashEventWriter : MoedeloKafkaTopicWriterBase, IConcretePaymentOrderDeletedEventWriter
    {
        private readonly string topic = PaymentOrderTopics.TransferFromCash.Event.CUD;

        public TransferFromCashEventWriter(
            IMoedeloKafkaTopicWriterBaseDependencies dependencies)
            : base(dependencies)
        {
        }

        public Task WriteCreatedEventAsync(TransferFromCashSaveRequest request)
        {
            var message = TransferFromCashMapper.MapToCreatedMessage(request);
            ValidateMessage(message);

            var key = message.DocumentBaseId.ToString();
            var value = new CUDEventMessageValue
            {
                EventType = CUDEventType.Created,
                EventDataJson = message.ToJsonString(),
            };

            return WriteAsync(topic, key, value);
        }

        public Task WriteUpdatedEventAsync(TransferFromCashSaveRequest request)
        {
            var message = TransferFromCashMapper.MapToUpdatedMessage(request);
            ValidateMessage(message);

            var key = message.DocumentBaseId.ToString();
            var value = new CUDEventMessageValue
            {
                EventType = CUDEventType.Updated,
                EventDataJson = message.ToJsonString(),
            };

            return WriteAsync(topic, key, value);
        }

        public Task WriteDeletedEventAsync(TransferFromCashResponse response, long? newDocumentBaseId)
        {
            var message = TransferFromCashMapper.MapToDeletedMessage(response, newDocumentBaseId);

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
            var message = new TransferFromCashDeletedMessage { DocumentBaseId = documentBaseId };

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