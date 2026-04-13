using System.Collections.Generic;

namespace Moedelo.Accounts.Kafka.Abstractions.Events.EntityMapping
{
    public class EntityMappingChanged
    {
        public List<EntityMapping> List { get; set; } = new List<EntityMapping>();
    }
}