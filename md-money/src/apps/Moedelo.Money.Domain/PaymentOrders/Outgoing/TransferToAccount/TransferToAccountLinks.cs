using Moedelo.Money.Domain.LinkedDocuments;

namespace Moedelo.Money.Domain.PaymentOrders.Outgoing.PaymentToSupplier
{
    public class TransferToAccountLinks
    {
        public RemoteServiceResponse<PaymentOrderLink> TransferFromAccount { get; set; }
    }
}