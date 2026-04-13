using Moedelo.Common.Audit.Abstractions.Interfaces;
using Moedelo.Common.ExecutionContext.Abstractions.Interfaces;
using Moedelo.Common.Kafka.Abstractions.Entities.Events;
using Moedelo.Common.Kafka.Abstractions.Entities.Events.Builders;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Kafka.Abstractions.CashOrders.Outgoing.PaymentToNaturalPersons.Events;
using Moedelo.Money.Kafka.Abstractions.CashOrders.Outgoing.PaymentToNaturalPersons;
using Moedelo.Money.Kafka.Abstractions.Topics;
using System;
using System.Threading.Tasks;

namespace Moedelo.Money.Kafka.CashOrders.Outgoing.PaymentToNaturalPersons
{
    [InjectAsSingleton(typeof(IPaymentToNaturalPersonsEventReaderBuilder))]
    class PaymentToNaturalPersonsEventReaderBuilder : MoedeloEntityEventKafkaTopicReaderBuilder, IPaymentToNaturalPersonsEventReaderBuilder
    {
        public PaymentToNaturalPersonsEventReaderBuilder(
            IMoedeloEntityEventKafkaTopicReader reader,
            IExecutionInfoContextInitializer contextInitializer,
            IExecutionInfoContextAccessor contextAccessor,
            IAuditTracer auditTracer)
            : base(
                MoneyTopics.CashOrders.PaymentToNaturalPersons.Event.Topic,
                MoneyTopics.CashOrders.PaymentToNaturalPersons.EntityName,
                reader,
                contextInitializer,
                contextAccessor,
                auditTracer)
        {
        }

        public IPaymentToNaturalPersonsEventReaderBuilder OnCreated(Func<PaymentToNaturalPersonsCreated, Task> onEvent)
        {
            OnEvent(onEvent);
            return this;
        }

        public IPaymentToNaturalPersonsEventReaderBuilder OnUpdated(Func<PaymentToNaturalPersonsUpdated, Task> onEvent)
        {
            OnEvent(onEvent);
            return this;
        }

        public IPaymentToNaturalPersonsEventReaderBuilder OnDeleted(Func<PaymentToNaturalPersonsDeleted, Task> onEvent)
        {
            OnEvent(onEvent);
            return this;
        }
    }
}
