using System;
using System.Collections.Generic;

namespace Moedelo.BankIntegrations.Dto.PaymentRegistries
{
    public class CreatePaymentRegistryRequestDto : BaseRequestDto
    {
        public string CompanyAccountNumber { get; set; }
        public DateTime? LoadDate { get; set; }
        public List<PaymentDto> Payments { get; set; }
    }
}
