using Moedelo.Common.Kafka.Abstractions.Base;
using Moedelo.Common.Kafka.Abstractions.Entities.Events;
using Moedelo.Common.Kafka.Abstractions.Entities.Events.Builders;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.Abstractions.CashOrders.Outgoing.UnifiedBudgetaryPayment;
using Moedelo.Money.Domain.CashOrders.Outgoing.UnifiedBudgetaryPayment;
using Moedelo.Money.Enums;
using Moedelo.Money.Kafka.Abstractions.Topics;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.CashOrders.Outgoing.UnifiedBudgetaryPayment
{
    //[OperationType(OperationType.CashOrderOutgoingUnifiedBudgetaryPayment)]
    [InjectAsSingleton(typeof(IUnifiedBudgetaryPaymentEventWriter))]
    //[InjectAsSingleton(typeof(IConcretePaymentOrderDeletedEventWriter))]
    internal class UnifiedBudgetaryPaymentEventWriter : MoedeloKafkaTopicWriterBase, IUnifiedBudgetaryPaymentEventWriter//, IConcretePaymentOrderDeletedEventWriter
    {
        private readonly string Topic = MoneyTopics.CashOrders.UnifiedBudgetaryPayment.Event.Topic;
        private readonly string EntityName = MoneyTopics.CashOrders.UnifiedBudgetaryPayment.EntityName;

        private readonly IMoedeloEntityEventKafkaTopicWriter topicWriter;
        private readonly MoedeloEntityEventKafkaMessageDefinitionBuilder definitionBuilder;

        public UnifiedBudgetaryPaymentEventWriter(
            IMoedeloEntityEventKafkaTopicWriter topicWriter,
            IMoedeloKafkaTopicWriterBaseDependencies dependencies)
            : base(dependencies)
        {
            this.topicWriter = topicWriter;

            definitionBuilder = MoedeloEntityEventKafkaMessageDefinitionBuilder.For(Topic, EntityName);
        }

        public Task WriteCreatedEventAsync(UnifiedBudgetaryPaymentSaveRequest request)
        {
            var eventData = UnifiedBudgetaryPaymentMapper.MapToCreatedMessage(request);
            var eventDefinition = definitionBuilder.CreateEventDefinition(eventData.DocumentBaseId.ToString(), eventData);
            return topicWriter.WriteEventDataAsync(eventDefinition);
        }

        public Task WriteUpdatedEventAsync(
            UnifiedBudgetaryPaymentSaveRequest request,
            OperationType oldOperationType,
            IReadOnlyCollection<long> deletedSubPaymentDocumentIds)
        {
            var eventData = UnifiedBudgetaryPaymentMapper.MapToUpdatedMessage(request, oldOperationType, deletedSubPaymentDocumentIds);
            var eventDefinition = definitionBuilder.CreateEventDefinition(eventData.DocumentBaseId.ToString(), eventData);
            return topicWriter.WriteEventDataAsync(eventDefinition);
        }

        //public Task WriteProvideRequiredEventAsync(UnifiedBudgetaryPaymentResponse response)
        //{
        //    var eventData = UnifiedBudgetaryPaymentMapper.MapToProvideRequired(response);
        //    var eventDefinition = definitionBuilder.CreateEventDefinition(eventData.DocumentBaseId.ToString(), eventData);
        //    return topicWriter.WriteEventDataAsync(eventDefinition);
        //}

        public Task WriteDeletedEventAsync(
            UnifiedBudgetaryPaymentResponse response,
            IReadOnlyCollection<long> deletedSubPaymentDocumentIds)
        {
            var eventData = UnifiedBudgetaryPaymentMapper.MapToDeleted(response, deletedSubPaymentDocumentIds);
            var eventDefinition = definitionBuilder.CreateEventDefinition(eventData.DocumentBaseId.ToString(), eventData);
            return topicWriter.WriteEventDataAsync(eventDefinition);
        }

        /// <summary>
        /// используется в технических целях
        /// в событии об удалении не заполняется никаких данных кроме documentBaseId 
        /// </summary>
        //public Task WriteDeletedEventAsync(long documentBaseId)
        //{
        //    var eventData = new UnifiedBudgetaryPaymentDeleted { DocumentBaseId = documentBaseId };
        //    var eventDefinition = definitionBuilder.CreateEventDefinition(eventData.DocumentBaseId.ToString(), eventData);
        //    return topicWriter.WriteEventDataAsync(eventDefinition);
        //}
    }
}
