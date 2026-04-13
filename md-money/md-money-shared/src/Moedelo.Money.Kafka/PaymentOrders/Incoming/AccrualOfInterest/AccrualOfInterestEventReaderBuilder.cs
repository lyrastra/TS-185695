using Moedelo.Common.Audit.Abstractions.Interfaces;
using Moedelo.Common.ExecutionContext.Abstractions.Interfaces;
using Moedelo.Common.Kafka.Abstractions.Entities.Events;
using Moedelo.Common.Kafka.Abstractions.Entities.Events.Builders;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Kafka.Abstractions.PaymentOrders.Incoming.AccrualOfInterest;
using Moedelo.Money.Kafka.Abstractions.PaymentOrders.Incoming.AccrualOfInterest.Events;
using Moedelo.Money.Kafka.Abstractions.Topics;
using System;
using System.Threading.Tasks;

namespace Moedelo.Money.Kafka.PaymentOrders.Incoming.AccrualOfInterest
{
    [InjectAsSingleton(typeof(IAccrualOfInterestEventReaderBuilder))]
    public class AccrualOfInterestEventReaderBuilder : MoedeloEntityEventKafkaTopicReaderBuilder, IAccrualOfInterestEventReaderBuilder
    {
        public AccrualOfInterestEventReaderBuilder(
            IMoedeloEntityEventKafkaTopicReader reader,
            IExecutionInfoContextInitializer contextInitializer,
            IExecutionInfoContextAccessor contextAccessor,
            IAuditTracer auditTracer)
            : base(
                  MoneyTopics.PaymentOrders.AccrualOfInterest.Event.Topic,
                  MoneyTopics.PaymentOrders.AccrualOfInterest.EntityName,
                  reader,
                  contextInitializer,
                  contextAccessor,
                  auditTracer)
        {
        }

        public IAccrualOfInterestEventReaderBuilder OnCreated(Func<AccrualOfInterestCreated, Task> onEvent)
        {
            OnEvent(onEvent);
            return this;
        }

        public IAccrualOfInterestEventReaderBuilder OnUpdated(Func<AccrualOfInterestUpdated, Task> onEvent)
        {
            OnEvent(onEvent);
            return this;
        }

        public IAccrualOfInterestEventReaderBuilder OnDeleted(Func<AccrualOfInterestDeleted, Task> onEvent)
        {
            OnEvent(onEvent);
            return this;
        }

        public IAccrualOfInterestEventReaderBuilder OnProvideRequired(Func<AccrualOfInterestProvideRequired, Task> onEvent)
        {
            OnEvent(onEvent);
            return this;
        }
    }
}