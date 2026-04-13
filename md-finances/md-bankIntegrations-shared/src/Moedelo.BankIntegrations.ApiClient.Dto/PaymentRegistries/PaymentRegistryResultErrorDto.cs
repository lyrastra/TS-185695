using System.Collections.Generic;

namespace Moedelo.BankIntegrations.Dto.PaymentRegistries
{
    public class PaymentRegistryResultErrorDto
    {
        public int Number { get; set; }
        public string AccountNumber { get; set; }
        public List<ErrorDto> Errors { get; set; }
    }
}
