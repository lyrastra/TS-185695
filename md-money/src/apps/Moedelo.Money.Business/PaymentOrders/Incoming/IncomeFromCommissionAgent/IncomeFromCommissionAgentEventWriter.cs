using Moedelo.Common.Kafka.Abstractions.Base;
using Moedelo.Common.Kafka.Abstractions.Entities.Events;
using Moedelo.Common.Kafka.Abstractions.Entities.Events.Builders;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Domain.PaymentOrders.Incoming.IncomeFromCommissionAgent;
using Moedelo.Money.Enums;
using Moedelo.Money.Kafka.Abstractions.Topics;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.PaymentOrders.Incoming.IncomeFromCommissionAgent
{
    [OperationType(OperationType.PaymentOrderIncomingIncomeFromCommissionAgent)]
    [InjectAsSingleton(typeof(IncomeFromCommissionAgentEventWriter))]
    internal class IncomeFromCommissionAgentEventWriter : MoedeloKafkaTopicWriterBase
    {
        private readonly string Topic = MoneyTopics.PaymentOrders.IncomeFromCommissionAgent.Event.Topic;
        private readonly string EntityName = MoneyTopics.PaymentOrders.IncomeFromCommissionAgent.EntityName;

        private readonly IMoedeloEntityEventKafkaTopicWriter topicWriter;
        private readonly MoedeloEntityEventKafkaMessageDefinitionBuilder definitionBuilder;

        public IncomeFromCommissionAgentEventWriter(
            IMoedeloEntityEventKafkaTopicWriter topicWriter,
            IMoedeloKafkaTopicWriterBaseDependencies dependencies)
            : base(dependencies)
        {
            this.topicWriter = topicWriter;

            definitionBuilder = MoedeloEntityEventKafkaMessageDefinitionBuilder.For(Topic, EntityName);
        }

        public Task WriteCreatedEventAsync(IncomeFromCommissionAgentSaveRequest request)
        {
            var eventData = IncomeFromCommissionAgentMapper.MapToCreatedMessage(request);
            var eventDefinition = definitionBuilder.CreateEventDefinition(eventData.DocumentBaseId.ToString(), eventData);
            return topicWriter.WriteEventDataAsync(eventDefinition);
        }

        public Task WriteUpdatedEventAsync(IncomeFromCommissionAgentSaveRequest request)
        {
            var eventData = IncomeFromCommissionAgentMapper.MapToUpdatedMessage(request);
            var eventDefinition = definitionBuilder.CreateEventDefinition(eventData.DocumentBaseId.ToString(), eventData);
            return topicWriter.WriteEventDataAsync(eventDefinition);
        }

        public Task WriteDeletedEventAsync(IncomeFromCommissionAgentResponse response, long? newDocumentBaseId)
        {
            var eventData = IncomeFromCommissionAgentMapper.MapToDeletedMessage(response, newDocumentBaseId);
            var eventDefinition = definitionBuilder.CreateEventDefinition(eventData.DocumentBaseId.ToString(), eventData);
            return topicWriter.WriteEventDataAsync(eventDefinition);
        }
    }
}
