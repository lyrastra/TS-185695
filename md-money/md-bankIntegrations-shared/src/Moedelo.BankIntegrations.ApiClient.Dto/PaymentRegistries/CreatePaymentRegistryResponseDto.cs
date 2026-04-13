using System.Collections.Generic;

namespace Moedelo.BankIntegrations.Dto.PaymentRegistries
{
    public class CreatePaymentRegistryResponseDto : BaseResponseDto
    {
        public string CorrelationId { get; set; }
        public ErrorDto Error { get; set; }
        public List<PaymentRegistryResultErrorDto> PaymentErrors { get; set; }
    }
}
