using Moedelo.Common.Audit.Abstractions.Interfaces;
using Moedelo.Common.ExecutionContext.Abstractions.Interfaces;
using Moedelo.Common.Kafka.Abstractions.Entities.Events;
using Moedelo.Common.Kafka.Abstractions.Entities.Events.Builders;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Kafka.Abstractions.PurseOperations.Outgoing.WithholdingOfFee;
using Moedelo.Money.Kafka.Abstractions.PurseOperations.Outgoing.WithholdingOfFee.Events;
using Moedelo.Money.Kafka.Abstractions.Topics;
using System;
using System.Threading.Tasks;

namespace Moedelo.Money.Kafka.PurseOperations.Outgoing.WithholdingOfFee
{
    [InjectAsSingleton(typeof(IWithholdingOfFeeEventReaderBuilder))]
    class WithholdingOfFeeEventReaderBuilder : MoedeloEntityEventKafkaTopicReaderBuilder, IWithholdingOfFeeEventReaderBuilder
    {
        public WithholdingOfFeeEventReaderBuilder(
            IMoedeloEntityEventKafkaTopicReader reader,
            IExecutionInfoContextInitializer contextInitializer,
            IExecutionInfoContextAccessor contextAccessor,
            IAuditTracer auditTracer)
            : base(
                MoneyTopics.PurseOperations.WithholdingOfFee.Event.Topic,
                MoneyTopics.PurseOperations.WithholdingOfFee.EntityName,
                reader,
                contextInitializer,
                contextAccessor,
                auditTracer)
        {
        }

        public IWithholdingOfFeeEventReaderBuilder OnCreated(Func<WithholdingOfFeeCreated, Task> onEvent)
        {
            OnEvent(onEvent);
            return this;
        }

        public IWithholdingOfFeeEventReaderBuilder OnUpdated(Func<WithholdingOfFeeUpdated, Task> onEvent)
        {
            OnEvent(onEvent);
            return this;
        }

        public IWithholdingOfFeeEventReaderBuilder OnDeleted(Func<WithholdingOfFeeDeleted, Task> onEvent)
        {
            OnEvent(onEvent);
            return this;
        }
    }
}
