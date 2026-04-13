using System.Collections.Generic;
using Moedelo.Money.Domain.LinkedDocuments;

namespace Moedelo.Money.Domain.PaymentOrders.Incoming.PaymentFromCustomer
{
    public class PaymentFromCustomerLinks
    {
        public RemoteServiceResponse<ContractLink> Contract { get; set; }

        public RemoteServiceResponse<IReadOnlyCollection<BillLink>> Bills { get; set; }

        public RemoteServiceResponse<IReadOnlyCollection<DocumentLink>> Documents { get; set; }

        public RemoteServiceResponse<IReadOnlyCollection<InvoiceLink>> Invoices { get; set; }

        public RemoteServiceResponse<decimal?> ReserveSum { get; set; }
    }
}