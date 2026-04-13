using System;
using System.Threading.Tasks;
using Moedelo.Common.Audit.Abstractions.Interfaces;
using Moedelo.Common.ExecutionContext.Abstractions.Interfaces;
using Moedelo.Common.Kafka.Abstractions.Entities.Events;
using Moedelo.Common.Kafka.Abstractions.Entities.Events.Builders;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Payroll.Kafka.Abstractions;
using Moedelo.Payroll.Kafka.Abstractions.Events;

namespace Moedelo.Payroll.Kafka.ChargeCudEvent
{
    [InjectAsSingleton]
    internal sealed class ChargeCudEventReaderBuilder : MoedeloEntityEventKafkaTopicReaderBuilder, IChargeCudEventReaderBuilder
    {
        private const string EntityType = "Charge";

        public ChargeCudEventReaderBuilder(
            IMoedeloEntityEventKafkaTopicReader reader,
            IExecutionInfoContextInitializer contextInitializer,
            IExecutionInfoContextAccessor contextAccessor,
            IAuditTracer auditTracer)
            : base(
                  PayrollTopics.Events.ChargeCudEvent,
                  EntityType,
                  reader,
                  contextInitializer,
                  contextAccessor,
                  auditTracer)
        {
        }

        public IChargeCudEventReaderBuilder OnChargeCudEvent(Func<ChargeCudEventMessage, Task> onEvent)
        {
           OnEvent(onEvent);

           return this;
        }
    }
}