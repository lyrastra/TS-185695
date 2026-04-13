using Moedelo.Common.Kafka.Abstractions.Entities.Events.Builders;
using Moedelo.Money.Kafka.Abstractions.CashOrders.Incoming.MiddlemanRetailRevenue.Events;
using System;
using System.Threading.Tasks;

namespace Moedelo.Money.Kafka.Abstractions.CashOrders.Incoming.MiddlemanRetailRevenue
{
    public interface IMiddlemanRetailRevenueEventReaderBuilder : IMoedeloEntityEventKafkaTopicReaderBuilder
    {
        IMiddlemanRetailRevenueEventReaderBuilder OnCreated(Func<MiddlemanRetailRevenueCreated, Task> onEvent);

        IMiddlemanRetailRevenueEventReaderBuilder OnUpdated(Func<MiddlemanRetailRevenueUpdated, Task> onEvent);

        IMiddlemanRetailRevenueEventReaderBuilder OnDeleted(Func<MiddlemanRetailRevenueDeleted, Task> onEvent);
    }
}
