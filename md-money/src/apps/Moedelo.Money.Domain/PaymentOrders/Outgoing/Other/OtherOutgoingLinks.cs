using Moedelo.Money.Domain.LinkedDocuments;

namespace Moedelo.Money.Domain.PaymentOrders.Outgoing.Other
{
    public class OtherOutgoingLinks
    {
        public RemoteServiceResponse<ContractLink> Contract { get; set; }
    }
}