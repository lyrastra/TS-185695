using Moedelo.Money.Domain.LinkedDocuments;

namespace Moedelo.Money.Domain.PaymentOrders.Outgoing.Deduction
{
    public class DeductionLinks
    {
        public RemoteServiceResponse<ContractLink> Contract { get; set; }
    }
}