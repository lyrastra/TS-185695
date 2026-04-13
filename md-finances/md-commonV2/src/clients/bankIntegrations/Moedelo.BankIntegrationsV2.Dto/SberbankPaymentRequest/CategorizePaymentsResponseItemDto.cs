using Moedelo.BankIntegrations.Enums.Sberbank;

namespace Moedelo.BankIntegrationsV2.Dto.SberbankPaymentRequest
{
    public class CategorizePaymentsResponseItemDto
    {
        public int FirmId { get; set; }

        public PaymentRequestCategory PaymentRequestCategory { get; set; }

        public string Description { get; set; }
    }
}