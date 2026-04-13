using Moedelo.Money.Domain.LinkedDocuments;

namespace Moedelo.Money.Domain.PaymentOrders.Outgoing.LoanRepayment
{
    public class LoanRepaymentLinks
    {
        public RemoteServiceResponse<ContractLink> Contract { get; set; }
    }
}