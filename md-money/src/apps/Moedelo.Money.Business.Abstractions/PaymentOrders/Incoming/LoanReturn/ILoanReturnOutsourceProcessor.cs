using System.Threading.Tasks;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Outsource.Models;
using Moedelo.Money.Domain.PaymentOrders.Incoming.LoanReturn;

namespace Moedelo.Money.Business.Abstractions.PaymentOrders.Incoming.LoanReturn;

public interface ILoanReturnOutsourceProcessor
{
    Task<OutsourceConfirmResult> ConfirmAsync(LoanReturnSaveRequest request);
}