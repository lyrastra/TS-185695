using System;
using System.Threading.Tasks;
using Moedelo.Accounting.Kafka.Abstractions.EventReaders;
using Moedelo.Accounting.Kafka.Abstractions.Events.EventData.ClosedPeriods;
using Moedelo.Accounting.Kafka.Abstractions.Events.Topics.ClosedPeriods;
using Moedelo.Common.Audit.Abstractions.Interfaces;
using Moedelo.Common.ExecutionContext.Abstractions.Interfaces;
using Moedelo.Common.Kafka.Abstractions.Entities.Events;
using Moedelo.Common.Kafka.Abstractions.Entities.Events.Builders;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;

namespace Moedelo.Accounting.Kafka.EventReaders.ClosedPeriods
{
    [InjectAsSingleton(typeof(IClosedPeriodEventReaderBuilder))]
    internal sealed class ClosedPeriodEventReaderBuilder : MoedeloEntityEventKafkaTopicReaderBuilder, IClosedPeriodEventReaderBuilder
    {
        public ClosedPeriodEventReaderBuilder(
            IMoedeloEntityEventKafkaTopicReader reader,
            IExecutionInfoContextInitializer contextInitializer,
            IExecutionInfoContextAccessor contextAccessor,
            IAuditTracer auditTracer)
            : base(
                ClosedPeriodTopics.Event.Topic,
                ClosedPeriodTopics.Event.EntityName,
                reader,
                contextInitializer,
                contextAccessor,
                auditTracer)
        {
        }

        public IClosedPeriodEventReaderBuilder OnCreated(Func<ClosedPeriodCreated, Task> onEvent)
        {
            OnEvent(onEvent);

            return this;
        }

        public IClosedPeriodEventReaderBuilder OnDeleted(Func<ClosedPeriodDeleted, Task> onEvent)
        {
            OnEvent(onEvent);

            return this;
        }

        public IClosedPeriodEventReaderBuilder OnCheckPeriodCompleted(Func<CheckPeriodCompleted, Task> onEvent)
        {
            OnEvent(onEvent);

            return this;
        }
    }
}