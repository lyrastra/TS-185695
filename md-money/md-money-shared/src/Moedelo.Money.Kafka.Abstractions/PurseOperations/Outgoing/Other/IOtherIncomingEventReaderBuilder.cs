using Moedelo.Common.Kafka.Abstractions.Entities.Events.Builders;
using Moedelo.Money.Kafka.Abstractions.PurseOperations.Outgoing.Other.Events;
using System;
using System.Threading.Tasks;

namespace Moedelo.Money.Kafka.Abstractions.PurseOperations.Outgoing.Other
{
    public interface IOtherOutgoingEventReaderBuilder : IMoedeloEntityEventKafkaTopicReaderBuilder
    {
        IOtherOutgoingEventReaderBuilder OnCreated(Func<OtherOutgoingCreated, Task> onEvent);

        IOtherOutgoingEventReaderBuilder OnUpdated(Func<OtherOutgoingUpdated, Task> onEvent);

        IOtherOutgoingEventReaderBuilder OnDeleted(Func<OtherOutgoingDeleted, Task> onEvent);
    }
}
