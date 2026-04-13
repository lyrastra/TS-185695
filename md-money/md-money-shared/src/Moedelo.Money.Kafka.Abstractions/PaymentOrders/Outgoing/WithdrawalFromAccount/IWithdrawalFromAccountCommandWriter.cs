using System.Threading.Tasks;
using Moedelo.Money.Kafka.Abstractions.PaymentOrders.Outgoing.WithdrawalFromAccount.Commands;

namespace Moedelo.Money.Kafka.Abstractions.PaymentOrders.Outgoing.WithdrawalFromAccount
{
    public interface IWithdrawalFromAccountCommandWriter
    {
        Task WriteImportAsync(ImportWithdrawalFromAccount commandData);

        Task WriteImportDuplicateAsync(ImportDuplicateWithdrawalFromAccount commandData);

        Task WriteApplyIgnoreNumberAsync(ApplyIgnoreNumberWithdrawalFromAccount commandData);
    }
}
