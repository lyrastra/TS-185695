using System;

namespace Moedelo.BankIntegrationsV2.Dto.IntegrationRequest
{
    public class IntegrationRequestResetDto
    {
        public int FirmId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
