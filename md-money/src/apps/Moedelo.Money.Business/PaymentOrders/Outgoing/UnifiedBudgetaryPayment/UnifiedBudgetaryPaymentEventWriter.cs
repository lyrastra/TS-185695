using Moedelo.Common.Kafka.Abstractions.Base;
using Moedelo.Common.Kafka.Abstractions.Entities.Events;
using Moedelo.Common.Kafka.Abstractions.Entities.Events.Builders;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Outgoing.UnifiedBudgetaryPayment;
using Moedelo.Money.Domain.PaymentOrders.Outgoing.UnifiedBudgetaryPayment;
using Moedelo.Money.Enums;
using Moedelo.Money.Kafka.Abstractions.PaymentOrders.Outgoing.UnifiedBudgetaryPayment.Events;
using Moedelo.Money.Kafka.Abstractions.Topics;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.PaymentOrders.Outgoing.UnifiedBudgetaryPayment
{
    [OperationType(OperationType.PaymentOrderOutgoingUnifiedBudgetaryPayment)]
    [InjectAsSingleton(typeof(IUnifiedBudgetaryPaymentEventWriter))]
    [InjectAsSingleton(typeof(IConcretePaymentOrderDeletedEventWriter))]
    internal class UnifiedBudgetaryPaymentEventWriter : MoedeloKafkaTopicWriterBase, IUnifiedBudgetaryPaymentEventWriter, IConcretePaymentOrderDeletedEventWriter
    {
        private readonly string Topic = MoneyTopics.PaymentOrders.UnifiedBudgetaryPayment.Event.Topic;
        private readonly string EntityName = MoneyTopics.PaymentOrders.UnifiedBudgetaryPayment.EntityName;

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
            IReadOnlyCollection<long> deletedSubPaymentDocumentIds)
        {
            var eventData = UnifiedBudgetaryPaymentMapper.MapToUpdatedMessage(request, deletedSubPaymentDocumentIds);
            var eventDefinition = definitionBuilder.CreateEventDefinition(eventData.DocumentBaseId.ToString(), eventData);
            return topicWriter.WriteEventDataAsync(eventDefinition);
        }

        public Task WriteProvideRequiredEventAsync(UnifiedBudgetaryPaymentResponse model)
        {
            var eventData = UnifiedBudgetaryPaymentMapper.MapToProvideRequired(model);
            var eventDefinition = definitionBuilder.CreateEventDefinition(eventData.DocumentBaseId.ToString(), eventData);
            return topicWriter.WriteEventDataAsync(eventDefinition);
        }

        public Task WriteDeletedEventAsync(UnifiedBudgetaryPaymentResponse model, IReadOnlyCollection<long> deletedSubPaymentDocumentIds, long? newDocumentBaseId)
        {
            var eventData = UnifiedBudgetaryPaymentMapper.MapToDeleted(model, deletedSubPaymentDocumentIds, newDocumentBaseId);
            var eventDefinition = definitionBuilder.CreateEventDefinition(eventData.DocumentBaseId.ToString(), eventData);
            return topicWriter.WriteEventDataAsync(eventDefinition);
        }

        /// <summary>
        /// используется в технических целях
        /// в событии об удалении не заполняется никаких данных кроме documentBaseId 
        /// </summary>
        public Task WriteDeletedEventAsync(long documentBaseId)
        {
            var eventData = new UnifiedBudgetaryPaymentDeleted { DocumentBaseId = documentBaseId };
            var eventDefinition = definitionBuilder.CreateEventDefinition(eventData.DocumentBaseId.ToString(), eventData);
            return topicWriter.WriteEventDataAsync(eventDefinition);
        }

        public Task WriteUpdateAfterAccountingStatementCreatedEventAsync(UnifiedBudgetaryPaymentAfterAccountingStatementCreatedUpdateRequest request)
        {
            var eventData = new UnifiedBudgetaryPaymentUpdateAfterAccountingStatementCreated
            {
                DocumentBaseId = request.DocumentBaseId,
                AccountingStatementDate = request.AccountingStatementDate,
                AccountingStatementSum = request.AccountingStatementSum,
                AccountingStatementBaseId = request.AccountingStatementBaseId
            };
            var eventDefinition = definitionBuilder.CreateEventDefinition(eventData.DocumentBaseId.ToString(), eventData);
            return topicWriter.WriteEventDataAsync(eventDefinition);
        }
    }
}
