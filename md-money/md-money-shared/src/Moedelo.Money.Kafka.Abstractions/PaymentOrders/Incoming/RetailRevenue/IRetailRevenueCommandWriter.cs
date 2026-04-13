using System.Threading.Tasks;
using Moedelo.Money.Kafka.Abstractions.PaymentOrders.Incoming.RetailRevenue.Commands;

namespace Moedelo.Money.Kafka.Abstractions.PaymentOrders.Incoming.RetailRevenue
{
    public interface IRetailRevenueCommandWriter
    {
        Task WriteImportAsync(ImportRetailRevenue commandData);

        Task WriteImportDuplicateAsync(ImportDuplicateRetailRevenue commandData);
    }
}