using System.Collections.Generic;
using Moedelo.Money.Domain.LinkedDocuments;

namespace Moedelo.Money.Domain.PaymentOrders.Outgoing.PaymentToAccountablePerson
{
    public class PaymentToAccountablePersonLinks
    {
        public RemoteServiceResponse<IReadOnlyCollection<AdvanceStatementLink>> Documents { get; set; }
    }
}