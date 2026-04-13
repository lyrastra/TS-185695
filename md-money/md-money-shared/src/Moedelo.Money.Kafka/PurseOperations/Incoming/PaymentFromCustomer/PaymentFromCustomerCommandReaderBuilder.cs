using Moedelo.Common.Kafka.Abstractions.Entities.Events;
using Moedelo.Common.Kafka.Abstractions.Entities.Events.Builders;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Kafka.Abstractions.PurseOperations.Incoming.PaymentFromCustomer;
using Moedelo.Money.Kafka.Abstractions.PurseOperations.Incoming.PaymentFromCustomer.Events;
using Moedelo.Money.Kafka.Abstractions.Topics;
using System;
using System.Threading.Tasks;
using Moedelo.Common.Audit.Abstractions.Interfaces;
using Moedelo.Common.ExecutionContext.Abstractions.Interfaces;

namespace Moedelo.Money.Kafka.PurseOperations.Incoming.PaymentFromCustomer
{
    [InjectAsSingleton(typeof(IPaymentFromCustomerEventReaderBuilder))]
    public class PaymentFromCustomerEventReaderBuilder : MoedeloEntityEventKafkaTopicReaderBuilder, IPaymentFromCustomerEventReaderBuilder
    {
        public PaymentFromCustomerEventReaderBuilder(
            IMoedeloEntityEventKafkaTopicReader reader,
            IExecutionInfoContextInitializer contextInitializer,
            IExecutionInfoContextAccessor contextAccessor,
            IAuditTracer auditTracer)
            : base(
                  MoneyTopics.PurseOperations.PaymentFromCustomer.Event.Topic,
                  MoneyTopics.PurseOperations.PaymentFromCustomer.EntityName,
                  reader,
                  contextInitializer,
                  contextAccessor,
                  auditTracer)
        {
        }

        public IPaymentFromCustomerEventReaderBuilder OnCreated(Func<PaymentFromCustomerCreated, Task> onEvent)
        {
            OnEvent(onEvent);
            return this;
        }

        public IPaymentFromCustomerEventReaderBuilder OnUpdated(Func<PaymentFromCustomerUpdated, Task> onEvent)
        {
            OnEvent(onEvent);
            return this;
        }

        public IPaymentFromCustomerEventReaderBuilder OnDeleted(Func<PaymentFromCustomerDeleted, Task> onEvent)
        {
            OnEvent(onEvent);
            return this;
        }

        public IPaymentFromCustomerEventReaderBuilder OnProvided(Func<PaymentFromCustomerProvided, Task> onEvent)
        {
            OnEvent(onEvent);
            return this;
        }
    }
}