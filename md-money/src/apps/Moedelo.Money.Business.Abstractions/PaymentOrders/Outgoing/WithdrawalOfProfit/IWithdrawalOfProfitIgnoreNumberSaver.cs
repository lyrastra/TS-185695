using System.Threading.Tasks;
using Moedelo.Money.Domain.PaymentOrders.Outgoing.WithdrawalOfProfit;

namespace Moedelo.Money.Business.Abstractions.PaymentOrders.Outgoing.WithdrawalOfProfit
{
    public interface IWithdrawalOfProfitIgnoreNumberSaver
    {
        Task ApplyIgnoreNumberAsync(WithdrawalOfProfitApplyIgnoreNumberRequest applyRequest);
    }
}
