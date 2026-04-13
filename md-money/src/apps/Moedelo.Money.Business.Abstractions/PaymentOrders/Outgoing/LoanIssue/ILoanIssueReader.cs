using Moedelo.Money.Domain.PaymentOrders.Outgoing.LoanIssue;

namespace Moedelo.Money.Business.Abstractions.PaymentOrders.Outgoing.LoanIssue
{
    public interface ILoanIssueReader : IPaymentOrderReader<LoanIssueResponse>
    {
    }
}
