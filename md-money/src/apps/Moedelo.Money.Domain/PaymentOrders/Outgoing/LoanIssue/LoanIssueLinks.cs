using Moedelo.Money.Domain.LinkedDocuments;

namespace Moedelo.Money.Domain.PaymentOrders.Outgoing.LoanIssue
{
    public class LoanIssueLinks
    {
        public RemoteServiceResponse<ContractLink> Contract { get; set; }
    }
}