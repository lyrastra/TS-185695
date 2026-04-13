using Moedelo.Common.Audit.Abstractions.Interfaces;
using Moedelo.Common.ExecutionContext.Abstractions.Interfaces;
using Moedelo.Common.Kafka.Abstractions.Entities.Events;
using Moedelo.Common.Kafka.Abstractions.Entities.Events.Builders;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Kafka.Abstractions.CashOrders.Outgoing.PaymentToAccountablePerson.Events;
using Moedelo.Money.Kafka.Abstractions.CashOrders.Outgoing.PaymentToAccountablePerson;
using Moedelo.Money.Kafka.Abstractions.Topics;
using System;
using System.Threading.Tasks;

namespace Moedelo.Money.Kafka.CashOrders.Outgoing.PaymentToAccountablePerson
{
    [InjectAsSingleton(typeof(IPaymentToAccountablePersonEventReaderBuilder))]
    class PaymentToAccountablePersonEventReaderBuilder : MoedeloEntityEventKafkaTopicReaderBuilder, IPaymentToAccountablePersonEventReaderBuilder
    {
        public PaymentToAccountablePersonEventReaderBuilder(
            IMoedeloEntityEventKafkaTopicReader reader,
            IExecutionInfoContextInitializer contextInitializer,
            IExecutionInfoContextAccessor contextAccessor,
            IAuditTracer auditTracer)
            : base(
                MoneyTopics.CashOrders.PaymentToAccountablePerson.Event.Topic,
                MoneyTopics.CashOrders.PaymentToAccountablePerson.EntityName,
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

        public IPaymentToAccountablePersonEventReaderBuilder OnProvided(Func<PaymentToAccountablePersonProvided, Task> onEvent)
        {
            OnEvent(onEvent);
            return this;
        }
    }
}
