using System.Collections.Generic;
using Moedelo.Money.Domain.LinkedDocuments;

namespace Moedelo.Money.Domain.PaymentOrders.Outgoing.CurrencyPaymentToSupplier
{
    public class CurrencyPaymentToSupplierLinks
    {
        public RemoteServiceResponse<ContractLink> Contract { get; set; }
        
        public RemoteServiceResponse<IReadOnlyCollection<DocumentLink>> Documents { get; set; }
    }
}