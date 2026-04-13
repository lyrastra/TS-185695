using System.Threading.Tasks;
using Moedelo.Common.Types;
using Moedelo.Money.Kafka.Abstractions.PaymentOrders.Incoming.CurrencyTransferFromAccount.Commands;

namespace Moedelo.Money.Kafka.Abstractions.PaymentOrders.Incoming.CurrencyTransferFromAccount
{
    public interface ICurrencyTransferFromAccountCommandWriter
    {
        Task WriteImportAsync(
            ImportCurrencyTransferFromAccount commandData);

        Task WriteImportDuplicateAsync(
            ImportDuplicateCurrencyTransferFromAccount commandData);

        Task WriteImportWithMissingCurrencySettlementAccountAsync(
            ImportWithMissingCurrencySettlementAccountCurrencyTransferFromAccount commandData);
    }
}