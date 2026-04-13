using Moedelo.Money.Domain.LinkedDocuments;

namespace Moedelo.Money.Domain.PaymentOrders.Incoming.LoanReturn
{
    public class LoanReturnLinks
    {
        public RemoteServiceResponse<ContractLink> Contract { get; set; }
    }
}