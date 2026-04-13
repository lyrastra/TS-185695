using System;
using System.Collections.Generic;

namespace Moedelo.Accounts.Kafka.NetCore.Abstractions.Events.EntityMapping
{
    public class RollbackEntityMappingsEvent : EntityMappingsEvent
    {
        public enum Stage
        {
            NeverLoggedAfterTransfer = 1
        }

        public Stage RollbackStage { get; }
        public DateTime Date { get; }

        public RollbackEntityMappingsEvent(List<Kafka.Abstractions.Events.EntityMapping.EntityMapping> list,
                                           Stage stage)
        {
            List = list;
            RollbackStage = stage;
            Date = DateTime.Now;
        }
    }
}
