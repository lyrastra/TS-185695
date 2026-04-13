using Moedelo.Money.Domain.LinkedDocuments;

namespace Moedelo.Money.Domain.PaymentOrders.Outgoing.RentPayment
{
    public class RentPaymentLinks
    {
        public RemoteServiceResponse<ContractLink> Contract { get; set; }

        public RemoteServiceResponse<InventoryCard> InventoryCard { get; set; }

    }
}
