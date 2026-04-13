using System;
using System.Threading.Tasks;
using Moedelo.Common.Audit.Abstractions.Interfaces;
using Moedelo.Common.ExecutionContext.Abstractions.Interfaces;
using Moedelo.Common.Kafka.Abstractions.Entities.Events;
using Moedelo.Common.Kafka.Abstractions.Entities.Events.Builders;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Kafka.Abstractions.PaymentOrders.Outgoing.Deduction;
using Moedelo.Money.Kafka.Abstractions.PaymentOrders.Outgoing.Deduction.Events;
using Moedelo.Money.Kafka.Abstractions.Topics;

namespace Moedelo.Money.Kafka.PaymentOrders.Outgoing.Deduction
{
    [InjectAsSingleton(typeof(IDeductionEventReaderBuilder))]
    public class DeductionEventReaderBuilder : MoedeloEntityEventKafkaTopicReaderBuilder, IDeductionEventReaderBuilder
    {
        public DeductionEventReaderBuilder(
            IMoedeloEntityEventKafkaTopicReader reader,
            IExecutionInfoContextInitializer contextInitializer,
            IExecutionInfoContextAccessor contextAccessor,
            IAuditTracer auditTracer)
            : base(
                  MoneyTopics.PaymentOrders.Deduction.Event.Topic,
                  MoneyTopics.PaymentOrders.Deduction.EntityName,
                  reader,
                  contextInitializer,
                  contextAccessor,
                  auditTracer)
        {
        }

        public IDeductionEventReaderBuilder OnCreated(Func<DeductionCreated, Task> onEvent)
        {
            OnEvent(onEvent);
            return this;
        }

        public IDeductionEventReaderBuilder OnUpdated(Func<DeductionUpdated, Task> onEvent)
        {
            OnEvent(onEvent);
            return this;
        }

        public IDeductionEventReaderBuilder OnDeleted(Func<DeductionDeleted, Task> onEvent)
        {
            OnEvent(onEvent);
            return this;
        }

        public IDeductionEventReaderBuilder OnProvideRequired(Func<DeductionProvideRequired, Task> onEvent)
        {
            OnEvent(onEvent);
            return this;
        }
    }
}