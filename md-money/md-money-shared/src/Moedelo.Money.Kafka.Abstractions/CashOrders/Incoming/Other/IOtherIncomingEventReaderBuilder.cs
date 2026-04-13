using Moedelo.Common.Kafka.Abstractions.Entities.Events.Builders;
using Moedelo.Money.Kafka.Abstractions.CashOrders.Incoming.Other.Events;
using Moedelo.Money.Kafka.Abstractions.CashOrders.Outgoing.Other.Events;
using System;
using System.Threading.Tasks;

namespace Moedelo.Money.Kafka.Abstractions.CashOrders.Incoming.Other
{
    public interface IOtherIncomingEventReaderBuilder : IMoedeloEntityEventKafkaTopicReaderBuilder
    {
        IOtherIncomingEventReaderBuilder OnCreated(Func<OtherIncomingCreated, Task> onEvent);

        IOtherIncomingEventReaderBuilder OnUpdated(Func<OtherIncomingUpdated, Task> onEvent);

        IOtherIncomingEventReaderBuilder OnDeleted(Func<OtherIncomingDeleted, Task> onEvent);
    }
}
