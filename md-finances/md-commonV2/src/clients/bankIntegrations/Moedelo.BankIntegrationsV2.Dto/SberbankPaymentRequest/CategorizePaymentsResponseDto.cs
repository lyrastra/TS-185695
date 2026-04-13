using System.Collections.Generic;

namespace Moedelo.BankIntegrationsV2.Dto.SberbankPaymentRequest
{
    public class CategorizePaymentsResponseDto
    {
        public bool Success { get; set; }
        
        public string Error { get; set; }
        
        public List<CategorizePaymentsResponseItemDto> Items { get; set; }

        public List<CategorizePaymentsErrorResponseItemDto> ErrorItems { get; set; }
    }
}