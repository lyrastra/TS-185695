using Moedelo.Common.Audit.Abstractions.Interfaces;
using Moedelo.Common.ExecutionContext.Abstractions.Interfaces;
using Moedelo.Common.Kafka.Abstractions.Entities.Events;
using Moedelo.Common.Kafka.Abstractions.Entities.Events.Builders;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Kafka.Abstractions.PaymentOrders.Incoming.MediationFee;
using Moedelo.Money.Kafka.Abstractions.PaymentOrders.Incoming.MediationFee.Events;
using Moedelo.Money.Kafka.Abstractions.Topics;
using System;
using System.Threading.Tasks;

namespace Moedelo.Money.Kafka.PaymentOrders.Incoming.MediationFee
{
    [InjectAsSingleton(typeof(IMediationFeeEventReaderBuilder))]
    public class MediationFeeEventReaderBuilder : MoedeloEntityEventKafkaTopicReaderBuilder, IMediationFeeEventReaderBuilder
    {
        public MediationFeeEventReaderBuilder(
            IMoedeloEntityEventKafkaTopicReader reader,
            IExecutionInfoContextInitializer contextInitializer,
            IExecutionInfoContextAccessor contextAccessor,
            IAuditTracer auditTracer)
            : base(
                  MoneyTopics.PaymentOrders.MediationFee.Event.Topic,
                  MoneyTopics.PaymentOrders.MediationFee.EntityName,
                  reader,
                  contextInitializer,
                  contextAccessor,
                  auditTracer)
        {
        }

        public IMediationFeeEventReaderBuilder OnCreated(Func<MediationFeeCreated, Task> onEvent)
        {
            OnEvent(onEvent);
            return this;
        }

        public IMediationFeeEventReaderBuilder OnUpdated(Func<MediationFeeUpdated, Task> onEvent)
        {
            OnEvent(onEvent);
            return this;
        }

        public IMediationFeeEventReaderBuilder OnDeleted(Func<MediationFeeDeleted, Task> onEvent)
        {
            OnEvent(onEvent);
            return this;
        }

        public IMediationFeeEventReaderBuilder OnProvideRequired(Func<MediationFeeProvideRequired, Task> onEvent)
        {
            OnEvent(onEvent);
            return this;
        }
    }
}