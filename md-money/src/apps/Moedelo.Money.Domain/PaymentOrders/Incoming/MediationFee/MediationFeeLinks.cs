using System.Collections.Generic;
using Moedelo.Money.Domain.LinkedDocuments;

namespace Moedelo.Money.Domain.PaymentOrders.Incoming.MediationFee
{
    public class MediationFeeLinks
    {
        public RemoteServiceResponse<ContractLink> Contract { get; set; }

        public RemoteServiceResponse<IReadOnlyCollection<BillLink>> Bills { get; set; }

        public RemoteServiceResponse<IReadOnlyCollection<DocumentLink>> Documents { get; set; }
    }
}