using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;
using Moedelo.Money.Kafka.PaymentOrders.Outgoing.CurrencyPurchase.Commands;
using System.Threading.Tasks;

namespace Moedelo.Money.Kafka.PaymentOrders.Outgoing.CurrencyPurchase
{
    public interface IOutgoingCurrencyPurchaseCommandWriter : IDI
    {
        Task WriteImportAsync(string key, string token, ImportOutgoingCurrencyPurchase commandData);
        Task WriteImportDuplicateAsync(string key, string token, ImportDuplicateOutgoingCurrencyPurchase commandData);
        Task WriteImportWithMissingExchangeRateAsync(string key, string token, ImportWithMissingMissingExchangeRateOutgoingCurrencyPurchase commandData);
        Task WriteImportWithMissingCurrencySettlementAccountAsync(string key, string token, ImportWithMissingCurrencySettlementAccountOutgoingCurrencyPurchase commandData);
    }
}
