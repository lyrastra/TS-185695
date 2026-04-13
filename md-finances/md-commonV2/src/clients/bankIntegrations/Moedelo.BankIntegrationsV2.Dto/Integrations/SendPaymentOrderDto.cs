using System.Collections.Generic;

namespace Moedelo.BankIntegrationsV2.Dto.Integrations
{
    public class SendPaymentOrderDto
    {
        public List<PaymentOrderDto> PaymentOrders { get; set; }
        public IntegrationIdentityDto Identity { get; set; }
    }
}
