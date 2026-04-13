using System.Threading.Tasks;
using Moedelo.Common.Types;
using Moedelo.Money.Kafka.Abstractions.PaymentOrders.Outgoing.CurrencySale.Commands;

namespace Moedelo.Money.Kafka.Abstractions.PaymentOrders.Outgoing.CurrencySale
{
    public interface IOutgoingCurrencySaleCommandWriter
    {
        Task WriteImportAsync(
            ImportOutgoingCurrencySale commandData);

        Task WriteImportDuplicateAsync(
            ImportDuplicateOutgoingCurrencySale commandData);

        Task WriteImportWithMissingExchangeRateAsync(
            ImportWithMissingMissingExchangeRateOutgoingCurrencySale commandData);
    }
}