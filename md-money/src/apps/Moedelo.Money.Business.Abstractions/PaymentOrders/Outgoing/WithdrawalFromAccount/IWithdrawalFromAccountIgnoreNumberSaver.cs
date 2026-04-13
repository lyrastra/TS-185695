using System.Threading.Tasks;
using Moedelo.Money.Domain.PaymentOrders.Outgoing.WithdrawalFromAccount;

namespace Moedelo.Money.Business.Abstractions.PaymentOrders.Outgoing.WithdrawalFromAccount
{
    public interface IWithdrawalFromAccountIgnoreNumberSaver
    {
        Task ApplyIgnoreNumberAsync(WithdrawalFromAccountApplyIgnoreNumberRequest applyRequest);
    }
}
