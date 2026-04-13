using Moedelo.Money.Domain.LinkedDocuments;

namespace Moedelo.Money.Domain.PaymentOrders.Incoming.RefundFromAccountablePerson
{
    public class RefundFromAccountablePersonLinks
    {
        public RemoteServiceResponse<AdvanceStatementLink> AdvanceStatement { get; set; }
    }
}