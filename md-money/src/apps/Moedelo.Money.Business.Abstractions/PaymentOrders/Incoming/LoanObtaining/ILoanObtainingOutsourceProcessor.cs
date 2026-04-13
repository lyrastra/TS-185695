using System.Threading.Tasks;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Outsource.Models;
using Moedelo.Money.Domain.PaymentOrders.Incoming.LoanObtaining;

namespace Moedelo.Money.Business.Abstractions.PaymentOrders.Incoming.LoanObtaining;

public interface ILoanObtainingOutsourceProcessor
{
    Task<OutsourceConfirmResult> ConfirmAsync(LoanObtainingSaveRequest request);
}