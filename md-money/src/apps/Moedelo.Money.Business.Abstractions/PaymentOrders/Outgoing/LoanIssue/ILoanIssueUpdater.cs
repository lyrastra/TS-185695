using Moedelo.Money.Domain.PaymentOrders;
using Moedelo.Money.Domain.PaymentOrders.Outgoing.LoanIssue;

namespace Moedelo.Money.Business.Abstractions.PaymentOrders.Outgoing.LoanIssue
{
    public interface ILoanIssueUpdater : IPaymentOrderUpdater<LoanIssueSaveRequest, PaymentOrderSaveResponse>
    {
    }
}