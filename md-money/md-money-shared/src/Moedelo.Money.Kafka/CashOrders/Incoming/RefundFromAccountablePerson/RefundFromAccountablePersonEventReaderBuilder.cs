using Moedelo.Common.Audit.Abstractions.Interfaces;
using Moedelo.Common.ExecutionContext.Abstractions.Interfaces;
using Moedelo.Common.Kafka.Abstractions.Entities.Events;
using Moedelo.Common.Kafka.Abstractions.Entities.Events.Builders;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Kafka.Abstractions.CashOrders.Incoming.RefundFromAccountablePerson;
using Moedelo.Money.Kafka.Abstractions.CashOrders.Incoming.RefundFromAccountablePerson.Events;
using Moedelo.Money.Kafka.Abstractions.Topics;
using System;
using System.Threading.Tasks;

namespace Moedelo.Money.Kafka.CashOrders.Incoming.RefundFromAccountablePerson
{
    [InjectAsSingleton(typeof(IRefundFromAccountablePersonEventReaderBuilder))]
    class RefundFromAccountablePersonEventReaderBuilder : MoedeloEntityEventKafkaTopicReaderBuilder, IRefundFromAccountablePersonEventReaderBuilder
    {
        public RefundFromAccountablePersonEventReaderBuilder(
            IMoedeloEntityEventKafkaTopicReader reader,
            IExecutionInfoContextInitializer contextInitializer,
            IExecutionInfoContextAccessor contextAccessor,
            IAuditTracer auditTracer)
            : base(
                MoneyTopics.CashOrders.RefundFromAccountablePerson.Event.Topic,
                MoneyTopics.CashOrders.RefundFromAccountablePerson.EntityName,
                reader,
                contextInitializer,
                contextAccessor,
                auditTracer)
        {
        }

        public IRefundFromAccountablePersonEventReaderBuilder OnCreated(Func<RefundFromAccountablePersonCreated, Task> onEvent)
        {
            OnEvent(onEvent);
            return this;
        }

        public IRefundFromAccountablePersonEventReaderBuilder OnUpdated(Func<RefundFromAccountablePersonUpdated, Task> onEvent)
        {
            OnEvent(onEvent);
            return this;
        }

        public IRefundFromAccountablePersonEventReaderBuilder OnDeleted(Func<RefundFromAccountablePersonDeleted, Task> onEvent)
        {
            OnEvent(onEvent);
            return this;
        }

        public IRefundFromAccountablePersonEventReaderBuilder OnProvided(Func<RefundFromAccountablePersonProvided, Task> onEvent)
        {
            OnEvent(onEvent);
            return this;
        }
    }
}
