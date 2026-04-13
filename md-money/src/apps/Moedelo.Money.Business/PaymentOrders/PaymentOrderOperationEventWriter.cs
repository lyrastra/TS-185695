using System;
using System.Threading.Tasks;
using Moedelo.Common.Kafka.Abstractions.Base;
using Moedelo.Common.Kafka.Abstractions.Entities.Events;
using Moedelo.Common.Kafka.Abstractions.Entities.Events.Builders;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Enums;
using Moedelo.Money.Kafka.Abstractions.PaymentOrders.Common.Events;
using Moedelo.Money.Kafka.Abstractions.Topics;

namespace Moedelo.Money.Business.PaymentOrders
{
    [InjectAsSingleton(typeof(PaymentOrderOperationEventWriter))]
    internal sealed class PaymentOrderOperationEventWriter : MoedeloKafkaTopicWriterBase
    {
        private readonly string Topic = MoneyTopics.PaymentOrders.Operation.Event.Topic;
        private readonly string EntityName = MoneyTopics.PaymentOrders.Operation.EntityName;

        private readonly IMoedeloEntityEventKafkaTopicWriter topicWriter;
        private readonly MoedeloEntityEventKafkaMessageDefinitionBuilder definitionBuilder;

        public PaymentOrderOperationEventWriter(
            IMoedeloEntityEventKafkaTopicWriter topicWriter,
            IMoedeloKafkaTopicWriterBaseDependencies dependencies)
            : base(dependencies)
        {
            this.topicWriter = topicWriter;

            definitionBuilder = MoedeloEntityEventKafkaMessageDefinitionBuilder.For(Topic, EntityName);
        }

        public Task WriteOperationTypeChangedEventAsync(
            long oldDocumentBaseId,
            OperationType oldOperationType,
            long newDocumentBaseId,
            OperationType newOperationType)
        {
            var eventData = new OperationTypeChanged
            {
                OldDocumentBaseId = oldDocumentBaseId,
                OldOperationType = oldOperationType,
                NewDocumentBaseId = newDocumentBaseId,
                NewOperationType = newOperationType
            };
            var eventDefinition = definitionBuilder.CreateEventDefinition(Guid.NewGuid().ToString(), eventData);
            return topicWriter.WriteEventDataAsync(eventDefinition);
        }
    }
}
