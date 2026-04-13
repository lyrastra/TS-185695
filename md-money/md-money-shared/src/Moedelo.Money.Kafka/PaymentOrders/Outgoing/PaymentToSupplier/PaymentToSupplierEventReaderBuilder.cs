using Moedelo.Common.Audit.Abstractions.Interfaces;
using Moedelo.Common.ExecutionContext.Abstractions.Interfaces;
using Moedelo.Common.Kafka.Abstractions.Entities.Events;
using Moedelo.Common.Kafka.Abstractions.Entities.Events.Builders;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Kafka.Abstractions.PaymentOrders.Outgoing.PaymentToSupplier;
using Moedelo.Money.Kafka.Abstractions.PaymentOrders.Outgoing.PaymentToSupplier.Events;
using Moedelo.Money.Kafka.Abstractions.Topics;
using System;
using System.Threading.Tasks;

namespace Moedelo.Money.Kafka.PaymentOrders.Outgoing.PaymentToSupplier
{
    [InjectAsSingleton(typeof(IPaymentToSupplierEventReaderBuilder))]
    public class PaymentToSupplierEventReaderBuilder : MoedeloEntityEventKafkaTopicReaderBuilder, IPaymentToSupplierEventReaderBuilder
    {
        public PaymentToSupplierEventReaderBuilder(
            IMoedeloEntityEventKafkaTopicReader reader,
            IExecutionInfoContextInitializer contextInitializer,
            IExecutionInfoContextAccessor contextAccessor,
            IAuditTracer auditTracer)
            : base(
                  MoneyTopics.PaymentOrders.PaymentToSupplier.Event.Topic,
                  MoneyTopics.PaymentOrders.PaymentToSupplier.EntityName,
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

        public IPaymentToSupplierEventReaderBuilder OnProvideRequired(Func<PaymentToSupplierProvideRequired, Task> onEvent)
        {
            OnEvent(onEvent);
            return this;
        }

        public IPaymentToSupplierEventReaderBuilder OnSetReserveAsync(Func<PaymentToSupplierSetReserve, Task> onEvent)
        {
            OnEvent(onEvent);
            return this;
        }
    }
}