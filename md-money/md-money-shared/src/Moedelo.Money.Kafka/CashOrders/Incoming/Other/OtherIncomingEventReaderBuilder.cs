using Moedelo.Common.Audit.Abstractions.Interfaces;
using Moedelo.Common.ExecutionContext.Abstractions.Interfaces;
using Moedelo.Common.Kafka.Abstractions.Entities.Events;
using Moedelo.Common.Kafka.Abstractions.Entities.Events.Builders;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Kafka.Abstractions.CashOrders.Incoming.Other;
using Moedelo.Money.Kafka.Abstractions.CashOrders.Incoming.Other.Events;
using Moedelo.Money.Kafka.Abstractions.Topics;
using System;
using System.Threading.Tasks;

namespace Moedelo.Money.Kafka.CashOrders.Incoming.Other
{
    [InjectAsSingleton(typeof(IOtherIncomingEventReaderBuilder))]
    class OtherIncomingEventReaderBuilder : MoedeloEntityEventKafkaTopicReaderBuilder, IOtherIncomingEventReaderBuilder
    {
        public OtherIncomingEventReaderBuilder(
            IMoedeloEntityEventKafkaTopicReader reader,
            IExecutionInfoContextInitializer contextInitializer,
            IExecutionInfoContextAccessor contextAccessor,
            IAuditTracer auditTracer)
            : base(
                MoneyTopics.CashOrders.OtherIncoming.Event.Topic,
                MoneyTopics.CashOrders.OtherIncoming.EntityName,
                reader,
                contextInitializer,
                contextAccessor,
                auditTracer)
        {
        }

        public IOtherIncomingEventReaderBuilder OnCreated(Func<OtherIncomingCreated, Task> onEvent)
        {
            OnEvent(onEvent);
            return this;
        }

        public IOtherIncomingEventReaderBuilder OnUpdated(Func<OtherIncomingUpdated, Task> onEvent)
        {
            OnEvent(onEvent);
            return this;
        }

        public IOtherIncomingEventReaderBuilder OnDeleted(Func<OtherIncomingDeleted, Task> onEvent)
        {
            OnEvent(onEvent);
            return this;
        }
    }
}
