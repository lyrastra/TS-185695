using System.Threading.Tasks;
using Moedelo.Common.Types;
using Moedelo.Money.Kafka.Abstractions.PaymentOrders.Incoming.CurrencySale.Commands;

namespace Moedelo.Money.Kafka.Abstractions.PaymentOrders.Incoming.CurrencySale
{
    public interface IIncomingCurrencySaleCommandWriter
    {
        Task WriteImportAsync(
            ImportIncomingCurrencySale commandData);

        Task WriteImportDuplicateAsync(
            ImportDuplicateIncomingCurrencySale commandData);

        Task WriteImportWithMissingCurrencySettlementAccountAsync(
            ImportWithMissingCurrencySettlementAccountIncomingCurrencySale commandData);
    }
}