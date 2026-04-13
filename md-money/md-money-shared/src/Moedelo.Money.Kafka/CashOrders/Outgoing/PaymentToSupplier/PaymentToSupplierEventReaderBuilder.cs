using Moedelo.Common.Kafka.Abstractions.Entities.Events;
using Moedelo.Common.Kafka.Abstractions.Entities.Events.Builders;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Kafka.Abstractions.CashOrders.Outgoing.PaymentToSupplier;
using Moedelo.Money.Kafka.Abstractions.CashOrders.Outgoing.PaymentToSupplier.Events;
using Moedelo.Money.Kafka.Abstractions.Topics;
using System;
using System.Threading.Tasks;
using Moedelo.Common.Audit.Abstractions.Interfaces;
using Moedelo.Common.ExecutionContext.Abstractions.Interfaces;

namespace Moedelo.Money.Kafka.CashOrders.Outgoing.PaymentToSupplier
{
    [InjectAsSingleton(typeof(IPaymentToSupplierEventReaderBuilder))]
    class PaymentToSupplierEventReaderBuilder : MoedeloEntityEventKafkaTopicReaderBuilder, IPaymentToSupplierEventReaderBuilder
    {
        public PaymentToSupplierEventReaderBuilder(
            IMoedeloEntityEventKafkaTopicReader reader,
            IExecutionInfoContextInitializer contextInitializer,
            IExecutionInfoContextAccessor contextAccessor,
            IAuditTracer auditTracer)
            : base(
                MoneyTopics.CashOrders.PaymentToSupplier.Event.Topic,
                MoneyTopics.CashOrders.PaymentToSupplier.EntityName,
                reader,
                contextInitializer,
                contextAccessor,
                auditTracer)
        {
        }

        public IPaymentToSupplierEventReaderBuilder OnCreated(Func<PaymentToSupplierCreated, Task> onEvent)
        {
            OnEvent(onEvent);
            return this;
        }

        public IPaymentToSupplierEventReaderBuilder OnUpdated(Func<PaymentToSupplierUpdated, Task> onEvent)
        {
            OnEvent(onEvent);
            return this;
        }

        public IPaymentToSupplierEventReaderBuilder OnDeleted(Func<PaymentToSupplierDeleted, Task> onEvent)
        {
            OnEvent(onEvent);
            return this;
        }

        public IPaymentToSupplierEventReaderBuilder OnProvided(Func<PaymentToSupplierProvided, Task> onEvent)
        {
            OnEvent(onEvent);
            return this;
        }
    }
}
