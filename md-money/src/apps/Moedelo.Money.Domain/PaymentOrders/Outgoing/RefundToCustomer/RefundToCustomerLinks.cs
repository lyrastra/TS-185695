using System.Collections.Generic;
using Moedelo.Money.Domain.LinkedDocuments;

namespace Moedelo.Money.Domain.PaymentOrders.Outgoing.RefundToCustomer
{
    public class RefundToCustomerLinks
    {
        public RemoteServiceResponse<ContractLink> Contract { get; set; }
        
        public RemoteServiceResponse<RetailRefundLink> RetailRefund { get; set; }
    }
}