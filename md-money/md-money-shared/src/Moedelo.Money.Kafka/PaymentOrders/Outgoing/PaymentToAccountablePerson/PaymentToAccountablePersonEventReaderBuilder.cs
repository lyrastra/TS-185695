using Moedelo.Common.Audit.Abstractions.Interfaces;
using Moedelo.Common.ExecutionContext.Abstractions.Interfaces;
using Moedelo.Common.Kafka.Abstractions.Entities.Events;
using Moedelo.Common.Kafka.Abstractions.Entities.Events.Builders;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Kafka.Abstractions.PaymentOrders.Outgoing.PaymentToAccountablePerson;
using Moedelo.Money.Kafka.Abstractions.PaymentOrders.Outgoing.PaymentToAccountablePerson.Events;
using Moedelo.Money.Kafka.Abstractions.Topics;
using System;
using System.Threading.Tasks;

namespace Moedelo.Money.Kafka.PaymentOrders.Outgoing.PaymentToAccountablePerson
{
    [InjectAsSingleton(typeof(IPaymentToAccountablePersonEventReaderBuilder))]
    public class PaymentToAccountablePersonEventReaderBuilder : MoedeloEntityEventKafkaTopicReaderBuilder, IPaymentToAccountablePersonEventReaderBuilder
    {
        public PaymentToAccountablePersonEventReaderBuilder(
            IMoedeloEntityEventKafkaTopicReader reader,
            IExecutionInfoContextInitializer contextInitializer,
            IExecutionInfoContextAccessor contextAccessor,
            IAuditTracer auditTracer)
            : base(
                  MoneyTopics.PaymentOrders.PaymentToAccountablePerson.Event.Topic,
                  MoneyTopics.PaymentOrders.PaymentToAccountablePerson.EntityName,
                  reader,
                  contextInitializer,
                  contextAccessor,
                  auditTracer)
        {
        }

        public IPaymentToAccountablePersonEventReaderBuilder OnCreated(Func<PaymentToAccountablePersonCreated, Task> onEvent)
        {
            OnEvent(onEvent);
            return this;
        }

        public IPaymentToAccountablePersonEventReaderBuilder OnUpdated(Func<PaymentToAccountablePersonUpdated, Task> onEvent)
        {
            OnEvent(onEvent);
            return this;
        }

        public IPaymentToAccountablePersonEventReaderBuilder OnDeleted(Func<PaymentToAccountablePersonDeleted, Task> onEvent)
        {
            OnEvent(onEvent);
            return this;
        }

        public IPaymentToAccountablePersonEventReaderBuilder OnProvideRequired(Func<PaymentToAccountablePersonProvideRequired, Task> onEvent)
        {
            OnEvent(onEvent);
            return this;
        }
    }
}