using Moedelo.Common.Audit.Abstractions.Interfaces;
using Moedelo.Common.ExecutionContext.Abstractions.Interfaces;
using Moedelo.Common.Kafka.Abstractions.Entities.Events;
using Moedelo.Common.Kafka.Abstractions.Entities.Events.Builders;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Kafka.Abstractions.CashOrders.Incoming.MediationFee;
using Moedelo.Money.Kafka.Abstractions.CashOrders.Incoming.MediationFee.Events;
using Moedelo.Money.Kafka.Abstractions.Topics;
using System;
using System.Threading.Tasks;

namespace Moedelo.Money.Kafka.CashOrders.Incoming.MediationFee
{
    [InjectAsSingleton(typeof(IMediationFeeEventReaderBuilder))]
    class MediationFeeEventReaderBuilder : MoedeloEntityEventKafkaTopicReaderBuilder, IMediationFeeEventReaderBuilder
    {
        public MediationFeeEventReaderBuilder(
            IMoedeloEntityEventKafkaTopicReader reader,
            IExecutionInfoContextInitializer contextInitializer,
            IExecutionInfoContextAccessor contextAccessor,
            IAuditTracer auditTracer)
            : base(
                MoneyTopics.CashOrders.MediationFee.Event.Topic,
                MoneyTopics.CashOrders.MediationFee.EntityName,
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
    }
}
