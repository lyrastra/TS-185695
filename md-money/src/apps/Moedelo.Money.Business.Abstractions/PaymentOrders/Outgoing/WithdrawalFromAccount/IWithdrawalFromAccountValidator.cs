using Moedelo.Money.Domain.PaymentOrders.Outgoing.WithdrawalFromAccount;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.Abstractions.PaymentOrders.Outgoing.WithdrawalFromAccount
{
    public interface IWithdrawalFromAccountValidator
    {
        Task ValidateAsync(WithdrawalFromAccountSaveRequest request);
    }
}