using Moedelo.Money.Domain.PaymentOrders;
using Moedelo.Money.Domain.PaymentOrders.Outgoing.WithdrawalOfProfit;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.Abstractions.PaymentOrders.Outgoing.WithdrawalOfProfit
{
    public interface IWithdrawalOfProfitCreator
    {
        Task<PaymentOrderSaveResponse> CreateAsync(WithdrawalOfProfitSaveRequest saveRequest);
    }
}