using Moedelo.Money.Domain.PaymentOrders;
using Moedelo.Money.Domain.PaymentOrders.Outgoing.WithdrawalFromAccount;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.Abstractions.PaymentOrders.Outgoing.WithdrawalFromAccount
{
    public interface IWithdrawalFromAccountUpdater
    {
        Task<PaymentOrderSaveResponse> UpdateAsync(WithdrawalFromAccountSaveRequest saveRequest);
    }
}