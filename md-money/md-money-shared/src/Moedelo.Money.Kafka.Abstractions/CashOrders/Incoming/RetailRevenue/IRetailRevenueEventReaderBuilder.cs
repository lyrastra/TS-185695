using Moedelo.Common.Kafka.Abstractions.Entities.Events.Builders;
using Moedelo.Money.Kafka.Abstractions.CashOrders.Incoming.RetailRevenue.Events;
using System;
using System.Threading.Tasks;

namespace Moedelo.Money.Kafka.Abstractions.CashOrders.Incoming.RetailRevenue
{
    /// <summary>
    /// ПКО - "Розничная выручка". Чтение событий
    /// </summary>
    public interface IRetailRevenueEventReaderBuilder : IMoedeloEntityEventKafkaTopicReaderBuilder
    {
        IRetailRevenueEventReaderBuilder OnCreated(Func<RetailRevenueCreated, Task> onEvent);

        IRetailRevenueEventReaderBuilder OnUpdated(Func<RetailRevenueUpdated, Task> onEvent);

        IRetailRevenueEventReaderBuilder OnDeleted(Func<RetailRevenueDeleted, Task> onEvent);
    }
}
