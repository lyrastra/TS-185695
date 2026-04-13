using Moedelo.Common.Audit.Abstractions.Interfaces;
using Moedelo.Common.ExecutionContext.Abstractions.Interfaces;
using Moedelo.Common.Kafka.Abstractions.Entities.Events;
using Moedelo.Common.Kafka.Abstractions.Entities.Events.Builders;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Kafka.Abstractions.PaymentOrders.Outgoing.Other;
using Moedelo.Money.Kafka.Abstractions.PaymentOrders.Outgoing.Other.Events;
using Moedelo.Money.Kafka.Abstractions.Topics;
using System;
using System.Threading.Tasks;

namespace Moedelo.Money.Kafka.PaymentOrders.Outgoing.Other
{
    [InjectAsSingleton(typeof(IOtherOutgoingEventReaderBuilder))]
    public class OtherOutgoingEventReaderBuilder : MoedeloEntityEventKafkaTopicReaderBuilder, IOtherOutgoingEventReaderBuilder
    {
        public OtherOutgoingEventReaderBuilder(
            IMoedeloEntityEventKafkaTopicReader reader,
            IExecutionInfoContextInitializer contextInitializer,
            IExecutionInfoContextAccessor contextAccessor,
            IAuditTracer auditTracer)
            : base(
                  MoneyTopics.PaymentOrders.OtherOutgoing.Event.Topic,
                  MoneyTopics.PaymentOrders.OtherOutgoing.EntityName,
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

        public IOtherOutgoingEventReaderBuilder OnProvideRequired(Func<OtherOutgoingProvideRequired, Task> onEvent)
        {
            OnEvent(onEvent);
            return this;
        }
    }
}