using Moedelo.Common.Audit.Abstractions.Interfaces;
using Moedelo.Common.ExecutionContext.Abstractions.Interfaces;
using Moedelo.Common.Kafka.Abstractions.Entities.Events;
using Moedelo.Common.Kafka.Abstractions.Entities.Events.Builders;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Kafka.Abstractions.PaymentOrders.Incoming.RefundFromAccountablePerson;
using Moedelo.Money.Kafka.Abstractions.PaymentOrders.Incoming.RefundFromAccountablePerson.Events;
using Moedelo.Money.Kafka.Abstractions.Topics;
using System;
using System.Threading.Tasks;

namespace Moedelo.Money.Kafka.PaymentOrders.Incoming.RefundFromAccountablePerson
{
    [InjectAsSingleton(typeof(IRefundFromAccountablePersonEventReaderBuilder))]
    public class RefundFromAccountablePersonEventReaderBuilder : MoedeloEntityEventKafkaTopicReaderBuilder, IRefundFromAccountablePersonEventReaderBuilder
    {
        public RefundFromAccountablePersonEventReaderBuilder(
            IMoedeloEntityEventKafkaTopicReader reader,
            IExecutionInfoContextInitializer contextInitializer,
            IExecutionInfoContextAccessor contextAccessor,
            IAuditTracer auditTracer)
            : base(
                  MoneyTopics.PaymentOrders.RefundFromAccountablePerson.Event.Topic,
                  MoneyTopics.PaymentOrders.RefundFromAccountablePerson.EntityName,
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

        public IRefundFromAccountablePersonEventReaderBuilder OnProvideRequired(Func<RefundFromAccountablePersonProvideRequired, Task> onEvent)
        {
            OnEvent(onEvent);
            return this;
        }
    }
}