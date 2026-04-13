using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;
using Moedelo.Money.Kafka.PaymentOrders.Incoming.RetailRevenue.Commands;
using System.Threading.Tasks;

namespace Moedelo.Money.Kafka.PaymentOrders.Incoming.RetailRevenue
{
    public interface IRetailRevenueCommandWriter : IDI
    {
        Task WriteImportAsync(string key, string token, ImportRetailRevenue commandData);
        Task WriteImportDuplicateAsync(string key, string token, ImportDuplicateRetailRevenue commandData);
    }
}