using Moedelo.BankIntegrations.Enums;
using System.Collections.Generic;

namespace Moedelo.BankIntegrations.Dto.PaymentRegistries
{
    public class GetPaymentRegistryResultResponseDto : BaseResponseDto
    {
        public SalaryPaymentRegistryResultCode? ResultCode { get; set; }
        public ErrorDto Error { get; set; }
        public List<PaymentRegistryResultErrorDto> PaymentErrors { get; set; }
        public string PaymentRegistryId { get; set; }
    }
}
