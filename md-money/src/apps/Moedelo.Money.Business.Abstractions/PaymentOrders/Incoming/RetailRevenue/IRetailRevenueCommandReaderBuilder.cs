using Moedelo.Common.Kafka.Abstractions.Entities.Commands.Builders;
using Moedelo.Money.Kafka.Abstractions.PaymentOrders.Incoming.RetailRevenue.Commands;
using System;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.Abstractions.PaymentOrders.Incoming.RetailRevenue
{
    public interface IRetailRevenueCommandReaderBuilder : IMoedeloEntityCommandKafkaTopicReaderBuilder
    {
        IRetailRevenueCommandReaderBuilder OnImport(Func<ImportRetailRevenue, Task> onCommand);
        IRetailRevenueCommandReaderBuilder OnImportDuplicate(Func<ImportDuplicateRetailRevenue, Task> onCommand);
    }
}
