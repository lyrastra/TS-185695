using System;
using System.Threading.Tasks;
using Moedelo.Common.Audit.Abstractions.Interfaces;
using Moedelo.Common.ExecutionContext.Abstractions.Interfaces;
using Moedelo.Common.Kafka.Abstractions.Entities.Events;
using Moedelo.Common.Kafka.Abstractions.Entities.Events.Builders;
using Moedelo.Estate.Kafka.Abstractions.EventReaders.InventoryCard;
using Moedelo.Estate.Kafka.Abstractions.Events.EventData.InventoryCard;
using Moedelo.Estate.Kafka.Abstractions.Events.Topics;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;

namespace Moedelo.Estate.Kafka.EventReaders.InventoryCard
{
    [InjectAsSingleton(typeof(IInventoryCardEventReaderBuilder))]
    public class InventoryCardEventReaderBuilder : MoedeloEntityEventKafkaTopicReaderBuilder, IInventoryCardEventReaderBuilder
    {
        public InventoryCardEventReaderBuilder(
            IMoedeloEntityEventKafkaTopicReader reader,
            IExecutionInfoContextInitializer contextInitializer,
            IExecutionInfoContextAccessor contextAccessor,
            IAuditTracer auditTracer)
            : base(
                EstateTopics.InventoryCard.CUD,
                EstateTopics.InventoryCard.EntityName,
                reader,
                contextInitializer,
                contextAccessor,
                auditTracer)
        {
        }

        public IInventoryCardEventReaderBuilder OnCreated(Func<InventoryCardCreatedMessage, Task> onEvent)
        {
            OnEvent(onEvent);

            return this;
        }

        public IInventoryCardEventReaderBuilder OnUpdated(Func<InventoryCardUpdatedMessage, Task> onEvent)
        {
            OnEvent(onEvent);

            return this;
        }

        public IInventoryCardEventReaderBuilder OnDeleted(Func<InventoryCardDeletedMessage, Task> onEvent)
        {
            OnEvent(onEvent);

            return this;
        }
    }
}