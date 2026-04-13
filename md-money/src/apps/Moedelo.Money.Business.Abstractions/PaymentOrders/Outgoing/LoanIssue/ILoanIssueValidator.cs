using System.Threading.Tasks;
using Moedelo.Money.Domain.PaymentOrders.Outgoing.LoanIssue;

namespace Moedelo.Money.Business.Abstractions.PaymentOrders.Outgoing.LoanIssue
{
    public interface ILoanIssueValidator
    {
        Task ValidateAsync(LoanIssueSaveRequest request);
    }
}
