using System.Threading.Tasks;
using Moedelo.Money.Domain.PaymentOrders;
using Moedelo.Money.Domain.PaymentOrders.Outgoing.LoanIssue;

namespace Moedelo.Money.Business.Abstractions.PaymentOrders.Outgoing.LoanIssue
{
    public interface ILoanIssueCreator
    {
        Task<PaymentOrderSaveResponse> CreateAsync(LoanIssueSaveRequest request);
    }
}