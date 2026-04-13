using Moedelo.Common.Kafka.Abstractions.Base;
using Moedelo.Common.Kafka.Abstractions.Entities.Events;
using Moedelo.Common.Kafka.Abstractions.Entities.Events.Builders;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Domain.PaymentOrders.Outgoing.PaymentToAccountablePerson;
using Moedelo.Money.Enums;
using Moedelo.Money.Kafka.Abstractions.PaymentOrders.Outgoing.PaymentToAccountablePerson.Events;
using Moedelo.Money.Kafka.Abstractions.Topics;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.PaymentOrders.Outgoing.PaymentToAccountablePerson
{
    [OperationType(OperationType.PaymentOrderOutgoingPaymentToAccountablePerson)]
    [InjectAsSingleton(typeof(PaymentToAccountablePersonEventWriter))]
    [InjectAsSingleton(typeof(IConcretePaymentOrderDeletedEventWriter))]
    internal sealed class PaymentToAccountablePersonEventWriter : MoedeloKafkaTopicWriterBase, IConcretePaymentOrderDeletedEventWriter
    {
        private readonly string Topic = MoneyTopics.PaymentOrders.PaymentToAccountablePerson.Event.Topic;
        private readonly string EntityName = MoneyTopics.PaymentOrders.PaymentToAccountablePerson.EntityName;

        private readonly IMoedeloEntityEventKafkaTopicWriter topicWriter;
        private readonly MoedeloEntityEventKafkaMessageDefinitionBuilder definitionBuilder;

        public PaymentToAccountablePersonEventWriter(
            IMoedeloEntityEventKafkaTopicWriter topicWriter,
            IMoedeloKafkaTopicWriterBaseDependencies dependencies)
            : base(dependencies)
        {
            this.topicWriter = topicWriter;

            definitionBuilder = MoedeloEntityEventKafkaMessageDefinitionBuilder.For(Topic, EntityName);
        }

        public Task WriteCreatedEventAsync(PaymentToAccountablePersonSaveRequest request)
        {
            var eventData = PaymentToAccountablePersonMapper.MapToCreatedMessage(request);
            var eventDefinition = definitionBuilder.CreateEventDefinition(eventData.DocumentBaseId.ToString(), eventData);
            return topicWriter.WriteEventDataAsync(eventDefinition);
        }

        public Task WriteUpdatedEventAsync(PaymentToAccountablePersonSaveRequest request)
        {
            var eventData = PaymentToAccountablePersonMapper.MapToUpdatedMessage(request);
            var eventDefinition = definitionBuilder.CreateEventDefinition(eventData.DocumentBaseId.ToString(), eventData);
            return topicWriter.WriteEventDataAsync(eventDefinition);
        }

        public Task WriteProvideRequiredEventAsync(PaymentToAccountablePersonResponse response)
        {
            var eventData = PaymentToAccountablePersonMapper.MapToProvideRequired(response);
            if (eventData.IsManualTaxPostings && eventData.AdvanceStatementBaseIds.Count > 0)
            {
                // костыль для сброса режима проведения ну "вручную"
                eventData.IsManualTaxPostings = false;
            }
            var eventDefinition = definitionBuilder.CreateEventDefinition(eventData.DocumentBaseId.ToString(), eventData);
            return topicWriter.WriteEventDataAsync(eventDefinition);
        }

        public Task WriteDeletedEventAsync(PaymentToAccountablePersonResponse response, long? newDocumentBaseId)
        {
            var eventData = PaymentToAccountablePersonMapper.MapToDeleted(response, newDocumentBaseId);
            var eventDefinition = definitionBuilder.CreateEventDefinition(eventData.DocumentBaseId.ToString(), eventData);
            return topicWriter.WriteEventDataAsync(eventDefinition);
        }

        /// <summary>
        /// используется в технических целях
        /// в событии об удалении не заполняется никаких данных кроме documentBaseId 
        /// </summary>
        public Task WriteDeletedEventAsync(long documentBaseId)
        {
            var eventData = new PaymentToAccountablePersonDeleted { DocumentBaseId = documentBaseId };
            var eventDefinition = definitionBuilder.CreateEventDefinition(eventData.DocumentBaseId.ToString(), eventData);
            return topicWriter.WriteEventDataAsync(eventDefinition);
        }
    }
}
