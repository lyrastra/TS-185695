using Moedelo.Money.Domain.LinkedDocuments;

namespace Moedelo.Money.Domain.PaymentOrders.Outgoing.CurrencyOther
{
    public class CurrencyOtherOutgoingLinks
    {
        public RemoteServiceResponse<ContractLink> Contract { get; set; }
    }
}