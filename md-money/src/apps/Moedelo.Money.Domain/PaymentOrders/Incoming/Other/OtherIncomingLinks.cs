using System.Collections.Generic;
using Moedelo.Money.Domain.LinkedDocuments;

namespace Moedelo.Money.Domain.PaymentOrders.Incoming.Other
{
    public class OtherIncomingLinks
    {
        public RemoteServiceResponse<IReadOnlyCollection<BillLink>> Bills { get; set; }

        public RemoteServiceResponse<ContractLink> Contract { get; set; }
    }
}