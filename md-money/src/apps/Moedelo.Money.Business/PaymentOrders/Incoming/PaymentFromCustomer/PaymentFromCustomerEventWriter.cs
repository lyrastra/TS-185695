using Moedelo.Common.Kafka.Abstractions.Base;
using Moedelo.Common.Kafka.Abstractions.Entities.Events;
using Moedelo.Common.Kafka.Abstractions.Entities.Events.Builders;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Incoming.PaymentFromCustomer;
using Moedelo.Money.Domain.PaymentOrders.Incoming.PaymentFromCustomer;
using Moedelo.Money.Enums;
using Moedelo.Money.Kafka.Abstractions.PaymentOrders.Incoming.PaymentFromCustomer.Events;
using Moedelo.Money.Kafka.Abstractions.Topics;
using System.Threading.Tasks;
using Moedelo.Money.Domain.LinkedDocuments;
using Moedelo.Money.Business.PaymentOrders.Providing;

namespace Moedelo.Money.Business.PaymentOrders.Incoming.PaymentFromCustomer
{
    [OperationType(OperationType.PaymentOrderIncomingPaymentFromCustomer)]
    [InjectAsSingleton(typeof(IPaymentFromCustomerEventWriter))]
    [InjectAsSingleton(typeof(IConcretePaymentOrderDeletedEventWriter))]
    internal class PaymentFromCustomerEventWriter : MoedeloKafkaTopicWriterBase, IPaymentFromCustomerEventWriter, IConcretePaymentOrderDeletedEventWriter
    {
        private readonly string Topic = MoneyTopics.PaymentOrders.PaymentFromCustomer.Event.Topic;
        private readonly string EntityName = MoneyTopics.PaymentOrders.PaymentFromCustomer.EntityName;
        
        private readonly IMoedeloEntityEventKafkaTopicWriter topicWriter;
        private readonly MoedeloEntityEventKafkaMessageDefinitionBuilder definitionBuilder;
        private readonly PaymentOrderProvidingStateSetter providingStateSetter;

        public PaymentFromCustomerEventWriter(
            IMoedeloEntityEventKafkaTopicWriter topicWriter,
            IMoedeloKafkaTopicWriterBaseDependencies dependencies,
            PaymentOrderProvidingStateSetter providingStateSetter)
            : base(dependencies)
        {
            this.topicWriter = topicWriter;
            this.providingStateSetter = providingStateSetter;
            definitionBuilder = MoedeloEntityEventKafkaMessageDefinitionBuilder.For(Topic, EntityName);
        }

        public async Task WriteCreatedEventAsync(PaymentFromCustomerSaveRequest request)
        {
            var eventData = PaymentFromCustomerMapper.MapToCreatedMessage(request);
            eventData.ProvidingStateId = await providingStateSetter.SetStateAsync(request.DocumentBaseId);
            var eventDefinition = definitionBuilder.CreateEventDefinition(eventData.DocumentBaseId.ToString(), eventData);
            await topicWriter.WriteEventDataAsync(eventDefinition);
        }

        public async Task WriteUpdatedEventAsync(PaymentFromCustomerSaveRequest request)
        {
            var eventData = PaymentFromCustomerMapper.MapToUpdatedMessage(request);
            eventData.ProvidingStateId = await providingStateSetter.SetStateAsync(request.DocumentBaseId);
            var eventDefinition = definitionBuilder.CreateEventDefinition(eventData.DocumentBaseId.ToString(), eventData);
            await topicWriter.WriteEventDataAsync(eventDefinition);
        }

        public async Task WriteProvideRequiredEventAsync(PaymentFromCustomerResponse response)
        {
            var eventData = PaymentFromCustomerMapper.MapToProvideRequiredEvent(response);
            eventData.ProvidingStateId = await providingStateSetter.SetStateAsync(response.DocumentBaseId);
            var eventDefinition = definitionBuilder.CreateEventDefinition(eventData.DocumentBaseId.ToString(), eventData);
            await topicWriter.WriteEventDataAsync(eventDefinition);
        }

        public Task WriteDeletedEventAsync(PaymentFromCustomerResponse response, long? newDocumentBaseId)
        {
            var eventData = PaymentFromCustomerMapper.MapToDeleted(response, newDocumentBaseId);
            var eventDefinition = definitionBuilder.CreateEventDefinition(eventData.DocumentBaseId.ToString(), eventData);
            return topicWriter.WriteEventDataAsync(eventDefinition);
        }

        /// <summary>
        /// используется в технических целях
        /// в событии об удалении не заполняется никаких данных кроме documentBaseId 
        /// </summary>
        public Task WriteDeletedEventAsync(long documentBaseId)
        {
            var eventData = new PaymentFromCustomerDeleted { DocumentBaseId = documentBaseId };
            var eventDefinition = definitionBuilder.CreateEventDefinition(eventData.DocumentBaseId.ToString(), eventData);
            return topicWriter.WriteEventDataAsync(eventDefinition);
        }

        public Task WriteSetReserveEventAsync(SetReserveRequest request)
        {
            var eventData = new PaymentFromCustomerSetReserve
            {
                DocumentBaseId = request.DocumentBaseId,
                ReserveSum = request.ReserveSum
            };
            var eventDefinition = definitionBuilder.CreateEventDefinition(eventData.DocumentBaseId.ToString(), eventData);
            return topicWriter.WriteEventDataAsync(eventDefinition);
        }
    }
}
