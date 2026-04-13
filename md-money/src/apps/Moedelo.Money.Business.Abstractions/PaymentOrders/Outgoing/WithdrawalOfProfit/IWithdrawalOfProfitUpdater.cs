using Moedelo.Money.Domain.PaymentOrders;
using Moedelo.Money.Domain.PaymentOrders.Outgoing.WithdrawalOfProfit;

namespace Moedelo.Money.Business.Abstractions.PaymentOrders.Outgoing.WithdrawalOfProfit
{
    public interface IWithdrawalOfProfitUpdater : IPaymentOrderUpdater<WithdrawalOfProfitSaveRequest, PaymentOrderSaveResponse>
    {
    }
}