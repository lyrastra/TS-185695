using System;
using System.Threading.Tasks;
using Moedelo.Accounts.Kafka.Abstractions;
using Moedelo.Accounts.Kafka.NetCore.Abstractions.Events.EntityMapping;
using Moedelo.Accounts.Kafka.NetCore.Abstractions.Readers;
using Moedelo.Common.Audit.Abstractions.Interfaces;
using Moedelo.Common.ExecutionContext.Abstractions.Interfaces;
using Moedelo.Common.Kafka.Abstractions.Entities.Events;
using Moedelo.Common.Kafka.Abstractions.Entities.Events.Builders;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;

namespace Moedelo.Accounts.Kafka.NetCore.Readers
{
    [InjectAsSingleton(typeof(IEntityMappingEventReaderBuilder))]
    internal sealed class EntityMappingEventReaderBuilder : MoedeloEntityEventKafkaTopicReaderBuilder, IEntityMappingEventReaderBuilder
    {
        public EntityMappingEventReaderBuilder(
            IMoedeloEntityEventKafkaTopicReader reader,
            IExecutionInfoContextInitializer contextInitializer,
            IExecutionInfoContextAccessor contextAccessor,
            IAuditTracer auditTracer)
            : base(
                Topics.EntityMapping.Event.Topic,
                Topics.EntityMapping.EntityName,
                reader,
                contextInitializer,
                contextAccessor,
                auditTracer)
        {
        }

        public IEntityMappingEventReaderBuilder OnEntityMappingChanged(Func<EntityMappingsEvent, Task> onEvent)
        {
            OnEvent(onEvent);
            
            return this;
        }
        
        public IEntityMappingEventReaderBuilder OnRollbackEntityMapping(Func<RollbackEntityMappingsEvent, Task> onEvent)
        {
            OnEvent(onEvent, def => def.WithoutContext());
            
            return this;
        }
    }
}
