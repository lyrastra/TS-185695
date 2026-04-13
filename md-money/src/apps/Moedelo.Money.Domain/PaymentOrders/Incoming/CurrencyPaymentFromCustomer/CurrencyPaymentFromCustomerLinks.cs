using System.Collections.Generic;
using Moedelo.Money.Domain.LinkedDocuments;

namespace Moedelo.Money.Domain.PaymentOrders.Incoming.CurrencyPaymentFromCustomer
{
    public class CurrencyPaymentFromCustomerLinks
    {
        public RemoteServiceResponse<ContractLink> Contract { get; set; }
        
        public RemoteServiceResponse<IReadOnlyCollection<DocumentLink>> Documents { get; set; }
    }
}