using Moedelo.BankIntegrations.ApiClient.Dto.BankOperation;
using System.Collections.Generic;

namespace Moedelo.BankIntegrations.ApiClient.Dto.Payments
{
    public class SendPaymentOrderRequestDto
    {
        public List<PaymentOrderDto> PaymentOrders { get; set; }
        public IntegrationIdentityDto Identity { get; set; }
    }
}
