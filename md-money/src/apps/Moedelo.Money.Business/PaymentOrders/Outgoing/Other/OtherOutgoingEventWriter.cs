using Moedelo.Common.Kafka.Abstractions.Base;
using Moedelo.Common.Kafka.Abstractions.Entities.Events;
using Moedelo.Common.Kafka.Abstractions.Entities.Events.Builders;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Domain.PaymentOrders.Outgoing.Other;
using Moedelo.Money.Enums;
using Moedelo.Money.Kafka.Abstractions.PaymentOrders.Outgoing.Other.Events;
using Moedelo.Money.Kafka.Abstractions.Topics;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.PaymentOrders.Outgoing.Other
{
    [OperationType(OperationType.PaymentOrderOutgoingOther)]
    [InjectAsSingleton(typeof(OtherOutgoingEventWriter))]
    [InjectAsSingleton(typeof(IConcretePaymentOrderDeletedEventWriter))]
    internal sealed class OtherOutgoingEventWriter : MoedeloKafkaTopicWriterBase, IConcretePaymentOrderDeletedEventWriter
    {
        private readonly string Topic = MoneyTopics.PaymentOrders.OtherOutgoing.Event.Topic;
        private readonly string EntityName = MoneyTopics.PaymentOrders.OtherOutgoing.EntityName;

        private readonly IMoedeloEntityEventKafkaTopicWriter topicWriter;
        private readonly MoedeloEntityEventKafkaMessageDefinitionBuilder definitionBuilder;

        public OtherOutgoingEventWriter(
            IMoedeloEntityEventKafkaTopicWriter topicWriter,
            IMoedeloKafkaTopicWriterBaseDependencies dependencies)
            : base(dependencies)
        {
            this.topicWriter = topicWriter;

            definitionBuilder = MoedeloEntityEventKafkaMessageDefinitionBuilder.For(Topic, EntityName);
        }

        public Task WriteCreatedEventAsync(OtherOutgoingSaveRequest request)
        {
            var eventData = OtherOutgoingMapper.MapToCreatedMessage(request);
            var eventDefinition = definitionBuilder.CreateEventDefinition(eventData.DocumentBaseId.ToString(), eventData);
            return topicWriter.WriteEventDataAsync(eventDefinition);
        }

        public Task WriteUpdatedEventAsync(OtherOutgoingSaveRequest request)
        {
            var eventData = OtherOutgoingMapper.MapToUpdatedMessage(request);
            var eventDefinition = definitionBuilder.CreateEventDefinition(eventData.DocumentBaseId.ToString(), eventData);
            return topicWriter.WriteEventDataAsync(eventDefinition);
        }

        public Task WriteProvideRequiredEventAsync(OtherOutgoingResponse response)
        {
            var eventData = OtherOutgoingMapper.MapToProvideRequired(response);
            var eventDefinition = definitionBuilder.CreateEventDefinition(eventData.DocumentBaseId.ToString(), eventData);
            return topicWriter.WriteEventDataAsync(eventDefinition);
        }

        public Task WriteDeletedEventAsync(OtherOutgoingResponse response, long? newDocumentBaseId)
        {
            var eventData = OtherOutgoingMapper.MapToDeleted(response, newDocumentBaseId);
            var eventDefinition = definitionBuilder.CreateEventDefinition(eventData.DocumentBaseId.ToString(), eventData);
            return topicWriter.WriteEventDataAsync(eventDefinition);
        }

        /// <summary>
        /// используется в технических целях
        /// в событии об удалении не заполняется никаких данных кроме documentBaseId 
        /// </summary>
        public Task WriteDeletedEventAsync(long documentBaseId)
        {
            var eventData = new OtherOutgoingDeleted { DocumentBaseId = documentBaseId };
            var eventDefinition = definitionBuilder.CreateEventDefinition(eventData.DocumentBaseId.ToString(), eventData);
            return topicWriter.WriteEventDataAsync(eventDefinition);
        }
    }
}
