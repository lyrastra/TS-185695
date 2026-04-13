using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;
using Moedelo.Money.Kafka.PaymentOrders.Incoming.CurrencyPurchase.Commands;
using System.Threading.Tasks;

namespace Moedelo.Money.Kafka.PaymentOrders.Incoming.CurrencyPurchase
{
    public interface IIncomingCurrencyPurchaseCommandWriter : IDI
    {
        Task WriteImportAsync(string key, string token, ImportIncomingCurrencyPurchase commandData);
        Task WriteImportDuplicateAsync(string key, string token, ImportDuplicateIncomingCurrencyPurchase commandData);
    }
}
