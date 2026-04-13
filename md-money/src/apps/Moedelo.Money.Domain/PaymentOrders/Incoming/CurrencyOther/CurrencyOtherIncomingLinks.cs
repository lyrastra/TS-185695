using System.Collections.Generic;
using Moedelo.Money.Domain.LinkedDocuments;

namespace Moedelo.Money.Domain.PaymentOrders.Incoming.CurrencyOther
{
    public class CurrencyOtherIncomingLinks
    {
        public RemoteServiceResponse<ContractLink> Contract { get; set; }
    }
}