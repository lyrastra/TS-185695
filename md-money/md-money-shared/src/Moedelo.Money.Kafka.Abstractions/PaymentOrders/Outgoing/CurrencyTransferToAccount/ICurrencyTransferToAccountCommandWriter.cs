using System.Threading.Tasks;
using Moedelo.Common.Types;
using Moedelo.Money.Kafka.Abstractions.PaymentOrders.Outgoing.CurrencyTransferToAccount.Commands;

namespace Moedelo.Money.Kafka.Abstractions.PaymentOrders.Outgoing.CurrencyTransferToAccount
{
    public interface ICurrencyTransferToAccountCommandWriter
    {
        Task WriteImportAsync(
            ImportCurrencyTransferToAccount commandData);

        Task WriteImportDuplicateAsync(
            ImportDuplicateCurrencyTransferToAccount commandData);

        Task WriteImportWithMissingCurrencySettlementAccountAsync(
            ImportWithMissingCurrencySettlementAccountCurrencyTransferToAccount commandData);
    }
}