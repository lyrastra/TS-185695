using System;
using System.Threading.Tasks;
using Moedelo.Common.Audit.Abstractions.Interfaces;
using Moedelo.Common.ExecutionContext.Abstractions.Interfaces;
using Moedelo.Common.Kafka.Abstractions.Entities.Events;
using Moedelo.Common.Kafka.Abstractions.Entities.Events.Builders;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Requisites.Kafka.Abstractions.NdsRatePeriods;
using Moedelo.Requisites.Kafka.Abstractions.NdsRatePeriods.Events;

namespace Moedelo.Requisites.Kafka.NdsRatePeriods
{
    [InjectAsSingleton]
    internal sealed class NdsRatePeriodsEventReaderBuilder : MoedeloEntityEventKafkaTopicReaderBuilder, INdsRatePeriodsEventReaderBuilder
    {
        public NdsRatePeriodsEventReaderBuilder(
            IMoedeloEntityEventKafkaTopicReader reader,
            IExecutionInfoContextInitializer contextInitializer,
            IExecutionInfoContextAccessor contextAccessor,
            IAuditTracer auditTracer)
            : base(
                  Topics.NdsRatePeriodsEntity.Event.Topic, 
                  Topics.NdsRatePeriodsEntity.EntityName,
                  reader,
                  contextInitializer,
                  contextAccessor,
                  auditTracer)
        {
        }

        public INdsRatePeriodsEventReaderBuilder OnNdsRatePeriodsChanged(Func<NdsRatePeriodsChanged, Task> onEvent)
        {
            OnEvent(onEvent);

            return this;
        }
    }
}