using Moedelo.Common.Audit.Abstractions.Interfaces;
using Moedelo.Common.ExecutionContext.Abstractions.Interfaces;
using Moedelo.Common.Kafka.Abstractions.Entities.Events;
using Moedelo.Common.Kafka.Abstractions.Entities.Events.Builders;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Kafka.Abstractions.CashOrders.Outgoing.Other;
using Moedelo.Money.Kafka.Abstractions.CashOrders.Outgoing.Other.Events;
using Moedelo.Money.Kafka.Abstractions.Topics;
using System;
using System.Threading.Tasks;

namespace Moedelo.Money.Kafka.CashOrders.Outgoing.Other
{
    [InjectAsSingleton(typeof(IOtherOutgoingEventReaderBuilder))]
    class OtherOutgoingEventReaderBuilder : MoedeloEntityEventKafkaTopicReaderBuilder, IOtherOutgoingEventReaderBuilder
    {
        public OtherOutgoingEventReaderBuilder(
            IMoedeloEntityEventKafkaTopicReader reader,
            IExecutionInfoContextInitializer contextInitializer,
            IExecutionInfoContextAccessor contextAccessor,
            IAuditTracer auditTracer)
            : base(
                MoneyTopics.CashOrders.OtherOutgoing.Event.Topic,
                MoneyTopics.CashOrders.OtherOutgoing.EntityName,
                reader,
                contextInitializer,
                contextAccessor,
                auditTracer)
        {
        }

        public IOtherOutgoingEventReaderBuilder OnCreated(Func<OtherOutgoingCreated, Task> onEvent)
        {
            OnEvent(onEvent);
            return this;
        }

        public IOtherOutgoingEventReaderBuilder OnUpdated(Func<OtherOutgoingUpdated, Task> onEvent)
        {
            OnEvent(onEvent);
            return this;
        }

        public IOtherOutgoingEventReaderBuilder OnDeleted(Func<OtherOutgoingDeleted, Task> onEvent)
        {
            OnEvent(onEvent);
            return this;
        }
    }
}
