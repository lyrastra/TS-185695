using Moedelo.Common.Audit.Abstractions.Interfaces;
using Moedelo.Common.ExecutionContext.Abstractions.Interfaces;
using Moedelo.Common.Kafka.Abstractions.Entities.Events;
using Moedelo.Common.Kafka.Abstractions.Entities.Events.Builders;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Kafka.Abstractions.PaymentOrders.Outgoing.PaymentToNaturalPersons;
using Moedelo.Money.Kafka.Abstractions.PaymentOrders.Outgoing.PaymentToNaturalPersons.Events;
using Moedelo.Money.Kafka.Abstractions.Topics;
using System;
using System.Threading.Tasks;

namespace Moedelo.Money.Kafka.PaymentOrders.Outgoing.PaymentToNaturalPersons
{
    [InjectAsSingleton(typeof(IPaymentToNaturalPersonsEventReaderBuilder))]
    public class PaymentToNaturalPersonsEventReaderBuilder : MoedeloEntityEventKafkaTopicReaderBuilder, IPaymentToNaturalPersonsEventReaderBuilder
    {
        public PaymentToNaturalPersonsEventReaderBuilder(
            IMoedeloEntityEventKafkaTopicReader reader,
            IExecutionInfoContextInitializer contextInitializer,
            IExecutionInfoContextAccessor contextAccessor,
            IAuditTracer auditTracer)
            : base(
                  MoneyTopics.PaymentOrders.PaymentToNaturalPersons.Event.Topic,
                  MoneyTopics.PaymentOrders.PaymentToNaturalPersons.EntityName,
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

        public IPaymentToNaturalPersonsEventReaderBuilder OnProvideRequired(Func<PaymentToNaturalPersonsProvideRequired, Task> onEvent)
        {
            OnEvent(onEvent);
            return this;
        }
    }
}