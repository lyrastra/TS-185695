using System;
using System.Threading.Tasks;
using Moedelo.Common.Audit.Abstractions.Interfaces;
using Moedelo.Common.ExecutionContext.Abstractions.Interfaces;
using Moedelo.Common.Kafka.Abstractions.Entities.Events;
using Moedelo.Common.Kafka.Abstractions.Entities.Events.Builders;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Requisites.Kafka.Abstractions.TaxationSystem;
using Moedelo.Requisites.Kafka.Abstractions.TaxationSystem.Events;

namespace Moedelo.Requisites.Kafka.TaxationSystem
{
    [InjectAsSingleton]
    internal sealed class TaxationSystemEventReaderBuilder : MoedeloEntityEventKafkaTopicReaderBuilder, ITaxationSystemEventReaderBuilder
    {
        public TaxationSystemEventReaderBuilder(
            IMoedeloEntityEventKafkaTopicReader reader,
            IExecutionInfoContextInitializer contextInitializer,
            IExecutionInfoContextAccessor contextAccessor,
            IAuditTracer auditTracer)
            : base(
                  Topics.TaxationSystemEntity.Event.Topic, 
                  Topics.TaxationSystemEntity.EntityName,
                  reader,
                  contextInitializer,
                  contextAccessor,
                  auditTracer)
        {
        }

        public ITaxationSystemEventReaderBuilder OnTaxationSystemChanged(Func<TaxationSystemChanged, Task> onEvent)
        {
            OnEvent(onEvent);

            return this;
        }
    }
}