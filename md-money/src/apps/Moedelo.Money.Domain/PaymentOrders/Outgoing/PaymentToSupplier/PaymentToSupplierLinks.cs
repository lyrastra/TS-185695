using System.Collections.Generic;
using Moedelo.Money.Domain.LinkedDocuments;

namespace Moedelo.Money.Domain.PaymentOrders.Outgoing.PaymentToSupplier
{
    public class PaymentToSupplierLinks
    {
        public RemoteServiceResponse<ContractLink> Contract { get; set; }

        public RemoteServiceResponse<IReadOnlyCollection<DocumentLink>> Documents { get; set; }

        public RemoteServiceResponse<IReadOnlyCollection<InvoiceLink>> Invoices { get; set; }

        public RemoteServiceResponse<decimal?> ReserveSum { get; set; }
    }
}