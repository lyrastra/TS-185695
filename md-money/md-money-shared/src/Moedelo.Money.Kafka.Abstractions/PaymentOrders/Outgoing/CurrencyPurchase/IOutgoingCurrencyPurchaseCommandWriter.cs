using System.Threading.Tasks;
using Moedelo.Common.Types;
using Moedelo.Money.Kafka.Abstractions.PaymentOrders.Outgoing.CurrencyPurchase.Commands;

namespace Moedelo.Money.Kafka.Abstractions.PaymentOrders.Outgoing.CurrencyPurchase
{
    public interface IOutgoingCurrencyPurchaseCommandWriter
    {
        Task WriteImportAsync(
            ImportOutgoingCurrencyPurchase commandData);

        Task WriteImportDuplicateAsync(
            ImportDuplicateOutgoingCurrencyPurchase commandData);

        Task WriteImportWithMissingExchangeRateAsync(
            ImportWithMissingMissingExchangeRateOutgoingCurrencyPurchase commandData);

        Task WriteImportWithMissingCurrencySettlementAccountAsync(
            ImportWithMissingCurrencySettlementAccountOutgoingCurrencyPurchase commandData);
    }
}