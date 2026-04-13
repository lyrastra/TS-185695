using Moedelo.Money.Domain.LinkedDocuments;

namespace Moedelo.Money.Domain.PaymentOrders.Outgoing.PaymentToSupplier
{
    public class TransferFromAccountLinks
    {
        public RemoteServiceResponse<PaymentOrderLink> TransferToAccount { get; set; }
    }
}