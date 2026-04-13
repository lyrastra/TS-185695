using System.Collections.Generic;

namespace Moedelo.BankIntegrationsV2.Dto.IntegrationError
{
    public class IntegrationErrorSetReadDto
    {
        public List<int> Ids { get; set; }

        public int FirmId { get; set; }
    }
}