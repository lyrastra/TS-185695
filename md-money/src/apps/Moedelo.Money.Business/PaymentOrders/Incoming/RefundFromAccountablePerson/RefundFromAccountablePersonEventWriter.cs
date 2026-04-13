using Moedelo.Common.Kafka.Abstractions.Base;
using Moedelo.Common.Kafka.Abstractions.Entities.Events;
using Moedelo.Common.Kafka.Abstractions.Entities.Events.Builders;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Domain.PaymentOrders.Incoming.RefundFromAccountablePerson;
using Moedelo.Money.Enums;
using Moedelo.Money.Kafka.Abstractions.PaymentOrders.Incoming.RefundFromAccountablePerson.Events;
using Moedelo.Money.Kafka.Abstractions.Topics;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.PaymentOrders.Incoming.RefundFromAccountablePerson
{
    [OperationType(OperationType.PaymentOrderIncomingRefundFromAccountablePerson)]
    [InjectAsSingleton(typeof(RefundFromAccountablePersonEventWriter))]
    [InjectAsSingleton(typeof(IConcretePaymentOrderDeletedEventWriter))]
    internal sealed class RefundFromAccountablePersonEventWriter : MoedeloKafkaTopicWriterBase, IConcretePaymentOrderDeletedEventWriter
    {
        private readonly string Topic = MoneyTopics.PaymentOrders.RefundFromAccountablePerson.Event.Topic;
        private readonly string EntityName = MoneyTopics.PaymentOrders.RefundFromAccountablePerson.EntityName;

        private readonly IMoedeloEntityEventKafkaTopicWriter topicWriter;
        private readonly MoedeloEntityEventKafkaMessageDefinitionBuilder definitionBuilder;

        public RefundFromAccountablePersonEventWriter(
            IMoedeloEntityEventKafkaTopicWriter topicWriter,
            IMoedeloKafkaTopicWriterBaseDependencies dependencies)
            : base(dependencies)
        {
            this.topicWriter = topicWriter;
            definitionBuilder = MoedeloEntityEventKafkaMessageDefinitionBuilder.For(Topic, EntityName);
        }

        public Task WriteCreatedEventAsync(RefundFromAccountablePersonSaveRequest request)
        {
            var eventData = RefundFromAccountablePersonMapper.MapToCreatedMessage(request);
            var eventDefinition = definitionBuilder.CreateEventDefinition(eventData.DocumentBaseId.ToString(), eventData);
            return topicWriter.WriteEventDataAsync(eventDefinition);
        }

        public Task WriteUpdatedEventAsync(RefundFromAccountablePersonSaveRequest request)
        {
            var eventData = RefundFromAccountablePersonMapper.MapToUpdatedMessage(request);
            var eventDefinition = definitionBuilder.CreateEventDefinition(eventData.DocumentBaseId.ToString(), eventData);
            return topicWriter.WriteEventDataAsync(eventDefinition);
        }

        public Task WriteProvideRequiredEventAsync(RefundFromAccountablePersonResponse response)
        {
            var eventData = RefundFromAccountablePersonMapper.MapToProvideRequiredEvent(response);
            var eventDefinition = definitionBuilder.CreateEventDefinition(eventData.DocumentBaseId.ToString(), eventData);
            return topicWriter.WriteEventDataAsync(eventDefinition);
        }

        public Task WriteDeletedEventAsync(RefundFromAccountablePersonResponse response, long? newDocumentBaseId)
        {
            var eventData = RefundFromAccountablePersonMapper.MapToDeletedMessage(response, newDocumentBaseId);
            var eventDefinition = definitionBuilder.CreateEventDefinition(eventData.DocumentBaseId.ToString(), eventData);
            return topicWriter.WriteEventDataAsync(eventDefinition);
        }

        /// <summary>
        /// используется в технических целях
        /// в событии об удалении не заполняется никаких данных кроме documentBaseId 
        /// </summary>
        public Task WriteDeletedEventAsync(long documentBaseId)
        {
            var eventData = new RefundFromAccountablePersonDeleted { DocumentBaseId = documentBaseId };
            var eventDefinition = definitionBuilder.CreateEventDefinition(eventData.DocumentBaseId.ToString(), eventData);
            return topicWriter.WriteEventDataAsync(eventDefinition);
        }
    }
}
