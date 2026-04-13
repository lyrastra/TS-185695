using System.Threading.Tasks;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Outsource.Models;
using Moedelo.Money.Domain.PaymentOrders.Outgoing.LoanIssue;

namespace Moedelo.Money.Business.Abstractions.PaymentOrders.Outgoing.LoanIssue;

public interface ILoanIssueOutsourceProcessor
{
    Task<OutsourceConfirmResult> ConfirmAsync(LoanIssueSaveRequest request);
}