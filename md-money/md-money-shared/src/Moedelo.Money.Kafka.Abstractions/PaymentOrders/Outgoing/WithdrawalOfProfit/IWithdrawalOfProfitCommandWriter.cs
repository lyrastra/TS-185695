using System.Threading.Tasks;
using Moedelo.Money.Kafka.Abstractions.PaymentOrders.Outgoing.WithdrawalOfProfit.Commands;

namespace Moedelo.Money.Kafka.Abstractions.PaymentOrders.Outgoing.WithdrawalOfProfit
{
    public interface IWithdrawalOfProfitCommandWriter
    {
        Task WriteImportAsync(
            ImportWithdrawalOfProfit commandData);

        Task WriteImportDuplicateAsync(
            ImportDuplicateWithdrawalOfProfit commandData);

        Task WriteImportWithMissingContractorAsync(
            ImportWithMissingContractorWithdrawalOfProfit commandData);

        Task WriteApplyIgnoreNumberAsync(ApplyIgnoreNumberWithdrawalOfProfit commandData);
    }
}
