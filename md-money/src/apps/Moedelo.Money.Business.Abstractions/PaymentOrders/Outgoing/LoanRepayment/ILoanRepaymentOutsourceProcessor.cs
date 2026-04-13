using System.Threading.Tasks;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Outsource.Models;
using Moedelo.Money.Domain.PaymentOrders.Outgoing.LoanRepayment;

namespace Moedelo.Money.Business.Abstractions.PaymentOrders.Outgoing.LoanRepayment;

public interface ILoanRepaymentOutsourceProcessor
{
    Task<OutsourceConfirmResult> ConfirmAsync(LoanRepaymentSaveRequest request);
}