using Moedelo.Accounts.Kafka.Abstractions.Events.EntityMapping;
using Moedelo.Common.Kafka.Abstractions.Entities.Events;

namespace Moedelo.Accounts.Kafka.NetCore.Abstractions.Events.EntityMapping
{
    public class EntityMappingsEvent : EntityMappingChanged, IEntityEventData
    {
    }
}