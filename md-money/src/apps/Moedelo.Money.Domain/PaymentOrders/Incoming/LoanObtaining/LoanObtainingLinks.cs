using Moedelo.Money.Domain.LinkedDocuments;

namespace Moedelo.Money.Domain.PaymentOrders.Incoming.LoanObtaining
{
    public class LoanObtainingLinks
    {
        public RemoteServiceResponse<ContractLink> Contract { get; set; }
    }
}