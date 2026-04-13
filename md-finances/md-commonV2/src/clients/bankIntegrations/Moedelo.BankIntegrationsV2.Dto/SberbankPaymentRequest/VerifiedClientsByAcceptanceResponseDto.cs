using System.Collections.Generic;

namespace Moedelo.BankIntegrationsV2.Dto.SberbankPaymentRequest
{
    public class VerifiedClientsByAcceptanceResponseDto
    {
        public string Error { get; set; }
        public int LastAcceptanceId { get; set; }
        public List<int> SuccessfulFirmIds { get; set; }
        public List<VerifiedAcceptanceResponseItemDto> UnsuccessfulItems { get; set; }
        public List<VerifiedAcceptanceResponseItemDto> ErrorItems { get; set; }
    }
}