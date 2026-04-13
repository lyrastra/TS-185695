using Moedelo.Common.Audit.Abstractions.Interfaces;
using Moedelo.Common.ExecutionContext.Abstractions.Interfaces;
using Moedelo.Common.Kafka.Abstractions.Entities.Events;
using Moedelo.Common.Kafka.Abstractions.Entities.Events.Builders;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Kafka.Abstractions.CashOrders.Outgoing.CashCollection.Events;
using Moedelo.Money.Kafka.Abstractions.CashOrders.Outgoing.CashCollection;
using Moedelo.Money.Kafka.Abstractions.Topics;
using System;
using System.Threading.Tasks;

namespace Moedelo.Money.Kafka.CashOrders.Outgoing.CashCollection
{
    [InjectAsSingleton(typeof(ICashCollectionEventReaderBuilder))]
    class CashCollectionEventReaderBuilder : MoedeloEntityEventKafkaTopicReaderBuilder, ICashCollectionEventReaderBuilder
    {
        public CashCollectionEventReaderBuilder(
            IMoedeloEntityEventKafkaTopicReader reader,
            IExecutionInfoContextInitializer contextInitializer,
            IExecutionInfoContextAccessor contextAccessor,
            IAuditTracer auditTracer)
            : base(
                MoneyTopics.CashOrders.CashCollection.Event.Topic,
                MoneyTopics.CashOrders.CashCollection.EntityName,
                reader,
                contextInitializer,
                contextAccessor,
                auditTracer)
        {
        }

        public ICashCollectionEventReaderBuilder OnCreated(Func<CashCollectionCreated, Task> onEvent)
        {
            OnEvent(onEvent);
            return this;
        }

        public ICashCollectionEventReaderBuilder OnUpdated(Func<CashCollectionUpdated, Task> onEvent)
        {
            OnEvent(onEvent);
            return this;
        }

        public ICashCollectionEventReaderBuilder OnDeleted(Func<CashCollectionDeleted, Task> onEvent)
        {
            OnEvent(onEvent);
            return this;
        }
    }
}
