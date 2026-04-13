using Moedelo.Common.Audit.Abstractions.Interfaces;
using Moedelo.Common.ExecutionContext.Abstractions.Interfaces;
using Moedelo.Common.Kafka.Abstractions.Entities.Events;
using Moedelo.Common.Kafka.Abstractions.Entities.Events.Builders;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Requisites.Kafka.Abstractions.Patent;
using Moedelo.Requisites.Kafka.Abstractions.Patent.Events;
using System;
using System.Threading.Tasks;

namespace Moedelo.Requisites.Kafka.Patent
{
    [InjectAsSingleton]
    internal sealed class PatentEventReaderBuilder : MoedeloEntityEventKafkaTopicReaderBuilder, IPatentEventReaderBuilder
    {
        public PatentEventReaderBuilder(
            IMoedeloEntityEventKafkaTopicReader reader,
            IExecutionInfoContextInitializer contextInitializer,
            IExecutionInfoContextAccessor contextAccessor,
            IAuditTracer auditTracer)
            : base(
                  Topics.PatentEntity.Event.Topic,
                  Topics.PatentEntity.EntityName,
                  reader,
                  contextInitializer,
                  contextAccessor,
                  auditTracer)
        {
        }

        public IPatentEventReaderBuilder OnChange(Func<PatentDataChanged, Task> onEvent)
        {
            OnEvent(onEvent);

            return this;
        }

        public IPatentEventReaderBuilder OnRemove(Func<PatentDataRemove, Task> onEvent)
        {
            OnEvent(onEvent);

            return this;
        }

        public IPatentEventReaderBuilder OnStop(Func<PatentDataStopped, Task> onEvent)
        {
            OnEvent(onEvent);

            return this;
        }
    }
}