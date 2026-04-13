using Moedelo.Common.Kafka.Abstractions.Base;
using Moedelo.Common.Kafka.Abstractions.Entities.Events;
using Moedelo.Common.Kafka.Abstractions.Entities.Events.Builders;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Outgoing.PaymentToSupplier;
using Moedelo.Money.Domain.PaymentOrders.Outgoing.PaymentToSupplier;
using Moedelo.Money.Enums;
using Moedelo.Money.Kafka.Abstractions.PaymentOrders.Outgoing.PaymentToSupplier.Events;
using Moedelo.Money.Kafka.Abstractions.Topics;
using System.Threading.Tasks;
using Moedelo.Money.Domain.LinkedDocuments;
using Moedelo.Money.Business.PaymentOrders.Providing;

namespace Moedelo.Money.Business.PaymentOrders.Outgoing.PaymentToSupplier
{
    [OperationType(OperationType.PaymentOrderOutgoingPaymentToSupplier)]
    [InjectAsSingleton(typeof(IPaymentToSupplierEventWriter))]
    [InjectAsSingleton(typeof(IConcretePaymentOrderDeletedEventWriter))]
    internal class PaymentToSupplierEventWriter : MoedeloKafkaTopicWriterBase, IPaymentToSupplierEventWriter, IConcretePaymentOrderDeletedEventWriter
    {
        private readonly string Topic = MoneyTopics.PaymentOrders.PaymentToSupplier.Event.Topic;
        private readonly string EntityName = MoneyTopics.PaymentOrders.PaymentToSupplier.EntityName;

        private readonly IMoedeloEntityEventKafkaTopicWriter topicWriter;
        private readonly MoedeloEntityEventKafkaMessageDefinitionBuilder definitionBuilder;
        private readonly PaymentOrderProvidingStateSetter providingStateSetter;

        public PaymentToSupplierEventWriter(
            IMoedeloEntityEventKafkaTopicWriter topicWriter,
            IMoedeloKafkaTopicWriterBaseDependencies dependencies, 
            PaymentOrderProvidingStateSetter providingStateSetter)
            : base(dependencies)
        {
            this.topicWriter = topicWriter;
            this.providingStateSetter = providingStateSetter;

            definitionBuilder = MoedeloEntityEventKafkaMessageDefinitionBuilder.For(Topic, EntityName);
        }

        public async Task WriteCreatedEventAsync(PaymentToSupplierSaveRequest request)
        {
            var eventData = PaymentToSupplierMapper.MapToCreatedMessage(request);
            eventData.ProvidingStateId = await providingStateSetter.SetStateAsync(request.DocumentBaseId);
            var eventDefinition = definitionBuilder.CreateEventDefinition(eventData.DocumentBaseId.ToString(), eventData);
            await topicWriter.WriteEventDataAsync(eventDefinition);
        }

        public async Task WriteUpdatedEventAsync(PaymentToSupplierSaveRequest request)
        {
            var eventData = PaymentToSupplierMapper.MapToUpdatedMessage(request);
            eventData.ProvidingStateId = await providingStateSetter.SetStateAsync(request.DocumentBaseId);
            var eventDefinition = definitionBuilder.CreateEventDefinition(eventData.DocumentBaseId.ToString(), eventData);
            await topicWriter.WriteEventDataAsync(eventDefinition);
        }

        public async Task WriteProvideRequiredEventAsync(PaymentToSupplierResponse response)
        {
            var eventData = PaymentToSupplierMapper.MapToProvideRequired(response);
            eventData.ProvidingStateId = await providingStateSetter.SetStateAsync(response.DocumentBaseId);
            var eventDefinition = definitionBuilder.CreateEventDefinition(eventData.DocumentBaseId.ToString(), eventData);
            await topicWriter.WriteEventDataAsync(eventDefinition);
        }

        public Task WriteDeletedEventAsync(PaymentToSupplierResponse response, long? newDocumentBaseId)
        {
            var eventData = PaymentToSupplierMapper.MapToDeleted(response, newDocumentBaseId);
            var eventDefinition = definitionBuilder.CreateEventDefinition(eventData.DocumentBaseId.ToString(), eventData);
            return topicWriter.WriteEventDataAsync(eventDefinition);
        }

        /// <summary>
        /// используется в технических целях
        /// в событии об удалении не заполняется никаких данных кроме documentBaseId 
        /// </summary>
        public Task WriteDeletedEventAsync(long documentBaseId)
        {
            var eventData = new PaymentToSupplierDeleted { DocumentBaseId = documentBaseId };
            var eventDefinition = definitionBuilder.CreateEventDefinition(eventData.DocumentBaseId.ToString(), eventData);
            return topicWriter.WriteEventDataAsync(eventDefinition);
        }

        public Task WriteSetReserveEventAsync(SetReserveRequest request)
        {
            var eventData = new PaymentToSupplierSetReserve
            {
                DocumentBaseId = request.DocumentBaseId,
                ReserveSum = request.ReserveSum
            };
            var eventDefinition = definitionBuilder.CreateEventDefinition(eventData.DocumentBaseId.ToString(), eventData);
            return topicWriter.WriteEventDataAsync(eventDefinition);
        }
    }
}
