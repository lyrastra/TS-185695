using System;
using System.Threading.Tasks;
using Moedelo.Accounts.Kafka.NetCore.Abstractions.Events.EntityMapping;
using Moedelo.Common.Kafka.Abstractions.Entities.Events.Builders;

namespace Moedelo.Accounts.Kafka.NetCore.Abstractions.Readers
{
    public interface IEntityMappingEventReaderBuilder : IMoedeloEntityEventKafkaTopicReaderBuilder
    {
        IEntityMappingEventReaderBuilder OnEntityMappingChanged(Func<EntityMappingsEvent, Task> onEvent);
        IEntityMappingEventReaderBuilder OnRollbackEntityMapping(Func<RollbackEntityMappingsEvent, Task> onEvent);
    }
}
