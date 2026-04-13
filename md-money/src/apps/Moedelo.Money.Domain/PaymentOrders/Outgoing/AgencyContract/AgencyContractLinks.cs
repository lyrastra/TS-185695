using Moedelo.Money.Domain.LinkedDocuments;

namespace Moedelo.Money.Domain.PaymentOrders.Outgoing.AgencyContract
{
    public class AgencyContractLinks
    {
        public RemoteServiceResponse<ContractLink> Contract { get; set; }
    }
}