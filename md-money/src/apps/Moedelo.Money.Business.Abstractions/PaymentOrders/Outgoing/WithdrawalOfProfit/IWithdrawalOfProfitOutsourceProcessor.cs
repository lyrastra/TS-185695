using System.Threading.Tasks;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Outsource.Models;
using Moedelo.Money.Domain.PaymentOrders.Outgoing.WithdrawalOfProfit;

namespace Moedelo.Money.Business.Abstractions.PaymentOrders.Outgoing.WithdrawalOfProfit;

public interface IWithdrawalOfProfitOutsourceProcessor
{
    Task<OutsourceConfirmResult> ConfirmAsync(WithdrawalOfProfitSaveRequest request);
}