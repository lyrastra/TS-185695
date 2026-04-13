using Moedelo.Common.Kafka.Abstractions.Entities.Events.Builders;
using Moedelo.Money.Kafka.Abstractions.PaymentOrders.Incoming.RetailRevenue.Events;
using System;
using System.Threading.Tasks;

namespace Moedelo.Money.Kafka.Abstractions.PaymentOrders.Incoming.RetailRevenue
{
    public interface IRetailRevenueEventReaderBuilder : IMoedeloEntityEventKafkaTopicReaderBuilder
    {
        IRetailRevenueEventReaderBuilder OnCreated(Func<RetailRevenueCreated, Task> onEvent);
        IRetailRevenueEventReaderBuilder OnUpdated(Func<RetailRevenueUpdated, Task> onEvent);
        IRetailRevenueEventReaderBuilder OnDeleted(Func<RetailRevenueDeleted, Task> onEvent);

        IRetailRevenueEventReaderBuilder OnProvideRequired(Func<RetailRevenueProvideRequired, Task> onEvent);
        IRetailRevenueEventReaderBuilder OnUpdateAfterAccountingStatementCreated(Func<RetailRevenueUpdateAfterAccountingStatementCreated, Task> onEvent);
    }
}