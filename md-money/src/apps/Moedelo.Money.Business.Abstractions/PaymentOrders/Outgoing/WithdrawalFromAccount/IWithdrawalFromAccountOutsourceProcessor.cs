using System.Threading.Tasks;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Outsource.Models;
using Moedelo.Money.Domain.PaymentOrders.Outgoing.WithdrawalFromAccount;

namespace Moedelo.Money.Business.Abstractions.PaymentOrders.Outgoing.WithdrawalFromAccount;

public interface IWithdrawalFromAccountOutsourceProcessor
{
    Task<OutsourceConfirmResult> ConfirmAsync(WithdrawalFromAccountSaveRequest request);
}