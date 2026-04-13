using Moedelo.Common.Kafka.Abstractions.Base;
using Moedelo.Common.Kafka.Abstractions.Entities.Events;
using Moedelo.Common.Kafka.Abstractions.Entities.Events.Builders;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Domain.PaymentOrders.Outgoing.LoanRepayment;
using Moedelo.Money.Enums;
using Moedelo.Money.Kafka.Abstractions.PaymentOrders.Outgoing.LoanRepayment.Events;
using Moedelo.Money.Kafka.Abstractions.Topics;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.PaymentOrders.Outgoing.LoanRepayment
{
    [OperationType(OperationType.PaymentOrderOutgoingLoanRepayment)]
    [InjectAsSingleton(typeof(LoanRepaymentEventWriter))]
    [InjectAsSingleton(typeof(IConcretePaymentOrderDeletedEventWriter))]
    internal sealed class LoanRepaymentEventWriter : MoedeloKafkaTopicWriterBase, IConcretePaymentOrderDeletedEventWriter
    {
        private readonly string Topic = MoneyTopics.PaymentOrders.LoanRepayment.Event.Topic;
        private readonly string EntityName = MoneyTopics.PaymentOrders.LoanRepayment.EntityName;

        private readonly IMoedeloEntityEventKafkaTopicWriter topicWriter;
        private readonly MoedeloEntityEventKafkaMessageDefinitionBuilder definitionBuilder;

        public LoanRepaymentEventWriter(
            IMoedeloEntityEventKafkaTopicWriter topicWriter,
            IMoedeloKafkaTopicWriterBaseDependencies dependencies)
            : base(dependencies)
        {
            this.topicWriter = topicWriter;

            definitionBuilder = MoedeloEntityEventKafkaMessageDefinitionBuilder.For(Topic, EntityName);
        }

        public Task WriteCreatedEventAsync(LoanRepaymentSaveRequest request)
        {
            var eventData = LoanRepaymentMapper.MapToCreatedMessage(request);
            var eventDefinition = definitionBuilder.CreateEventDefinition(eventData.DocumentBaseId.ToString(), eventData);
            return topicWriter.WriteEventDataAsync(eventDefinition);
        }

        public Task WriteUpdatedEventAsync(LoanRepaymentSaveRequest request)
        {
            var eventData = LoanRepaymentMapper.MapToUpdatedMessage(request);
            var eventDefinition = definitionBuilder.CreateEventDefinition(eventData.DocumentBaseId.ToString(), eventData);
            return topicWriter.WriteEventDataAsync(eventDefinition);
        }

        public Task WriteProvideRequiredEventAsync(LoanRepaymentResponse response)
        {
            var eventData = LoanRepaymentMapper.MapToProvideRequired(response);
            var eventDefinition = definitionBuilder.CreateEventDefinition(eventData.DocumentBaseId.ToString(), eventData);
            return topicWriter.WriteEventDataAsync(eventDefinition);
        }

        public Task WriteDeletedEventAsync(LoanRepaymentResponse response, long? newDocumentBaseId)
        {
            var eventData = LoanRepaymentMapper.MapToDeleted(response, newDocumentBaseId);
            var eventDefinition = definitionBuilder.CreateEventDefinition(eventData.DocumentBaseId.ToString(), eventData);
            return topicWriter.WriteEventDataAsync(eventDefinition);
        }

        /// <summary>
        /// используется в технических целях
        /// в событии об удалении не заполняется никаких данных кроме documentBaseId 
        /// </summary>
        public Task WriteDeletedEventAsync(long documentBaseId)
        {
            var eventData = new LoanRepaymentDeleted { DocumentBaseId = documentBaseId };
            var eventDefinition = definitionBuilder.CreateEventDefinition(eventData.DocumentBaseId.ToString(), eventData);
            return topicWriter.WriteEventDataAsync(eventDefinition);
        }
    }
}
