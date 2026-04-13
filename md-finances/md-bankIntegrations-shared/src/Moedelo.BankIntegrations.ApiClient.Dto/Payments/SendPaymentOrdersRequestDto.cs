using System.Collections.Generic;
using Moedelo.BankIntegrations.Models.PaymentOrder;

namespace Moedelo.BankIntegrations.Dto.Payments
{
    public class SendPaymentOrdersRequestDto: BaseRequestDto
    {
        public List<PaymentOrder> PaymentOrders { get; set; }
    }
}
