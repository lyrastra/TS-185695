using Moedelo.Common.Kafka.Abstractions.Base;
using Moedelo.Common.Kafka.Abstractions.Entities.Events;
using Moedelo.Common.Kafka.Abstractions.Entities.Events.Builders;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Outgoing.BudgetaryPayment;
using Moedelo.Money.Domain.PaymentOrders.Outgoing.BudgetaryPayment;
using Moedelo.Money.Enums;
using Moedelo.Money.Kafka.Abstractions.PaymentOrders.Outgoing.BudgetaryPayment.Events;
using Moedelo.Money.Kafka.Abstractions.Topics;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.PaymentOrders.Outgoing.BudgetaryPayment
{
    [OperationType(OperationType.BudgetaryPayment)]
    [InjectAsSingleton(typeof(IBudgetaryPaymentEventWriter))]
    [InjectAsSingleton(typeof(IConcretePaymentOrderDeletedEventWriter))]
    internal class BudgetaryPaymentEventWriter : MoedeloKafkaTopicWriterBase, IBudgetaryPaymentEventWriter, IConcretePaymentOrderDeletedEventWriter
    {
        private readonly string Topic = MoneyTopics.PaymentOrders.BudgetaryPayment.Event.Topic;
        private readonly string EntityName = MoneyTopics.PaymentOrders.BudgetaryPayment.EntityName;

        private readonly IMoedeloEntityEventKafkaTopicWriter topicWriter;
        private readonly MoedeloEntityEventKafkaMessageDefinitionBuilder definitionBuilder;

        public BudgetaryPaymentEventWriter(
            IMoedeloEntityEventKafkaTopicWriter topicWriter,
            IMoedeloKafkaTopicWriterBaseDependencies dependencies)
            : base(dependencies)
        {
            this.topicWriter = topicWriter;

            definitionBuilder = MoedeloEntityEventKafkaMessageDefinitionBuilder.For(Topic, EntityName);
        }

        public Task WriteCreatedEventAsync(BudgetaryPaymentSaveRequest request)
        {
            var eventData = BudgetaryPaymentMapper.MapToCreatedMessage(request);
            var eventDefinition = definitionBuilder.CreateEventDefinition(eventData.DocumentBaseId.ToString(), eventData);
            return topicWriter.WriteEventDataAsync(eventDefinition);
        }

        public Task WriteUpdatedEventAsync(BudgetaryPaymentSaveRequest request)
        {
            var eventData = BudgetaryPaymentMapper.MapToUpdatedMessage(request);
            var eventDefinition = definitionBuilder.CreateEventDefinition(eventData.DocumentBaseId.ToString(), eventData);
            return topicWriter.WriteEventDataAsync(eventDefinition);
        }

        public Task WriteProvideRequiredEventAsync(BudgetaryPaymentResponse response)
        {
            var eventData = BudgetaryPaymentMapper.MapToProvideRequired(response);
            var eventDefinition = definitionBuilder.CreateEventDefinition(eventData.DocumentBaseId.ToString(), eventData);
            return topicWriter.WriteEventDataAsync(eventDefinition);
        }

        public Task WriteDeletedEventAsync(BudgetaryPaymentResponse response, long? newDocumentBaseId)
        {
            var eventData = BudgetaryPaymentMapper.MapToDeleted(response, newDocumentBaseId);
            var eventDefinition = definitionBuilder.CreateEventDefinition(eventData.DocumentBaseId.ToString(), eventData);
            return topicWriter.WriteEventDataAsync(eventDefinition);
        }

        /// <summary>
        /// используется в технических целях
        /// в событии об удалении не заполняется никаких данных кроме documentBaseId 
        /// </summary>
        public Task WriteDeletedEventAsync(long documentBaseId)
        {
            var eventData = new BudgetaryPaymentDeleted { DocumentBaseId = documentBaseId };
            var eventDefinition = definitionBuilder.CreateEventDefinition(eventData.DocumentBaseId.ToString(), eventData);
            return topicWriter.WriteEventDataAsync(eventDefinition);
        }

        public Task WriteUpdateAfterAccountingStatementCreatedEventAsync(BudgetaryPaymentAfterAccountingStatementCreatedUpdateRequest request)
        {
            var eventData = new BudgetaryPaymentUpdateAfterAccountingStatementCreated
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
